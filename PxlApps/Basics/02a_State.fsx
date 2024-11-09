#load "../../.deps/PxlLocalDevShadow.fsx"
open PxlLocalDevShadow

open System
open Pxl
open Pxl.Ui



// -------------------------------------------------------------




// A simple counter with a state variable
scene {
    let! count = useState { 0 }
    text.mono6x6($"{count.value}").color(Colors.white)
    do count.value <- count.value + 1
}
|> Simulator.start


