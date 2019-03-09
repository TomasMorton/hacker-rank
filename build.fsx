#r "paket:
nuget Fake.DotNet.Cli
nuget Fake.IO.FileSystem
nuget Fake.Core.Target //"
#load ".fake/build.fsx/intellisense.fsx"
open Fake.Core
open Fake.DotNet
open Fake.IO
open Fake.IO.FileSystemOperators
open Fake.IO.Globbing.Operators
open Fake.Core.TargetOperators

module Solution =
    let path = "HackerRank.sln"

module Build =
    let configuration = DotNet.BuildConfiguration.Release

Target.create "Clean" (fun _ ->
    !! "**/bin"
    ++ "**/obj"
    |> Shell.cleanDirs
)

Target.create "Restore" (fun _ ->
    DotNet.restore id Solution.path
)

Target.create "Build" (fun _ ->
    let configureBuild (buildOptions : DotNet.BuildOptions) =
        { buildOptions with
            Configuration = Build.configuration
            NoRestore = true
        }
    DotNet.build configureBuild Solution.path
)

Target.create "Test" (fun _ ->
    let configureTest (testOptions : DotNet.TestOptions) =
        { testOptions with
            Configuration = Build.configuration
            NoRestore = true
            NoBuild = true
            Logger = Some "console;verbosity=normal"
        }

    !! "**/*Tests.fsproj"
    |> Seq.iter (DotNet.test configureTest)
)

Target.create "All" ignore

"Clean"
  ==> "Restore"
  ==> "Build"
  ==> "Test"
  ==> "All"

Target.runOrDefault "All"
