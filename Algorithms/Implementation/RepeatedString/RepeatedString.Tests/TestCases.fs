module RepeatedString.Tests.TestCases

open Xunit
open RepeatedString
open RepeatedWord
open TestCases

type RepeatedStringTestCase = { Word : RepeatedWord; CharacterToCount : char; ExpectedCount : int64 }

let mapTestCase characterToCount (testCase : TestCaseReader.TestCase) =
    let word = testCase.Input.[0]
    let repetitions = testCase.Input.[1] |> int64
    let repeatedWord = RepeatedWord.create repetitions word
    
    let expectedCount = testCase.ExpectedOutput.[0] |> int64
    repeatedWord
    |> Option.map (fun w ->
        { Word = w; CharacterToCount = characterToCount; ExpectedCount = expectedCount })
    
let executeTestCase mappedTestCase =
    printfn "Executing test case"
    Assert.True(Option.isSome mappedTestCase, "The input could not be read correctly")
    
    let testCase = mappedTestCase.Value
    let repeatedWord = testCase.Word
    
    printfn "Expected character count: %d" testCase.ExpectedCount
    printfn "Word: %s (%d times)" repeatedWord.Word repeatedWord.Repetitions
    
    let characterCount = CharacterCounter.countInstances testCase.CharacterToCount testCase.Word
    Assert.Equal(testCase.ExpectedCount, characterCount)
        
[<Fact>]
let ``Pass all test cases``() =
    let testCaseLocation = TestCaseFinder.findTestCases ()
    TestCaseReader.readTestCases testCaseLocation
    |> Seq.map (mapTestCase 'a')
    |> Seq.iter executeTestCase
