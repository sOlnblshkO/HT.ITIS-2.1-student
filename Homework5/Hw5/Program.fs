open System

open System
open Hw5
open Hw5.Parser
open Hw5.Calculator
open  Hw5.MaybeBuilder
let args = [|"15,6"; "+"; "5,6"|]

let objectResult = 
    maybe {
        let! parsedArgs = parseCalcArguments args
        match parsedArgs with
        (arg1, operation, arg2) ->
            let result = calculate arg1 operation arg2
            return result
    }

printfn "Result is %A" objectResult