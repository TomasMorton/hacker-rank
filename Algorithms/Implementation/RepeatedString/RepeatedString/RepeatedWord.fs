namespace RepeatedString

module RepeatedWord =
    type RepeatedWord = { Word : string; }

    let private isValid repeatedWord =
        String.length repeatedWord.Word > 0

    let create word =
        let repeatedWord = { Word = word; }
        let valid = isValid repeatedWord
        match valid with
        | true -> Some repeatedWord
        | false -> None
