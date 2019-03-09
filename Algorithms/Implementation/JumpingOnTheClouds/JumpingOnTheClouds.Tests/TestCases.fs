module JumpingOnTheClouds.Tests.TestCases

open System
open Xunit
open JumpingOnTheClouds
open TestCases
open TestCases.TestCaseReader

type JumpingOnTheCloudsTestCase = { Path : CloudJumper.Path; ExpectedNumberOfJumps : int }

let readClouds (testCase : TestCase) =
    testCase.Input.[1].Replace(" ", String.Empty)
    |> List.ofSeq

let mapTestCase (testCase : TestCaseReader.TestCase) =
    let path =
        readClouds testCase
        |> CloudReader.createCloudPath

    let numberOfJumps = testCase.ExpectedOutput.[0] |> int

    path
    |> Option.map (fun path ->
        { Path = path; ExpectedNumberOfJumps = numberOfJumps })

let executeTestCase mappedTestCase =
    printfn "Executing test case"
    Assert.True(Option.isSome mappedTestCase, "The Clouds could not be read correctly")

    let testCase = mappedTestCase.Value
    printfn "Expected number of jumps: %d" testCase.ExpectedNumberOfJumps
    printfn "Path: %A" testCase.Path

    let numberOfJumps = CloudJumper.getMinimumJumps testCase.Path
    Assert.Equal(testCase.ExpectedNumberOfJumps, numberOfJumps)

[<Fact>]
let ``Pass all test cases``() =
    let testCaseLocation = TestCaseFinder.findTestCases ()
    TestCaseReader.readTestCases testCaseLocation
    |> List.map mapTestCase
    |> List.iter executeTestCase
