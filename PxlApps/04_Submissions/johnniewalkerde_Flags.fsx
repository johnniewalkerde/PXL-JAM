#load "../../.deps/PxlLocalDevShadow.fsx"
#r "nuget: Svg.Skia, 1.0.0.18" //latest version compatible with SkiaSharp 2.88.7.0
#r "nuget: FSharp.Data"

open PxlLocalDevShadow
open System.IO
open Pxl
open Pxl.Ui
open SkiaSharp
open Svg.Skia
open FSharp.Data

[<Literal>]
let jsonUrl = "D:/projects/privat/PXL-JAM/PxlApps/assets/submissions/johnniewalkerde_countries.json"
type Countries = JsonProvider<jsonUrl>
let countries = Countries.Load jsonUrl

let getSvgImagePath (country : Countries.Binding) =
    //let country = 
    //  countries.Results.Bindings |> Seq.tryFind (fun item -> item.Id.Value |> string = countryCode)
    match country.Id.Value.String with
    | Some s -> Some (sprintf "D:/projects/privat/PXL-JAM/PxlApps/assets/submissions/johnniewalkerde/svg/%s.svg" s)
    | None -> None

let convertSvgToPng (svgFilePath: string) : Stream option =
    let svg = new SKSvg()
    let picture = svg.Load svgFilePath
    if picture <> null then
        // Create a bitmap based on the SVG's dimensions
        let bitmap = new SKBitmap(int picture.CullRect.Width, int picture.CullRect.Height)
        use canvas = new SKCanvas(bitmap)
        
        canvas.DrawColor(new SKColor(byte 0, byte 0, byte 0))
        // Draw the SVG onto the canvas
        canvas.DrawPicture(picture)
        canvas.Flush()

        // Encode the bitmap to PNG format
        use image = SKImage.FromBitmap(bitmap)
        use data = image.Encode(SKEncodedImageFormat.Png, 100)

        // Save the encoded data to a file
        let stream = new MemoryStream()
        data.SaveTo(stream)
        stream.Position <- 0
        Some stream
    else
        None

let ger = countries.Results.Bindings |> Seq.tryFind (fun c -> c.Id.Value.String.Value = "AU")
let p = getSvgImagePath ger.Value
let ms = convertSvgToPng p.Value

//let myLoadedImage = Image.load(ms.Value)

let mutable myLoadedImage = 
    let path = sprintf "submissions/johnniewalkerde/png/%s.png" "AU"
    Image.load(PxlLocalDev.loadAsset path).resize(24, 24, true)

let mutable width = 0
let mutable height = 0

let img = myLoadedImage.resize(int 48, int 48, false)
let imgFull = myLoadedImage.resize(24, 24, false)
// The component for the plant rising animation.
scene {
    let! pos = Anim.linear(10, 0, 5, Repeat.Loop)
    let! x = Anim.linear(4, 0, -25, Repeat.Loop)
    let! zoom = Anim.linear(20, 5, 0, Repeat.Loop)
    //let! width = Anim.linear(10, 1, 25, Repeat.Loop)
    //let! height = Anim.linear(10, 1, 25, Repeat.Loop)
    //let img = myLoadedImage.resize(int width.value, int height.value, false)
    
    //let! x = Anim.linear(3, -26, 26, Repeat.Loop)
    //image(myLoadedImage, int y.value, int (-y.value))
    let i, x1, y =
        match int pos.value with
        //| 0 -> (img, 0, 0)
        //| 1 -> (img, -24, 0)
        //| 2 -> (img, 0, -24)
        //| 3 -> (img, -24, -24)
        | _ -> (imgFull, 0, 0)
    let width, x = 
        match zoom.value with
        | v when v < 1.0 -> 24, 0
        | _ -> int (zoom.value * 24.0), int -(((zoom.value * 24.0) / 2.0) - 12.0)
    let height = width
    let i = myLoadedImage.resize(width, height, false)
    //printfn "%f %d" x.value y
    //image(i, x, x)
    image(i, 0, 0)
} |> Simulator.start
