module Tests

open Xunit
open SockMerchant

type TestColor = Red = 1 | Blue = 2

let createSock (color : TestColor) =
    let colorNumber = int color
    { SockCounter.Sock.Color = colorNumber }


[<Fact>]
let ``Have no pairs when there are no socks``() =
    let socks = []

    let numberOfPairs = SockCounter.countPairs socks

    Assert.Equal(0, numberOfPairs)

[<Fact>]
let ``Have no pairs when there is one sock``() =
    let socks = [ createSock TestColor.Red ]

    let numberOfPairs = SockCounter.countPairs socks

    Assert.Equal(0, numberOfPairs)

[<Fact>]
let ``Have no pairs when the socks are different colors``() =
    let socks = [ createSock TestColor.Red; createSock TestColor.Blue ]

    let numberOfPairs = SockCounter.countPairs socks

    Assert.Equal(0, numberOfPairs)

[<Fact>]
let ``Have one pair when the socks are the same color``() =
    let socks = [ createSock TestColor.Red; createSock TestColor.Red ]

    let numberOfPairs = SockCounter.countPairs socks

    Assert.Equal(1, numberOfPairs)

[<Fact>]
let ``Have two pairs when there are 4 socks of the same color``() =
    let socks = Seq.replicate 4 <| createSock TestColor.Blue

    let numberOfPairs = SockCounter.countPairs socks

    Assert.Equal(2, numberOfPairs)
