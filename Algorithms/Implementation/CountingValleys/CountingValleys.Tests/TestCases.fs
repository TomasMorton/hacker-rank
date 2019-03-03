module CountingValleys.Tests.TestCases

open Xunit
open CountingValleys
open TestCases
open TestCases.TestCaseReader

type CountingValleysTestCase = { Hike: Hike.Hike; ExpectedNumberOfValleys : int }

let mapDirection directionCharacter =
    match directionCharacter with
    | 'U' -> Some Hike.ElevationChange.Incline
    | 'D' -> Some Hike.ElevationChange.Decline
    | _ -> None

let readDirections (testCase : TestCase) =
    testCase.Input.[1].Trim()
    |> Seq.map mapDirection
    
let createHike directions =
    match directions with
    | x when Seq.contains None x -> None
    | directions ->
        directions
        |> Seq.map Option.get
        |> Seq.map Hike.createStep
        |> Hike.createHike
        |> Some
            
let mapTestCase (testCase : TestCaseReader.TestCase) =
    let hike =
        readDirections testCase
        |> createHike
            
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
    let testCaseLocation = TestCaseFinder.findTestCases "CountingValleys"
    TestCaseReader.readTestCases "/Users/tomas/Documents/Development/hacker-rank/Algorithms/Implementation/CountingValleys/counting-valleys-testcases"
    |> Seq.map mapTestCase
    |> Seq.iter executeTestCase
