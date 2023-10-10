open System
open Hw5.Parser
open Hw5.Calculator

[<EntryPoint>]
let main argv =
    printfn "%A" argv
    let options = parseCalcArguments argv
    
    0