namespace TestCases

open System.IO

module TestCaseReader =
    type TestCase = { Input : string list; ExpectedOutput : string list }
   
    //Test Case Files are in either the input or output folder of the Challenge
    module private FileFinder =
        
        let private readFile directory =
            File.ReadAllLines directory
            |> List.ofArray
        
        let private getAllFilesInDirectory directory =
            Directory.GetFiles(directory)
            |> List.ofArray
        
        let private readAllFilesInDirectory directory =
            getAllFilesInDirectory directory
            |> List.map (fun file -> (file, readFile file))
    
        let getTestInputs testCaseLocation =
            let directory = "input"
            let path = Path.Combine(testCaseLocation, directory)
            readAllFilesInDirectory path
        
        let getTestOutputs testCaseLocation =
            let directory = "output"
            let path = Path.Combine(testCaseLocation, directory)
            readAllFilesInDirectory path
    
    
    //Files are in the format inputXX.txt or outputXX.txt
    module private FileMapper =
        
        let private getFileName path =
            Path.GetFileNameWithoutExtension(path)
        
        let private removeFilePrefix (file:string) =
            file.Replace("input", "").Replace("output", "")
        
        let private getTestIndex file =
            file
            |> getFileName
            |> removeFilePrefix
            |> int
        
        let private getIndexedFile (file: string * string list) =
            let index = getTestIndex <| fst file
            let fileContents = snd file
            (index, fileContents)
        
        let private getFileMap files =
            files
            |> List.map getIndexedFile
            |> Map.ofSeq
                            
        let mapInputsToOutputs inputFiles outputFiles =
            let inputMap = getFileMap inputFiles
            let findInput index = inputMap.[index]
                
            outputFiles
            |> List.map getIndexedFile
            |> List.map (fun indexedOutput -> { Input = findInput (fst indexedOutput); ExpectedOutput = snd indexedOutput })
    
    let readTestCases testCaseLocation =
        let input = FileFinder.getTestInputs testCaseLocation
        let output = FileFinder.getTestOutputs testCaseLocation
    
        FileMapper.mapInputsToOutputs input output