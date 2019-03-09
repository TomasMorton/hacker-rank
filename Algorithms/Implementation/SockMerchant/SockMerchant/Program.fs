open System
open SockMerchant

[<EntryPoint>]
let main args =
    let itemCount = Console.ReadLine()
                    |> int
    let input = Console.ReadLine().Trim().Split [| ' ' |]
                |> Array.map int

    input
    |> Array.map (fun color -> { SockCounter.Sock.Color = color })
    |> SockCounter.countPairs
    |> printf "%d"

    0
