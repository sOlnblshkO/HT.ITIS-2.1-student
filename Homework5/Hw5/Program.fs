open System
open Hw5
open Hw5.Parser
open Hw5.Calculator

let args = [|"10"; "*"; "5"|]
let parsedArguments = parseCalcArguments args
let resStr = match parsedArguments with
             | Ok success ->
                 let arg1, operation, arg2 = success
                 printfn $"Result: {calculate arg1 operation arg2}"
             | Error error ->
                  printfn $"Something went wrong: {error}"
                 
