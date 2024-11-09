#load "../../.deps/PxlLocalDevShadow.fsx"
open PxlLocalDevShadow

open System
open Pxl
open Pxl.Draw



// -------------------------------------------------------------



// Iterating and yielding (stateful) components
// also works in a nested way.
scene {
    bg(Colors.darkBlue)
    for x in 0..4 do
        for y in 0..4 do
            rect.xywh(x*4, y*4, 1, 1).fill(Colors.blue)
}
|> Simulator.start

