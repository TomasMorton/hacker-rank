module Tests

open Xunit
open CountingValleys
open CountingValleys.Hike

let createHike elevationChanges =
    elevationChanges
    |> List.map Hike.createStep
    |> Hike.createHike

let createElevationChange inclineLength declineLength =
    let incline = List.replicate inclineLength ElevationChange.Incline
    let decline = List.replicate declineLength ElevationChange.Decline
    (incline, decline)

let createMountain elevation =
    let (incline, decline) = createElevationChange elevation elevation
    
    decline
    |> List.append incline

let createValley depth =
    let (incline, decline) = createElevationChange depth depth
    
    incline
    |> List.append decline

[<Fact>]
let ``Have no valleys when the Hike is empty``() =
    let emptyHike = createHike [ ]
    
    let numberOfValleys = ValleyCounter.countValleys emptyHike
    
    Assert.Equal(0, numberOfValleys)

[<Fact>]
let ``Have no valleys when the Hike is only a mountain``() =
    let mountainHike = createMountain 1 |> createHike
    
    let numberOfValleys = ValleyCounter.countValleys mountainHike
    
    Assert.Equal(0, numberOfValleys)

[<Fact>]
let ``Have one valley when the Hike is only a valley``() =
    let valleyHike = createValley 1 |> createHike
    
    let numberOfValleys = ValleyCounter.countValleys valleyHike
    
    Assert.Equal(1, numberOfValleys)
    
[<Fact>]
let ``Have one valley when the Hike is two valleys that don't return to sea level``() =
    let steps = [ ElevationChange.Decline; ElevationChange.Decline;
                  ElevationChange.Incline;
                  ElevationChange.Decline;
                  ElevationChange.Incline; ElevationChange.Incline ]
    let valleyHike = steps |> createHike
    
    let numberOfValleys = ValleyCounter.countValleys valleyHike
    
    Assert.Equal(1, numberOfValleys)
    
[<Fact>]
let ``Have two valleys when the Hike is valley - mountain - valley``() =
    let steps =
        createValley 1
        |> List.append <| createMountain 1
        |> List.append <| createValley 1
    let hike = steps |> createHike
    
    let numberOfValleys = ValleyCounter.countValleys hike
    
    Assert.Equal(2, numberOfValleys)