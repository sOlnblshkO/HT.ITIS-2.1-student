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
        |"+" -> CalculatorOperation.Plus
        |"-" -> CalculatorOperation.Minus
        |"*" -> CalculatorOperation.Multiply
        |"/" -> CalculatorOperation.Divide
        |_ -> CalculatorOperation.Undefined
    
let parseCalcArguments(args : string[]) =
    if (isArgLengthSupported args = false) then
        ArgumentException("length is not supported") |> raise
    elif (parseOperation args[1] = CalculatorOperation.Undefined) then
        InvalidOperationException("calculator operation is not supported") |> raise
    else
        let operation = parseOperation args[1]
        
        match Double.TryParse args[0] with
        |true, arg1 ->
            match Double.TryParse args[2] with
            |true, arg2 ->
                { arg1 = arg1; arg2 = arg2; operation = operation }
            |false, _ -> ArgumentException("second arg is not a number") |> raise
        |false, _ -> ArgumentException("thirst arg is not a number") |> raise