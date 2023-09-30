module Hw4.Parser

open System
open Hw4.Calculator

type CalcOptions = {
    arg1: float
    arg2: float
    operation: CalculatorOperation
}

let isArgLengthSupported (args : string[]) =
    (args.Length = 3)
    
    
let parseOperation (arg : string) =
    match arg with
    | "+" -> CalculatorOperation.Plus
    | "-" -> CalculatorOperation.Minus
    | "*" -> CalculatorOperation.Multiply
    | "/" -> CalculatorOperation.Divide
    | _  -> ArgumentException($"Unaviable operation: {arg}") |> raise
    
    
let parseCalcArguments(args : string[]) =
    let mutable val1 = 0.0
    let mutable val2 = 0.0
    if (not(isArgLengthSupported(args)) || not(Double.TryParse(args[0], &val1) && Double.TryParse(args[2], &val2)))
    then ArgumentException("Inappropriate array format") |> raise
    { arg1 = val1; arg2 = val2; operation = parseOperation(args[1]) }
