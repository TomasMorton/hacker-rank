namespace JumpingOnTheClouds

module CloudJumper =
    type Cloud = | Ordinary | Thunder
    type Path = { Clouds : Cloud list }

    let createPath clouds =
        { Clouds = clouds }

    let private canJump cloud =
        match cloud with
        | Ordinary -> true
        | Thunder -> false


    let rec private jumpToEnd path =
        let takeDoubleJump remainingClouds =
            1 + jumpToEnd remainingClouds

        let takeSmallJump followingCloud remainingClouds =
            let followingAndRemainingClouds = followingCloud :: remainingClouds.Clouds |> createPath
            1 + jumpToEnd followingAndRemainingClouds

        let takeNextJump nextCloud followingCloud remainingClouds =
            let canMakeDoubleJump = canJump followingCloud
            match canMakeDoubleJump with
            | true -> takeDoubleJump remainingClouds
            | false -> takeSmallJump followingCloud remainingClouds

        match path.Clouds with
        | [] -> 0
        | nextCloud :: [] -> 1
        | nextCloud :: followingCloud :: tail -> takeNextJump nextCloud followingCloud <| createPath tail


    let getMinimumJumps path =
        let pathWithoutCurrentPosition =
            match path.Clouds with
            | [] -> path
            | currentPosition :: remainingPath -> createPath remainingPath

        jumpToEnd pathWithoutCurrentPosition

