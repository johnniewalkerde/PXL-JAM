/*
 * ******************************************************************
 * NOTE: This file must only be edited in src/pxl-receiver-canvas
 * because it is copied as a file-dependency to other projects.
 * ******************************************************************
 */

class CombinedFrameBuffer {
  private readonly _frameLength: number;

  private readonly _backArrays: Uint8Array[] = [];
  private _currArray: Uint8Array | undefined = undefined;
  private _currArrIdx = 0;

  constructor(width: number, height: number) {
    this._frameLength = width * height * bpp;
  }

  pushFrames(frames: Uint8Array) {
    if (frames.length === 0)
      throw new Error('Pushing empty frame is not allowed');
    if (frames.length % this._frameLength !== 0)
      throw new Error(`Invalid frame length; must be a multiple of ${this._frameLength}`);
    this._backArrays.push(frames);
  }

  get remainingFrameCount(): number {
    const bbFramesCount = this._backArrays.map(a => a.length).reduce((a, b) => a + b, 0) / this._frameLength;
    const currFramesCount = this.currentFrameHasData() ? (this._currArray!.length - this._currArrIdx) / this._frameLength : 0;
    return bbFramesCount + currFramesCount;
  }

  private currentFrameHasData(): boolean {
    return this._currArray !== undefined && this._currArrIdx < this._currArray.length
  }

  get hasFrames(): boolean {
    return this.currentFrameHasData() || this._backArrays.length > 0;
  }

  tryNextFrame(): Uint8Array | undefined {
    if (!this.hasFrames) {
      return undefined;
    }

    if (!this.currentFrameHasData()) {
      this._currArray = this._backArrays.shift();
      this._currArrIdx = 0;
    }

    const frame = this._currArray!.slice(this._currArrIdx, this._currArrIdx + this._frameLength);
    this._currArrIdx += this._frameLength;

    return frame;
  }
}

export const bpp = 3;

type TransmissionSyncedState = {
  startProcessingAt: Date,
}

type WaitingForNewTransmissionState = {
  waitingSince: Date
}

type State
  = { tag: 'processingFrame', nextFrameAt: Date }
  | { tag: 'waitingForNewTransmission' } & WaitingForNewTransmissionState
  | { tag: 'transmissionSynced' } & TransmissionSyncedState;

export type PxlBuffer = Readonly<{
  fps: number;
  frameBufferSize: number;
  setBackBuffer: (newBackBuffer: Uint8Array) => void;
  startProcessingLoop: () => (() => void);
}>;

export function createBuffer
  (
    width: number,
    height: number,
    fps: number,
    frameBufferSize: number,
    relativeFbDelayUntilStart: number,
    processFrame: (buffer: Uint8Array) => void,
    clearDisplay: () => void,
    onTransmissionLost: () => void,
  ): PxlBuffer {

  const frameBuffer = new CombinedFrameBuffer(width, height);
  const durationForOneFrame = 1000 / fps;

  let state: State = {
    tag: 'waitingForNewTransmission',
    waitingSince: new Date()
  };

  let lastLogTimestamp = new Date();
  let framesCountSinceLastLog = 0;

  function processState(): State {
    const now = new Date();

    switch (state.tag) {

      case 'processingFrame': {
        if (state.nextFrameAt > now)
          return state;

        const framePxlx = frameBuffer.tryNextFrame();
        if (framePxlx === undefined) {
          console.warn(`Buffer underrun; lost transmission  - waiting for new transmission ...`);
          onTransmissionLost();
          return {
            tag: 'waitingForNewTransmission',
            waitingSince: now
          };
        } else {
          processFrame(framePxlx);
          framesCountSinceLastLog++;
          // TODO: we still could have the effect of requiring to skip some frames
          return { tag: 'processingFrame', nextFrameAt: new Date(state.nextFrameAt.getTime() + durationForOneFrame) };
        }
      }

      case 'waitingForNewTransmission': {
        if (frameBuffer.hasFrames) {
          const frameBufferProcessingDuration = frameBufferSize * 1000 / fps * relativeFbDelayUntilStart;

          console.log(`New transmission synced - start processing in ${frameBufferProcessingDuration}ms ...`);
          return {
            tag: 'transmissionSynced',
            startProcessingAt: new Date(now.getTime() + frameBufferProcessingDuration),
          };
        }

        return state;
      }

      case 'transmissionSynced':
        if (state.startProcessingAt < now) {
          console.log('Sync delay awaited - starting processing in the next frame ...');
          return { tag: 'processingFrame', nextFrameAt: now };
        }

        return state;

      default:
        const _exhaustive: never = state;
        return _exhaustive;
    }
  }

  const setBackBuffer = (newBackBuffer: Uint8Array) => {
    frameBuffer.pushFrames(newBackBuffer);
  }

  let isStarted = false;

  function startProcessingLoop() {
    if (isStarted) {
      throw new Error('Processing loop already started');
    }

    const doWork = () => {
      const logDelay = 10000;
      const timePassedSinceLastLog = new Date().getTime() - lastLogTimestamp.getTime();
      if (timePassedSinceLastLog > logDelay) {
        console.log(`Processed frames: ${framesCountSinceLastLog} (FPS = ${framesCountSinceLastLog / (timePassedSinceLastLog / 1000)})  -  Frames left in buffer: ${frameBuffer.remainingFrameCount}`);
        framesCountSinceLastLog = 0;
        lastLogTimestamp = new Date();
      }

      // const startTime = new Date();

      state = processState();

      // const durationInMs = new Date().getTime() - startTime.getTime();
      // const overallWaitTime = 1000 / fps - durationInMs;

      // if (overallWaitTime < 0) {
      //   // TODO: skip n frames if overallWaitTime is negative
      //   console.warn(`Processing took too long: ${durationInMs}ms - skipping frames ...`);
      //   doWork();
      // } else {
      //   setTimeout(doWork, Math.max(0, overallWaitTime));
      // }
    }

    isStarted = true;

    const handle = setInterval(() => doWork(), durationForOneFrame / 5);

    const stopProcessingLoop = () => {
      clearInterval(handle);
      clearDisplay();
      isStarted = false;
    }

    return stopProcessingLoop;
  }

  return {
    fps,
    frameBufferSize,
    setBackBuffer,
    startProcessingLoop
  };
}
