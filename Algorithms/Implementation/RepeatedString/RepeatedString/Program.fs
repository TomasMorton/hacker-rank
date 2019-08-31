open System
open RepeatedString

[<EntryPoint>]
let main argv =
    let characterToCount = 'a'
    let word = Console.ReadLine()
    let repetitions = Console.ReadLine() |> int64
    
    let repeatedWord = RepeatedWord.create repetitions word
    match repeatedWord with
    | None -> failwithf "Unable to parse input: %s %d" word repetitions
    | Some word ->
        word
        |> CharacterCounter.countInstances characterToCount
        |> printf "%d"

    0
