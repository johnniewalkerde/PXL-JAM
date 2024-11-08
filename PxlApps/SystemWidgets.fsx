#load "./Helper/PxlLocalDevShadow.fsx"
open PxlLocalDevShadow

open System
open Pxl
open Pxl.Draw
open Pxl.Draw.Fsi

let createCanvas () = CanvasProxy.create CanvasProxy.Channel.Tcp "localhost"


// -------------------------------------------------------------


let blinkingCursor =
    scene {
        let! color = Anim.toggleValues(0.25, [ Colors.black; Colors.white ], Repeat.Loop)
        // pxl(0, 0).stroke(color)
        // text4x5("█", 0, 0, color)
        text.var4x5("_", 0, 0).color(color)
    }

blinkingCursor |> Eval.start createCanvas
