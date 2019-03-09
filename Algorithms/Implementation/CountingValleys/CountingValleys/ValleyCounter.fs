namespace CountingValleys

module ValleyCounter =
    open Topography

    let private isValley topography = topography.Terrain = Terrain.Valley
    
    let private isNewTerrain topography =
        match (topography.Elevation) with
        | Elevation -1 | Elevation 0 | Elevation 1 -> true
        | _ -> false
        
    let countValleys hike =
        let topography = Region.createFromHike hike
        
        topography.Areas
        |> List.filter isValley
        |> List.length
