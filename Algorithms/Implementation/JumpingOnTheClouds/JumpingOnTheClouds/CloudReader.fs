namespace JumpingOnTheClouds

module CloudReader =
    open CloudJumper

    let private mapCloud directionCharacter =
        match directionCharacter with
        | '0' -> Some Cloud.Ordinary
        | '1' -> Some Cloud.Thunder
        | _ -> None

    let createCloudPath input =
        let hasInvalidCloud = List.contains None

        let clouds =
            input
            |> List.ofSeq
            |> List.map mapCloud

        match clouds with
        | x when hasInvalidCloud x -> None
        | clouds ->
            clouds
            |> List.map Option.get
            |> fun clouds -> Some <| createPath clouds