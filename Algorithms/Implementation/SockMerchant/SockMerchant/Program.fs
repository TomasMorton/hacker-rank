open System
open SockMerchant

[<EntryPoint>]
let main args =
    let itemCount = Console.ReadLine()
                    |> int
    let input = Console.ReadLine().Trim().Split [| ' ' |]
                |> Array.map int

    input
    |> Array.map (function color -> { SockCounter.Sock.Color = color })
    |> SockCounter.countPairs
    |> printf "%d"

    0
