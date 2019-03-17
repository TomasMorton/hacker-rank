namespace RepeatedString

module RepeatedWord =
    type RepeatedWord = { Word : string; Repetitions : int }

    let repititionsAbove0 repititions =
        repititions > 0

    let isValid repeatedWord =
        repititionsAbove0 repeatedWord.Repetitions

    let create repetitions word =
        let repeatedWord = { Word = word; Repetitions = repetitions }
        let valid = isValid repeatedWord
        match valid with
        | true -> Some repeatedWord
        | false -> None
