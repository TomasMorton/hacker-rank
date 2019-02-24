namespace CountingValleys

module Hike =

    type ElevationChange = Incline | Decline
    type Step = { Direction : ElevationChange }
    type Hike = { Steps : seq<Step> }

    let createStep direction =
        { Direction = direction }

    let createHike steps =
        { Steps = steps }
