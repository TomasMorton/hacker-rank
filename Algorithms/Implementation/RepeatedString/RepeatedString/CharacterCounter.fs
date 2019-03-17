namespace RepeatedString

module CharacterCounter =
    open RepeatedWord

    let private countCharacter expectedChar actualChar =
        let isSame = expectedChar = actualChar
        if isSame then 1 else 0

    let countInstances character repeatedWord =
        let word =
            List.replicate repeatedWord.Repetitions repeatedWord.Word
            |> String.concat ""

        word
        |> Seq.sumBy (countCharacter character)
