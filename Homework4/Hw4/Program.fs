open Hw4

[<EntryPoint>]
let main args =
    try
        let calcOptions = Parser.parseCalcArguments args
        printfn "%f" (Calculator.calculate calcOptions.arg1 calcOptions.operation calcOptions.arg2)
    with e ->
        printfn "%s" e.Message

    0
