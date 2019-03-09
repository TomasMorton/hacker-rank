open JumpingOnTheClouds
open System

[<EntryPoint>]
let main args =
    let itemCount = Console.ReadLine()
                    |> int
    let input = Console.ReadLine().Replace(" ", String.Empty)
    
    let cloudPath = CloudReader.createCloudPath input
    
    match cloudPath with
    | None -> failwithf "Unable to parse input clouds: %s" input
    | Some cloudPath ->
        cloudPath
        |> CloudJumper.getMinimumJumps
        |> printf "%d"
    
    0