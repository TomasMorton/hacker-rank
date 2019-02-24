module CountingValleys.Tests.TestCases

open Xunit
open CountingValleys
open TestCases


let mapTestCase (testCase : TestCaseReader.TestCase) =
    testCase
    
let executeTestCase mappedTestCase =
    printfn "Executing test case"
        
[<Fact>]
let ``Pass all test cases``() =
    let testCaseLocation = TestCaseFinder.findTestCases "CountingValleys"
    TestCaseReader.readTestCases "/Users/tomas/Documents/Development/hacker-rank/Algorithms/Implementation/CountingValleys/counting-valleys-testcases"
    |> Seq.map mapTestCase
    |> Seq.iter executeTestCase
