#load "../../.deps/PxlLocalDevShadow.fsx"
#r "nuget: Svg.Skia, 1.0.0.18" //latest version compatible with SkiaSharp 2.88.7.0
#r "nuget: FSharp.Data"
#r "nuget: Nager.Country"
#r "nuget: Nager.Country.Translation"
#r "nuget: ColorHelper"

open PxlLocalDevShadow
open System.IO
open Pxl
open Pxl.Ui
open SkiaSharp
open Svg.Skia
open FSharp.Data
open Nager.Country
open Nager.Country.Translation
open System.Net.Http

let countryProvider = new CountryProvider()
let sampleCountry = countryProvider.GetCountry(Alpha2Code.DE)

[<Literal>]
let assetsDir = __SOURCE_DIRECTORY__ + "/../assets/submissions"
[<Literal>]
let jsonUrl = assetsDir + "/johnniewalkerde_Flags.json"
type Countries = JsonProvider<jsonUrl>
let countries = Countries.Load jsonUrl

let getSvgImagePath (country : ICountryInfo) =
    sprintf "%s/johnniewalkerde_Flags/svg/%s.svg" assetsDir ((country.Alpha2Code |> string).ToLowerInvariant())

let httpClient = new HttpClient()
let imageUrl = sprintf "https://kapowaz.github.io/square-flags/flags/%s.svg" ((sampleCountry.Alpha2Code |> string).ToLowerInvariant())
let data = 
    async {
        let! response = httpClient.GetAsync(imageUrl) |> Async.AwaitTask
        response.EnsureSuccessStatusCode () |> ignore
        let! content = response.Content.ReadAsStringAsync() |> Async.AwaitTask
        return content
    }

let convertSvgToPng (svgData: string) : Stream option =
    let svg = new SKSvg()
    //let picture = svg.Load svgFilePath
    let picture = svg.FromSvg svgData
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
        use data = image.Encode(SKEncodedImageFormat.Png, 0)

        // Save the encoded data to a file
        let stream = new MemoryStream()
        data.SaveTo(stream)
        stream.Position <- 0
        Some stream
    else
        None

let samplePath = getSvgImagePath sampleCountry
//let ms = convertSvgToPng samplePath
let ms = convertSvgToPng (data |> Async.RunSynchronously)

let myLoadedImage = Image.load(ms.Value)

//let mutable myLoadedImage = 
//    let path = sprintf "submissions/johnniewalkerde_Flags/png/%s.png" "au"
//    Image.load(PxlLocalDev.loadAsset path).resize(24, 24, true)

let mutable width = 0
let mutable height = 0

let img = myLoadedImage.resize(int 48, int 48, false)
let imgFull = myLoadedImage.resize(24, 24, false)

open ColorHelper
let textShader (inputPoint : ShaderFxInput) = 
    let color = System.Drawing.Color.FromArgb(
        int inputPoint.pxlColor.a, 
        int inputPoint.pxlColor.r,
        int inputPoint.pxlColor.g,
        int inputPoint.pxlColor.b)
    //let rgb = new ColorHelper.RGB(inputPoint.pxlColor.r, inputPoin)
    //let hsl = ColorConverter.RgbToHsl(color)
    //hsl
    let outColor = System.Drawing.Color.FromArgb(color.ToArgb() ^^^ 0xFFFFFFFF)
    Color.argb(outColor.A, outColor.R, outColor.G, outColor.B)

type State =
    | FadeIn
    | Countdown
    | Transition
    | Solution
    | FadeOut

let getNextState state =
    match state with
    | FadeIn -> Countdown
    | Countdown -> Transition
    | Transition -> Solution
    | Solution -> FadeOut
    | FadeOut -> FadeIn

let fullCycleDuration = 10
let durationOfState fullDuration (state : State) =
    match state with
    | FadeIn -> 0.05
    | Countdown -> 0.5
    | Transition -> 0.1
    | Solution -> 0.3
    | FadeOut -> 0.05
    * fullDuration

let errorScene =
    scene { 
        let! opacity = Anim.linear(1, 1.0, 0.0)
        rect.xywh(0, 0, 24, 24).fill(Colors.red) 
    }

scene {
    let! ctx = getCtx()
    let! currentState = useState { FadeIn }
    let textLength = (sampleCountry.CommonName.Length + 1) * 10
    let! pos = Anim.linear(5, 25, -textLength, Repeat.Loop)
    let! x = Anim.linear(4, 0, -25, Repeat.Loop)
    let! zoom = Anim.linear(20, 5, 0, Repeat.Loop)
    //let! width = Anim.linear(10, 1, 25, Repeat.Loop)
    //let! height = Anim.linear(10, 1, 25, Repeat.Loop)
    //let img = myLoadedImage.resize(int width.value, int height.value, false)
    
    //let! x = Anim.linear(3, -26, 26, Repeat.Loop)
    //image(myLoadedImage, int y.value, int (-y.value))
    // let i, x1, y =
    //     match int pos.value with
    //     //| 0 -> (img, 0, 0)
    //     //| 1 -> (img, -24, 0)
    //     //| 2 -> (img, 0, -24)
    //     //| 3 -> (img, -24, -24)
    //     | _ -> (imgFull, 0, 0)
    // let width, x = 
    //     match zoom.value with
    //     | v when v < 1.0 -> 24, 0
    //     | _ -> int (zoom.value * 24.0), int -(((zoom.value * 24.0) / 2.0) - 12.0)
    // let height = width
    let i = myLoadedImage.resize(24, 24, true)
    
    //printfn "%f %d" x.value y
    //image(i, x, x)
    
    //image(i, 0, 0)
    let duration = (durationOfState fullCycleDuration currentState.value)
    let! elapsed = Anim.linear(duration, 0, 1)
    let! opacity = Anim.linear(duration, 1.0, 0.0)
    match currentState.value with
    | FadeIn -> 
        rect.xywh(0, 0, 24, 24).fill(Colors.black.opacity opacity.value)
    | Countdown ->
        rect.xywh(0, 1, 1, 1).fill(Colors.green)
    | Transition ->
        rect.xywh(0, 2, 1, 1).fill(Colors.yellow)
    | Solution ->
        rect.xywh(0, 3, 1, 1).fill(Colors.blue)
    | FadeOut ->
        rect.xywh(0, 4, 1, 1).fill(Colors.orange)
    | _ -> 
        rect.xywh(0, 5, 1, 1).fill(Colors.red)

    if elapsed.isAtEnd then
        do currentState.value <- getNextState currentState.value
    else
        preserveState

    //
    // rect.xywh(0, 0, 24, 24).fill(Colors.black.opacity 0.3)
    //text.mono10x10($"{sampleCountry.CommonName}").xy(pos.valuei, 0).color(Colors.white)
    //text.mono10x10($"{sampleCountry.CommonName}").xy(0, 12).color(Colors.white)
    //shaderEffect textShader
} |> Simulator.start
