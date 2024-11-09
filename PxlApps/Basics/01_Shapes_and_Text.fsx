#load "../../.deps/PxlLocalDevShadow.fsx"
open PxlLocalDevShadow

open System
open Pxl
open Pxl.Draw



// -------------------------------------------------------------



scene {
    let! ctx = getCtx()
    pxl.xy(0, 0).stroke(Colors.red).noAntiAlias()
    pxl.xy(ctx.width - 1, 0).stroke(Colors.blue)
    pxl.xy(0, ctx.height - 1).stroke(Colors.green)
    pxl.xy(ctx.width-1, ctx.height - 1).stroke(Colors.yellow)
}
|> Simulator.start



// ...or some rectangles ...
scene {
    rect.xywh(0, 0, 24, 24).fill(Colors.blue)
    rect.xywh(2, 2, 20, 20).fill(Colors.white)
}
|> Simulator.start



// ...if you wanna know which drawing primitives are available,
// use the `Draw` type (which is auto-opened above,
// and therefore redundant in this case):
scene {
    bg Colors.green
}
|> Simulator.start




// and some text!
scene {
    let txtRow(txt, i) = text.mono3x5(txt).y((Fonts.mono3x5.height + 2) * i).color(Colors.white)

    txtRow("fsharp", 0)
    txtRow("kicks", 1)
    txtRow("a$$", 2)
}
|> Simulator.start

