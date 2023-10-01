open Hw4

let main args =
    try
        let calcOperation = Parser.parseCalcArguments args
        let result = (Calculator.calculate calcOperation.arg1 calcOperation.operation calcOperation.arg2)
        printfn "%f" (result)
    with ex ->
        printfn "%s" ex.Message
    0