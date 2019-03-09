module CountingValleys.Tests.TestCases

open Xunit
open CountingValleys
open TestCases
open TestCases.TestCaseReader

type CountingValleysTestCase = { Hike: Hike.Hike; ExpectedNumberOfValleys : int }

let readDirections (testCase : TestCase) =
    testCase.Input.[1].Trim()
    |> List.ofSeq
            
let mapTestCase (testCase : TestCaseReader.TestCase) =
    let hike =
        readDirections testCase
        |> HikeReader.createHike
            
    let numberOfValleys = testCase.ExpectedOutput.[0] |> int
    
    hike
    |> Option.map (fun hike ->
        { Hike = hike; ExpectedNumberOfValleys = numberOfValleys })
    
let executeTestCase mappedTestCase =
    printfn "Executing test case"
    Assert.True(Option.isSome mappedTestCase, "The Hike could not be read correctly")
    
    let testCase = mappedTestCase.Value
    printfn "Expected number of valleys: %d" testCase.ExpectedNumberOfValleys
    printfn "Hike: %A" testCase.Hike
    
    let numberOfValleys = ValleyCounter.countValleys testCase.Hike
    Assert.Equal(testCase.ExpectedNumberOfValleys, numberOfValleys)
        
[<Fact>]
let ``Pass all test cases``() =
    let testCaseLocation = TestCaseFinder.findTestCases ()
    TestCaseReader.readTestCases testCaseLocation
    |> List.map mapTestCase
    |> List.iter executeTestCase
