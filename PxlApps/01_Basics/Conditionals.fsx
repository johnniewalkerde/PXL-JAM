#load "../../.deps/PxlLocalDevShadow.fsx"
open PxlLocalDevShadow

open System
open Pxl
open Pxl.Ui



// -------------------------------------------------------------



// It is possible to render views conditionally.
// Please note that in an `else` branch,
// there are only 2 things allowed: `preserveState` and `discardState`.
// They both are required to tell the system if the (eventual) state
// of the components from the `if` branch should be kept for upcoming
// frames or not.
scene {
    let! cycle = Logic.count(0, 1)
    if cycle % 10 = 0 then
        let! innerCount = Logic.count(0, 1)
        text.mono6x6($"{innerCount}").color(Colors.white)
    else preserveState
}
|> Simulator.start



// TODO:
// Pitfall - sometimes a "do if" is required (not only an "if") to make the code work.
// In that case, also an "else preserve" or "else discard" must NOT be used.


let doIfPitfall =
    scene {
        let! ctx = getCtx ()

        let! currSceneNo = useState { 0 }
        let! timeLeft = Anim.linear(10, 0, 1, repeat = Repeat.Loop, autoStart = true)
        let! swipeOffsetAnim = Anim.easeInOutCubic(
            0.7,
            0,
            0,
            repeat = Repeat.StopAtEnd,
            autoStart = false)

        scene {
            Layer.offset(swipeOffsetAnim.value, 0)
            scene1
        }

        scene {
            Layer.offset(swipeOffsetAnim.value + float ctx.width, 0)
            scene2
        }

        do if timeLeft.isAtEndTrigger then
            if currSceneNo.value = 0 then
                swipeOffsetAnim.startValue <- 0
                swipeOffsetAnim.endValue <- -24
            else
                swipeOffsetAnim.startValue <- -24
                swipeOffsetAnim.endValue <- 0

            swipeOffsetAnim.restart()

            currSceneNo.value <- currSceneNo.value + 1
            if currSceneNo.value = 2 then
                currSceneNo.value <- 0
    }


