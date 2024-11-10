#load "../../.deps/PxlLocalDevShadow.fsx"
open PxlLocalDevShadow

open System
open Pxl
open Pxl.Ui



// -------------------------------------------------------------



// ...or some rectangles ...
scene {
    // ...if you wanna know which drawing primitives are available, use the `Ui` namespace.
    Ui.Shapes.bg(Colors.green)

    rect.xywh(2, 2, 20, 20).fill(Colors.blue)
    rect.xywh(4, 4, 16, 16).fill(Colors.white)
}
|> Simulator.start


