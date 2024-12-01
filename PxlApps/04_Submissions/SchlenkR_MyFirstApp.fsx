#load "../../.deps/PxlLocalDevShadow.fsx"
open PxlLocalDevShadow

open System
open Pxl
open Pxl.Ui


// -------------------------------------------------------------


// If you want to load an asset, the root path is the ./PxlApps/assets folder.
let myLoadedImage = Image.load(PxlLocalDev.loadAsset "submissions/SchlenkR_MyImage.png")

// The component for the plant rising animation.
let plantRising =
    scene {
        let! y = Anim.easeInOutSine(4, 24, 0, Repeat.StopAtEnd)
        image(myLoadedImage, 0, y.value)
    }

// Let's start the animation in the simulator!
plantRising |> Simulator.start


// Run an App
// ==========
//
// - Ensure the simulator is running (using VSCode build task - see README.md).
// - Select the entire content of this file and run it by pressing `Alt+Enter` (Windows) or `Cmd+Enter` (Mac).
// - Change code: You can modify the code, open new files, and re-run apps as often as you like. Simply re-evaluate the **entire file** (that's the mose easy way.)
// - Stop the simulator: Place your cursor on the commented line below (`Simulator.stop ()`) and press `Alt+Enter` (Windows) or `Cmd+Enter` (Mac) to stop the simulator.

(*
Simulator.stop ()
*)
