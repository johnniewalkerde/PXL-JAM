#load "../../.deps/PxlLocalDevShadow.fsx"
open PxlLocalDevShadow

open System
open Pxl
open Pxl.Ui



// -------------------------------------------------------------



// and some text!
scene {
    let y row = (Fonts.mono3x5.height + 2) * row

    text.mono3x5("fsharp").y(y 0).color(Colors.white)
    text.mono3x5("kicks").y(y 1).color(Colors.white)
    text.mono6x6("A$$").y(y 2).color(Colors.white)
}
|> Simulator.start


