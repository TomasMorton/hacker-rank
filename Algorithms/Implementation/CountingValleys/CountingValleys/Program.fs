open System
open CountingValleys

[<EntryPoint>]
let main args =
    let itemCount = Console.ReadLine()
                    |> int
    let input = Console.ReadLine().Trim()

    let hike =
        input
        |> HikeReader.createHike
    
    match hike with
    | None -> failwithf "Unable to parse input hike: %s" input
    | Some hike ->
        hike
        |> ValleyCounter.countValleys
        |> printf "%d"
    
    0