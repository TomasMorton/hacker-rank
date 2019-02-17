module SockMerchant.Tests.TestCases

open Xunit
open SockMerchant
open TestCases

type SockMerchantTestCase = { Socks : seq<SockMerchant.SockCounter.Sock>; ExpectedNumberOfPairs : int }

let mapTestCase (testCase : TestCaseReader.TestCase) =
        let socks = testCase.Input.[1].Trim().Split([|' '|])
                    |> Seq.map (function color -> { SockCounter.Sock.Color = int color })
        let numberOfPairs = testCase.ExpectedOutput.[0] |> int     
        { Socks = socks; ExpectedNumberOfPairs = numberOfPairs }
    
let executeTestCase mappedTestCase =
    printfn "Executing test case"
    printfn "Expect number of pairs: %d" mappedTestCase.ExpectedNumberOfPairs
    printfn "Socks: %s" <| System.String.Join(", ", mappedTestCase.Socks)
    
    let numberOfPairs = SockCounter.countPairs mappedTestCase.Socks
    Assert.Equal(mappedTestCase.ExpectedNumberOfPairs, numberOfPairs)
        
[<Fact>]
let ``Pass all test cases``() =
    let testCaseLocation = TestCaseFinder.findTestCases "SockMerchant"
    TestCaseReader.readTestCases "/Users/tomas/Documents/Development/hacker-rank/Algorithms/Implementation/SockMerchant/sock-merchant-testcases"
    |> Seq.map mapTestCase
    |> Seq.iter executeTestCase
