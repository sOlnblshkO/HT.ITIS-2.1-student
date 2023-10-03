open Hw4
open Hw4.Parser
open Hw4.Calculator

[<EntryPoint>]
let main args =
    try
        let calcOptions = parseCalcArguments args
        let result = (calculate calcOptions.arg1 calcOptions.operation calcOptions.arg2)
        printfn $"%f{result}"
    with e ->
        printfn $"%s{e.Message}"
    0