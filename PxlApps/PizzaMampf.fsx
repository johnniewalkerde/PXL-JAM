#load "./Helper/PxlLocalDevShadow.fsx"
open PxlLocalDevShadow

open System
open Pxl
open Pxl.Draw
open Pxl.Draw.Fsi

let createCanvas () = CanvasProxy.create CanvasProxy.Channel.Tcp "localhost"


// -------------------------------------------------------------



let pizzaSpriteMap =
    Image.load(PxlLocalDev.loadAsset "pizzaMampf.png")
        .crop(456, 0, 0, 0)
        .makeSpriteMap(16, 16, 50)

let pacmanLR = pizzaSpriteMap.animate [ 0,0; 0,1; 0,2; 0,1 ]
let pacmanRL = pizzaSpriteMap.animate [ 1,0; 1,1; 0,2; 1,1 ]

let ghostHunterRedLR = pizzaSpriteMap.animate [ 4,0; 4,1 ]
let ghostHunterRedRL = pizzaSpriteMap.animate [ 4,2; 4,3 ]

let ghostHunterPinkLR = pizzaSpriteMap.animate [ 5,0; 5,1 ]
let ghostHunterPinkRL = pizzaSpriteMap.animate [ 5,2; 5,3 ]

let ghostFrightenedRed = pizzaSpriteMap.animate [ 4,8; 4,9 ]
let ghostFrightenedPink = pizzaSpriteMap.animate [ 4,10; 4,11 ]

let pacGhostGhostLR offset =
    scene {
        let offset(col, space) = col * 20.0 + offset + space
        let y = 9

        image(pacmanLR, offset(0, 0), y)
        image(ghostHunterRedLR, offset(-1, -10), y-1)
        image(ghostHunterPinkLR, offset(-2, -10), y-1)
    }

let finalScene =
    scene {
        let! ctx = getCtx()
        let! x = Anim.linear(4.0, -16, 80., repeat = Repeat.Loop)
        pacGhostGhostLR x.value
        line.p1p2(0, ctx.height, ctx.width, ctx.height).stroke(Colors.green)
    }


finalScene |> Eval.start createCanvas


(*
Eval.stop ()
*)
