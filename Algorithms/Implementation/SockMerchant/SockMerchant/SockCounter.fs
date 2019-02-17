namespace SockMerchant

module SockCounter =
    type Sock = Color of int

    let private groupSocksByColor socks =
        socks
        |> Seq.countBy (fun sock -> sock)

    let private countPairsInGroup totalSocks =
        totalSocks / 2

    let countPairs (socks : seq<Sock>) =
        socks
        |> groupSocksByColor
        |> Seq.map snd
        |> Seq.map countPairsInGroup
        |> Seq.sum
