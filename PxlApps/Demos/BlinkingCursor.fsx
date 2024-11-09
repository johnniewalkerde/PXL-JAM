#load "../../.deps/PxlLocalDevShadow.fsx"
open PxlLocalDevShadow

open System
open Pxl
open Pxl.Ui



// -------------------------------------------------------------



let blinkingCursor =
    scene {
        let! color = Anim.toggleValues(0.25, [ Colors.black; Colors.white ], Repeat.Loop)
        // pxl(0, 0).stroke(color)
        // text4x5("█", 0, 0, color)
        text.var4x5("_", 0, 0).color(color)
    }

blinkingCursor |> Simulator.start
