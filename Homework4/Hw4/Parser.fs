module Hw4.Parser

open System
open Hw4.Calculator


type CalcOptions = {
    arg1: float
    arg2: float
    operation: CalculatorOperation
}

let isArgLengthSupported (args : string[]) : bool =
    args.Length = 3

let parseOperation (arg : string) =
    match arg with
    | "+" -> CalculatorOperation.Plus
    | "-" -> CalculatorOperation.Minus
    | "*" -> CalculatorOperation.Multiply
    | "/" -> CalculatorOperation.Divide
    | _ -> raise (new ArgumentException($"Invalid argument: {arg}"))
    
let parseCalcArguments(args : string[]) : CalcOptions =
    if not (isArgLengthSupported args) then raise (new ArgumentException("Invalid argument count"))
    let isOk1, val1 = Double.TryParse args[0]
    if not isOk1 then raise (new ArgumentException("First argument invalid"))
    let isOk2, val2 = Double.TryParse args[2]
    if not isOk2 then raise (new ArgumentException("Second argument invalid"))
    {CalcOptions.arg1 = val1; CalcOptions.arg2 = val2; CalcOptions.operation = parseOperation args[1]}