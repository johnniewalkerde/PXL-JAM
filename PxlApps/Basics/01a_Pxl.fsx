#load "../../.deps/PxlLocalDevShadow.fsx"
open PxlLocalDevShadow

open System
open Pxl
open Pxl.Ui



// -------------------------------------------------------------



scene {
    let! ctx = getCtx()
    pxl.xy(0, 0).stroke(Colors.red).noAntiAlias()
    pxl.xy(ctx.width - 1, 0).stroke(Colors.blue)
    pxl.xy(0, ctx.height - 1).stroke(Colors.green)
    pxl.xy(ctx.width-1, ctx.height - 1).stroke(Colors.yellow)
}
|> Simulator.start

