open System
open Hw4
[<EntryPoint>]
let start args =
    try
        let calcOptions = Parser.parseCalcArguments args
        printfn "%f" (Calculator.calculate calcOptions.arg1 calcOptions.operation calcOptions.arg2)
    with
    |e -> e |> raise
    
    0
 