namespace RepeatedString

module CharacterCounter =
    open System
    open RepeatedWord

    let private countCharacter expectedChar actualChar =
        let isSame = expectedChar = actualChar
        if isSame then 1 else 0

    let private wordAsStringWithLimit limit repeatedWord =
        Seq.replicate repeatedWord.Repetitions repeatedWord.Word
        |> Seq.truncate limit
        |> String.concat ""
        
    let countInstancesWithinLimit character limit repeatedWord =
        wordAsStringWithLimit limit repeatedWord
        |> Seq.sumBy (countCharacter character)
        
    let countInstances character repeatedWord =
        countInstancesWithinLimit character Int32.MaxValue repeatedWord
