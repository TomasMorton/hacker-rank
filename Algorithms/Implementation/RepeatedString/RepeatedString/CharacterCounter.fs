namespace RepeatedString

module CharacterCounter =
    open RepeatedWord

    let private countCharacter expectedChar actualChar =
        let isSame = expectedChar = actualChar
        if isSame then 1 else 0

    let countCharacterInWord character word =
        word
        |> Seq.sumBy (countCharacter character)
    
    let private wordAsStringWithLimit character limit repeatedWord =
        let countPerWord =
            repeatedWord.Word
            |> countCharacterInWord character
            
        let numberOfCompleteWords = limit / int64 repeatedWord.Word.Length
        let sumOfCompleteWords = numberOfCompleteWords * int64 countPerWord
        
        let remainder = limit % int64 repeatedWord.Word.Length |> int
        let sumOfRemainder =
            repeatedWord.Word
            |> Seq.take remainder
            |> countCharacterInWord character
        
        sumOfCompleteWords + int64 sumOfRemainder
        
    let countInstancesWithinLimit character (limit : int64) repeatedWord =
        wordAsStringWithLimit character limit repeatedWord
