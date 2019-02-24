open System
open CountingValleys

[<EntryPoint>]
let main args =
    let itemCount = Console.ReadLine()
                    |> int
    let input = Console.ReadLine().Trim().Split [| ' ' |]
                |> Array.map int

    0