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
    
let parseNumber(arg: string) =
    let mutable result: double = 0
    match Double.TryParse(arg,  &result) with 
    | true -> result
    | false ->  ArgumentException($"Incorrect number: {arg}") |> raise
    
let parseOperation (arg : string) =
    match arg with
        | "+" -> CalculatorOperation.Plus
        | "-" -> CalculatorOperation.Minus   
        | "*" -> CalculatorOperation.Multiply   
        | "/" -> CalculatorOperation.Divide   
        | _ -> ArgumentException($"Incorrect operation - {arg}") |> raise
        
let parseCalcArguments(args : string[]) =
    match isArgLengthSupported(args) with
    | true -> {arg1 = parseNumber(args[0])
               arg2 = parseNumber(args[2])
               operation = parseOperation(args[1])}
    | false -> ArgumentException("Incorrect count of arguments") |> raise

    
        
        