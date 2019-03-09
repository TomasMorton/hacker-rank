module Tests

open JumpingOnTheClouds
open JumpingOnTheClouds.CloudJumper
open Xunit

[<Fact>]
let ``Return no jumps when there is no path`` () =
    let path = CloudJumper.createPath []
    
    let numberOfJumps = CloudJumper.getMinimumJumps path
    
    Assert.Equal(0, numberOfJumps)
    
[<Fact>]
let ``Return no jumps when there is a single cloud`` () =
    let path = CloudJumper.createPath [ Cloud.Ordinary ]
    
    let numberOfJumps = CloudJumper.getMinimumJumps path
    
    Assert.Equal(0, numberOfJumps)

[<Fact>]
let ``Return one jump when there are two clouds`` () =
    let path = CloudJumper.createPath [ Cloud.Ordinary; Cloud.Ordinary ]
    
    let numberOfJumps = CloudJumper.getMinimumJumps path
    
    Assert.Equal(1, numberOfJumps)
    
[<Fact>]
let ``Return one jump when there are three clouds`` () =
    let path = CloudJumper.createPath [ Cloud.Ordinary; Cloud.Ordinary; Cloud.Ordinary ]
    
    let numberOfJumps = CloudJumper.getMinimumJumps path
    
    Assert.Equal(1, numberOfJumps)
    
[<Fact>]
let ``Return two jumps when there are five clouds`` () =
    let path = CloudJumper.createPath [ Cloud.Ordinary; Cloud.Ordinary; Cloud.Ordinary; Cloud.Ordinary; Cloud.Ordinary ]
    
    let numberOfJumps = CloudJumper.getMinimumJumps path
    
    Assert.Equal(2, numberOfJumps)
    
[<Fact>]
let ``Return three jumps when there are four clouds and a thunder cloud`` () =
    let path = CloudJumper.createPath [ Cloud.Ordinary; Cloud.Ordinary; Cloud.Thunder; Cloud.Ordinary; Cloud.Ordinary  ]
    
    let numberOfJumps = CloudJumper.getMinimumJumps path
    
    Assert.Equal(3, numberOfJumps)