#load "../../.deps/PxlLocalDevShadow.fsx"
open PxlLocalDevShadow

open System
open Pxl
open Pxl.Ui



// -------------------------------------------------------------


let finalScene =
    scene {
        let! ctx = getCtx()

        for x in 0..ctx.width do
            for y in 0..ctx.height do
                let xrel = float x / float ctx.width
                let yrel = float y / float ctx.height
                pxl.xy(x, y).stroke(Colors.blue.opacity(xrel * yrel))
    }


finalScene |> Simulator.start

(*
Simulator.stop ()
*)

