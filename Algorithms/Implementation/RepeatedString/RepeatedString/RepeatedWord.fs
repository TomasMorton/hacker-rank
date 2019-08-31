namespace RepeatedString

module RepeatedWord =
    type RepeatedWord = { Word : string; Repetitions : int64 }

    let private repetitionsAbove0 repetitions =
        repetitions > 0L

    let private isValid repeatedWord =
        repetitionsAbove0 repeatedWord.Repetitions

    let create repetitions word =
        let repeatedWord = { Word = word; Repetitions = repetitions }
        let valid = isValid repeatedWord
        match valid with
        | true -> Some repeatedWord
        | false -> None
