namespace TestCases

module TestCaseFinder =

    open System
    open System.IO

    let private findTestCaseFolder challengeFolder =
        Directory.GetDirectories challengeFolder
        |> Seq.find (fun directory -> directory.Contains("testcases"))

    let findTestCases () =
        let currentDirectory = Environment.CurrentDirectory
        findTestCaseFolder currentDirectory
