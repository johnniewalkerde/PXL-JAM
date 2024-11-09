#load "fsi/PxlLocalDev.fsx"

open System.IO

module PxlLocalDev =
    let loadAsset (assetPath: string) =
        let path = Path.Combine(__SOURCE_DIRECTORY__, "../PxlApps/assets", assetPath)
        let content = File.ReadAllBytes(path)
        new MemoryStream(content)

let createCanvas () = Pxl.CanvasProxy.create Pxl.CanvasProxy.Channel.Tcp "localhost"

module Simulator =
    let start scene = Pxl.Draw.Fsi.Eval.start createCanvas scene
