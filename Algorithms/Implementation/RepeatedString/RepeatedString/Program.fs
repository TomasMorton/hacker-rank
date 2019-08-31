open System
open RepeatedString

[<EntryPoint>]
let main argv =
    let characterToCount = 'a'
    let word = Console.ReadLine()
    let limit = Console.ReadLine() |> int64
    
    let repeatedWord = RepeatedWord.create word
    match repeatedWord with
    | None -> failwithf "Unable to parse input: %s %d" word limit
    | Some word ->
        word
        |> CharacterCounter.countInstancesWithinLimit characterToCount limit
        |> printf "%d"

    0
