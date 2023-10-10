open System
open Hw5.Parser
open Hw5.Calculator

[<EntryPoint>]
let main argv =
    printfn "%A" argv
    let options = parseCalcArguments argv
    let result = match options with
    | Ok (arg1, operation, arg2) ->
        calculate arg1 operation arg2
    | Error err -> Double.NaN

    printf "result: %f" result
    0