open System
open Hw4
[<EntryPoint>]
let start args =
    let calcOptions = Parser.parseCalcArguments args
    printfn "%f" (Calculator.calculate calcOptions.arg1 calcOptions.operation calcOptions.arg2)
    0
 