module Tests

open System
open Xunit
open RepeatedString

let characterToCount = 'a'

let countCharacter =
    CharacterCounter.countInstances characterToCount

let countCharacterWithinLimit limit =
    CharacterCounter.countInstancesWithinLimit characterToCount limit

let getWordWithoutCharacter () =
    "abcdefghijklmnopqrstuvwxyz"
    |> Seq.filter (fun c -> c <> characterToCount)
    |> Array.ofSeq
    |> String

let getWordWithCharacter () =
    let wordWithoutCharacter = getWordWithoutCharacter ()
    string characterToCount
    |> (+) wordWithoutCharacter

let createRepeatedWord repetitions word =
    let result = RepeatedWord.create repetitions word
    match result with
    | Some repeatedWord ->
        repeatedWord
    | None ->
        failwithf "Unable to create repeated word '%s' with %i repetitions" word repetitions

[<Fact>]
let ``Return 0 when word is empty`` () =
    let word = createRepeatedWord 5 String.Empty
    let count = countCharacter word
    Assert.Equal(0, count)
 
[<Fact>]   
let ``Return 0 when word doesn't contain character`` () =
    let word =
        getWordWithoutCharacter ()
        |> createRepeatedWord 5
    let count = countCharacter word
    Assert.Equal(0, count)
    
[<Fact>]
let ``Return 1 when word contains character and is not repeated`` () =
    let word =
        getWordWithCharacter ()
        |> createRepeatedWord 1
    let count = countCharacter word
    Assert.Equal(1, count)
    
[<Fact>]
let ``Return 2 when word contains character and is repeated once`` () =
    let word =
        getWordWithCharacter ()
        |> createRepeatedWord 2
    let count = countCharacter word
    Assert.Equal(2, count)
    
[<Fact>]
let ``Return 0 when limit is 0`` () =
    let limit = 0
    let word =
        getWordWithCharacter ()
        |> createRepeatedWord 2
    let count = countCharacterWithinLimit limit word
    Assert.Equal(0, count)
    
[<Fact>]
let ``Return 2 when word contains the character twice`` () =
    let word =
        sprintf "%c!%c" characterToCount characterToCount
        |> createRepeatedWord 1
    let count = countCharacter word
    Assert.Equal(2L, count)
  
[<Fact>]
let ``Handle very large repetitions`` () =
    let word =
        sprintf "%c" characterToCount
        |> createRepeatedWord Int64.MaxValue
    let count = countCharacter word
    Assert.Equal(Int64.MaxValue, count)