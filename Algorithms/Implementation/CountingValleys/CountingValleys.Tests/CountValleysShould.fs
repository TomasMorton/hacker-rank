module Tests

open Xunit
open CountingValleys
open CountingValleys.Hike

let createHike elevationChanges =
    elevationChanges
    |> Seq.map Hike.createStep
    |> Hike.createHike

let createElevationChange inclineLength declineLength =
    let incline = Seq.replicate inclineLength ElevationChange.Incline
    let decline = Seq.replicate declineLength ElevationChange.Decline
    (incline, decline)

let createMountain elevation =
    let (incline, decline) = createElevationChange elevation elevation
    
    incline
    |> Seq.append decline

let createValley depth =
    let (incline, decline) = createElevationChange depth depth
    
    decline
    |> Seq.append incline

[<Fact>]
let ``Have no valleys when the Hike is empty``() =
    let emptyHike = createHike [|  |]
    
    let numberOfValleys = ValleyCounter.countValleys emptyHike
    
    Assert.Equal(0, numberOfValleys)

[<Fact>]
let ``Have no valleys when the Hike is only a mountain``() =
    let mountainHike = createHike <| createMountain 1
    
    let numberOfValleys = ValleyCounter.countValleys mountainHike
    
    Assert.Equal(0, numberOfValleys)

[<Fact>]
let ``Have one valley when the Hike is only a valley``() =
    let valleyHike = createHike <| createValley 1
    
    let numberOfValleys = ValleyCounter.countValleys valleyHike
    
    Assert.Equal(1, numberOfValleys)