namespace CountingValleys

///
/// A Region contains one or more Topographies.
/// A Topography is a land feature, such as a Mountain or Valley.
/// A Topography has an Elevation, which is the highest / lowest point.
///

module Topography =
    
    type Elevation = Elevation of int
    type Terrain = Mountain | Valley | Plain

    type Topography = { Terrain : Terrain; Elevation : Elevation }
    
    let private seaLevel = Elevation 0
    let plain = { Terrain = Plain; Elevation = seaLevel }

    let createTopography elevation =
        match elevation with
        | x when x < seaLevel -> { Terrain = Valley; Elevation = x }
        | x when x > seaLevel -> { Terrain = Mountain; Elevation = x }
        | seaLevel -> { Terrain = Plain; Elevation = seaLevel }
    
    let getLargestTopography topography1 topography2 =
        let isFirstLarger =
            match topography1.Terrain with
            | Terrain.Mountain -> (>)
            | Terrain.Valley -> (<)
            | Terrain.Plain -> (=)
            
        if isFirstLarger topography1 topography2 then topography1 else topography2

module TopographyBuilder =
    open Topography
    open Hike
    
    type TopographyBuilder = { Areas : List<Topography>; CurrentElevation : int }
    
    let getCurrentArea builder =
        match builder.Areas with
        | currentArea :: tail -> currentArea
        | [] -> Topography.plain
        
    let getCurrentElevation builder =
        builder.CurrentElevation
        
    let getCurrentTerrain builder =
        let currentArea = getCurrentArea builder
        currentArea.Terrain
        
    let getAreas builder =
        match builder.Areas with
        | [] -> [ Topography.plain ]
        | _ -> builder.Areas
        
    let getCurrentAndRemainingAreas builder =
        let areas = getAreas builder
        match areas with
        | (previousTopography :: remainingAreas) -> (previousTopography, remainingAreas)
        | [] -> (Topography.plain, [])
    
    let private addNewArea builder newTopography =
        newTopography :: builder.Areas
        
    let private updateExistingArea builder newTopography =
        let (previousTopography, remainingAreas) = getCurrentAndRemainingAreas builder
        let largestTerrain = Topography.getLargestTopography previousTopography newTopography
        
        largestTerrain :: remainingAreas
        
    let private calculateNewElevation builder elevationChange =
        let elevationChangeInt =
            match elevationChange with
            | ElevationChange.Incline -> +1;
            | ElevationChange.Decline -> -1;
        
        elevationChangeInt + (getCurrentElevation builder)
            
    let withElevationChange builder elevationChange =
        let previousTerrain = getCurrentTerrain builder
            
        let newElevation = calculateNewElevation builder elevationChange
        let newTopography = createTopography <| Elevation newElevation
        
        let newAreas =
            match previousTerrain with
            | terrain when terrain = newTopography.Terrain -> updateExistingArea builder newTopography
            | _ -> addNewArea builder newTopography
        
        { Areas = newAreas; CurrentElevation = newElevation }

module Region =
    open Hike
    open Topography
    open TopographyBuilder

    type Region = { Areas : List<Topography> }
    
    let createFromHike (hike : Hike.Hike) =
        let plain = Topography.plain
        let (Elevation plainElevation) = plain.Elevation
        let builder = { Areas = [ plain ]; CurrentElevation = plainElevation }
        
        hike.Steps
        |> List.map (fun step -> step.Direction)
        |> List.fold (fun builder elevationChange ->
            withElevationChange builder elevationChange) builder