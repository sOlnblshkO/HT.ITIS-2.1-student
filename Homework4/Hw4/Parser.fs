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
    | "*" -> CalculatorOperation.Multiply
    | "/" -> CalculatorOperation.Divide
    | _ -> ArgumentException() |> raise
    
let parseCalcArguments(args : string[]) =
    if not (isArgLengthSupported args) then
        ArgumentException() |> raise
    {
        arg1 = match Double.TryParse(args[0]) with
               | (true, value) -> value
               | _ -> ArgumentException() |> raise
        operation = parseOperation(args[1])
        arg2 = match Double.TryParse(args[2]) with
               | (true, value) -> value
               | _ -> ArgumentException() |> raise
    };
