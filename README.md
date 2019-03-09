# hacker-rank
[![Build Status](https://tmorton.visualstudio.com/hacker-rank/_apis/build/status/hacker-rank-CI?branchName=master)](https://tmorton.visualstudio.com/hacker-rank/_build/latest?definitionId=1&branchName=master)\
This repository contains my solutions to coding challenges from HackerRank.

## Challenges
Challenges can be found in the subfolders, such as the `Algorithms` folder.

## Utilities
There are also some utilities, to make it easier to work with the challenge data provided by HackerRank.

### TestCases
The `TestCases` utility is used to read test cases downloaded from HackerRank.\
It expects that there are two folders, `input` and `output`, inside the directory of the Challenge.\
Inside those folders, there should be test files labelled `inputXX.txt` or `outputXX.txt`.

1. Copy the `{challenge}-testcases` into your test project.
1. Set the `CopyToOutputDirectory.PreserveNewest` option on the input and output files.

## Implementations
The following technologies are currently in use:
* F#
* FAKE - F# Make
* .NET Core
* xUnit
* NuGet