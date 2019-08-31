module Tests

open System
open Xunit
open RepeatedString

let characterToCount = 'a'

let countCharacterWithinLimit limit =
    CharacterCounter.countInstancesWithinLimit characterToCount limit

let countCharacterInSingleWord (repeatedWord : RepeatedWord.RepeatedWord) =
    countCharacterWithinLimit (int64 repeatedWord.Word.Length) repeatedWord

let getWordWithoutCharacter () =
    "abcdefghijklmnopqrstuvwxyz"
    |> Seq.filter (fun c -> c <> characterToCount)
    |> Array.ofSeq
    |> String

let getWordWithCharacter () =
    let wordWithoutCharacter = getWordWithoutCharacter ()
    string characterToCount
    |> (+) wordWithoutCharacter

let createRepeatedWord word =
    let result = RepeatedWord.create word
    match result with
    | Some repeatedWord ->
        repeatedWord
    | None ->
        failwithf "Unable to create repeated word '%s'" word

[<Fact>]
let ``Fail when word is empty`` () =
    let word = RepeatedWord.create String.Empty
    Assert.Equal(None, word)

[<Fact>]   
let ``Return 0 when word doesn't contain character`` () =
    let word =
        getWordWithoutCharacter ()
        |> createRepeatedWord
    let count = countCharacterInSingleWord word
    Assert.Equal(0L, count)
    
[<Fact>]
let ``Return 1 when word contains character and is not repeated`` () =
    let word =
        getWordWithCharacter ()
        |> createRepeatedWord
    let count = countCharacterInSingleWord word
    Assert.Equal(1L, count)
    
[<Fact>]
let ``Return 2 when word contains character and is repeated once`` () =
    let word =
        getWordWithCharacter ()
        |> createRepeatedWord
    let length = int64 word.Word.Length
    let count = countCharacterWithinLimit (length * 2L) word
    Assert.Equal(2L, count)
    
[<Fact>]
let ``Return 0 when limit is 0`` () =
    let limit = 0L
    let word =
        getWordWithCharacter ()
        |> createRepeatedWord
    let count = countCharacterWithinLimit limit word
    Assert.Equal(0L, count)
    
[<Fact>]
let ``Return 2 when word contains the character twice`` () =
    let word =
        sprintf "%c!%c" characterToCount characterToCount
        |> createRepeatedWord
    let count = countCharacterInSingleWord word
    Assert.Equal(2L, count)
  
[<Fact>]
let ``Handle very large repetitions`` () =
    let word =
        sprintf "a%ca" characterToCount
        |> createRepeatedWord
    let count = countCharacterWithinLimit Int64.MaxValue word
    Assert.Equal(Int64.MaxValue, count)