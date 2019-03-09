namespace CountingValleys

module HikeReader =
    
    let private mapDirection directionCharacter =
        match directionCharacter with
        | 'U' -> Some Hike.ElevationChange.Incline
        | 'D' -> Some Hike.ElevationChange.Decline
        | _ -> None
        
    let createHike directionString =
        let hasInvalidDirection = List.contains None
        
        let directions =
            directionString
            |> List.ofSeq
            |> List.map mapDirection
        
        match directions with
        | x when hasInvalidDirection x -> None
        | directions ->
            directions
            |> List.map Option.get
            |> List.map Hike.createStep
            |> Hike.createHike
            |> Some