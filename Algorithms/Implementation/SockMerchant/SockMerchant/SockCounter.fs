namespace SockMerchant
    
module SockCounter =
    type Sock = | Color of int
    
    let countPairs (socks: seq<Sock>) =
        socks |> Seq.length