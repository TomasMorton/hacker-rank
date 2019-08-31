module RepeatedString.Tests.TestCases

open Xunit
open RepeatedString
open RepeatedWord
open TestCases

type RepeatedStringTestCase = { Word : RepeatedWord; Limit: int64; CharacterToCount : char; ExpectedCount : int64 }

let mapTestCase characterToCount (testCase : TestCaseReader.TestCase) =
    let word = testCase.Input.[0] |> RepeatedWord.create
    let limit = testCase.Input.[1] |> int64
    
    let expectedCount = testCase.ExpectedOutput.[0] |> int64
    word
    |> Option.map (fun w ->
        { Word = w; Limit = limit; CharacterToCount = characterToCount; ExpectedCount = expectedCount })
    
let executeTestCase mappedTestCase =
    printfn "Executing test case"
    Assert.True(Option.isSome mappedTestCase, "The input could not be read correctly")
    
    let testCase = mappedTestCase.Value
    let repeatedWord = testCase.Word
    
    printfn "Expected character count: %d" testCase.ExpectedCount
    printfn "Word: %s (Length of %d)" repeatedWord.Word testCase.Limit
    
    let characterCount = CharacterCounter.countInstancesWithinLimit testCase.CharacterToCount testCase.Limit testCase.Word
    Assert.Equal(testCase.ExpectedCount, characterCount)
        
[<Fact>]
let ``Pass all test cases``() =
    let testCaseLocation = TestCaseFinder.findTestCases ()
    TestCaseReader.readTestCases testCaseLocation
    |> Seq.map (mapTestCase 'a')
    |> Seq.iter executeTestCase
