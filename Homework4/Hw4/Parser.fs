module Hw4.Parser

open System
open Hw4.Calculator


type CalcOptions = {
    arg1: float
    arg2: float
    operation: CalculatorOperation
}

let isArgLengthSupported (args : string[]) =
    args.Length = 3

let parseOperation (arg : string) =
    match arg with
    | "+" -> CalculatorOperation.Plus
    | "-" -> CalculatorOperation.Minus
    | "/" -> CalculatorOperation.Divide
    | "*" -> CalculatorOperation.Multiply
    | _ -> ArgumentException("Incorrect input") |> raise
    
let FromStringToDouble(arg : string) =
    match Double.TryParse(arg) with
    | (true, value) -> value
    | _ -> ArgumentException("Value is not correct") |> raise
    
let parseCalcArguments(args : string[]) =
    if not (isArgLengthSupported args) then
        ArgumentException("Incorrect input") |> raise
    let val1 = FromStringToDouble(args[0])
    let operation = parseOperation(args[1])
    let val2 = FromStringToDouble(args[2])  
    {arg1 = val1; operation = operation; arg2 = val2 };    
    

