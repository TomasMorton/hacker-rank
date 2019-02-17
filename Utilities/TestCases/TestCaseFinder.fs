namespace TestCases

module TestCaseFinder =

    open System
    open System.IO

    let private findChallengeFolder fromFolder challengeName =
        let mutable resultFolder = new DirectoryInfo(fromFolder)
        
        //TODO: This will crash if the directory isn't found
        while resultFolder.Name <> challengeName
            do resultFolder <- resultFolder.Parent

        resultFolder.FullName
        
    let private findTestCaseFolder challengeFolder =
        Directory.GetDirectories challengeFolder
        |> Seq.find (function directory -> directory.Contains("testcases"))

    let findTestCases challengeName =
        let currentDirectory = Environment.CurrentDirectory
        
        findChallengeFolder currentDirectory challengeName
        |> findTestCaseFolder
