#load "../../.deps/fsi/PxlLocalDev.fsx"

open System.IO

module PxlLocalDev =
    let loadAsset (assetPath: string) =
        let path = Path.Combine(__SOURCE_DIRECTORY__, "../assets", assetPath)
        let content = File.ReadAllBytes(path)
        new MemoryStream(content)
