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
        |_ -> InvalidOperationException("calculator operation is not supported") |> raise
    
let tryParseDouble (str:string) =
    match Double.TryParse str with
    | true, value -> value
    | false, _    -> ArgumentException("arg is not a number") |> raise
let parseCalcArguments(args : string[]) =
    if (isArgLengthSupported args = false) then
        ArgumentException("length is not supported") |> raise
    else
        let operation = parseOperation args[1]
        
        let arg1 = tryParseDouble args.[0]
        let arg2 = tryParseDouble args.[2]
        
        { arg1 = arg1; arg2 = arg2; operation = operation }     