namespace TestCases

open System.IO

module TestCaseReader =
    type TestCase = { Input : string []; ExpectedOutput : string [] }
   
    //Test Case Files are in either the input or output folder of the Challenge
    module private FileFinder =
        
        let private readFile directory =
            File.ReadAllLines directory
        
        let private getAllFilesInDirectory directory =
            Directory.GetFiles(directory)
        
        let private readAllFilesInDirectory directory =
            getAllFilesInDirectory directory
            |> Seq.map (function file -> (file, readFile file))
    
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
        
        let private removeFileSuffix (file:string) =
            file.Replace(".txt", "")
        
        let private getTestIndex file =
            file
            |> getFileName
            |> removeFilePrefix
            |> removeFileSuffix
            |> int
        
        let private getIndexedFile (file: string * string[]) =
            let index = getTestIndex <| fst file
            let fileContents = snd file
            (index, fileContents)
        
        let private getFileMap files =
            files
            |> Seq.map getIndexedFile
            |> Map.ofSeq
                            
        let mapInputsToOutputs inputFiles outputFiles =
            let inputMap = getFileMap inputFiles
            let findInput index = inputMap.[index]
                
            outputFiles
            |> Seq.map getIndexedFile
            |> Seq.map (function indexedOutput -> { Input = findInput (fst indexedOutput); ExpectedOutput = snd indexedOutput })
    
    let readTestCases testCaseLocation =
        let input = FileFinder.getTestInputs testCaseLocation
        let output = FileFinder.getTestOutputs testCaseLocation
    
        FileMapper.mapInputsToOutputs input output