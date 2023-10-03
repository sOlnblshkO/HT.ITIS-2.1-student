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
    if (Double.TryParse(arg,  &result)) then result
    else raise(ArgumentException($"Incorrect number: {arg}"))
    
let parseOperation (arg : string) =
    match arg with
        | "+" -> CalculatorOperation.Plus
        | "-" -> CalculatorOperation.Minus   
        | "*" -> CalculatorOperation.Multiply   
        | "/" -> CalculatorOperation.Divide   
        | _ -> raise(ArgumentException($"Incorrect operation - {arg}"))
        
let parseCalcArguments(args : string[]) =
    if (not(isArgLengthSupported(args))) then
        raise(ArgumentException("Incorrect count of arguments"))
    {arg1 = parseNumber(args[0]); arg2 = parseNumber(args[2]); operation = parseOperation(args[1])}
        