#load "../../.deps/PxlLocalDevShadow.fsx"
open PxlLocalDevShadow

open System
open Pxl
open Pxl.Draw



// -------------------------------------------------------------



let finalScene =
    scene {
        let! ctx = getCtx()
        let now = ctx.now

        let c = Colors.white

        let angle =
            let secFrac = now.Millisecond
            let angleFor1s = (float (now.Second * 1000 + secFrac)) * 360.0 / 60000.0
            angleFor1s % 360.0

        // Calculate the maximum distance from the center to any corner (diagonal of the canvas)
        let maxDistance = Math.Sqrt(Math.Pow(ctx.halfWidth * 2.0, 2.0) + Math.Pow(ctx.halfHeight * 2.0, 2.0))
        let radians = angle * Math.PI / 180.0
        let endX = ctx.halfWidth + (maxDistance * Math.Cos(radians))
        let endY = ctx.halfHeight + (maxDistance * Math.Sin(radians))

        // Draw the line from the center to the edge
        let mkLine () = line.p1p2(ctx.halfWidth, ctx.halfHeight, endX, endY)
        for i in 1.0 .. 2.0 .. 10.0 do
            mkLine().stroke(c.opacity(1.0 - i / 10.0)).strokeThickness(i)

        // arc(
        //     -20, -20,
        //     ctx.width + 40,
        //     ctx.height + 40)
        //     .angle(angle)
        //     .fill(c)

        // a black circle
        let marginX = -0.5
        let factorY = 0.6
        oval
            .centerRad(
                ctx.halfWidth, ctx.halfHeight,
                ctx.halfWidth - marginX, ctx.halfHeight * factorY)
            .fill(Colors.black)
            .useAntiAlias()

        // a centered HH:mm
        let timeText = text.var3x5($"{DateTime.Now:HH}:{DateTime.Now:mm}").color(Colors.white)
        let textWidth = timeText.measure()
        let marginLeft = (ctx.width - textWidth) / 2
        let marginTop = (ctx.height - (int timeText._data.fontSize) - 1) / 2
        timeText.xy(marginLeft, marginTop)
    }


finalScene |> Simulator.start

(*
Eval.stop ()
*)
