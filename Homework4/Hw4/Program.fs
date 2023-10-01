open Hw4

let main args =
    try
        let calcOptions = Parser.parseCalcArguments args
        let result = (Calculator.calculate calcOptions.arg1 calcOptions.operation calcOptions.arg2)
        printfn "%f" (result)
    with ex ->
        printfn "%s" ex.Message
    0