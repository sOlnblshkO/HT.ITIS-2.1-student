open Hw4
open System

[<EntryPoint>]
let main argv =
    printfn "%A" argv
    let options = Parser.parseCalcArguments argv
    let result = Calculator.calculate options.arg1 options.operation options.arg2
    printfn "result: %f" result
    0
    