module Hw4.Parser

open System
open Hw4.Calculator


type CalcOptions = {
    arg1: float
    arg2: float
    operation: CalculatorOperation
}
  
let isArgLengthSupported (args : string[]) =
    if args.Length = 3 then true
    else false     

let parseOperation (arg : string) =
        match arg with
        | "+" -> CalculatorOperation.Plus
        | "-" -> CalculatorOperation.Minus
        | "*" -> CalculatorOperation.Multiply
        | "/" -> CalculatorOperation.Divide
        | _ -> ArgumentException() |> raise
   
let parseCalcArguments(args : string[]) =
    let mutable value1 = 0.0
    let mutable value2 = 0.0
    if isArgLengthSupported(args) && Double.TryParse(args[0], &value1) && Double.TryParse(args[2], &value2) then {
        arg1 = value1
        arg2 = value2
        operation = parseOperation(args[1])
        }
    else ArgumentException() |> raise