module Hw4.Parser

open System
open Hw4.Calculator


type CalcOptions = {
    arg1: float
    arg2: float
    operation: CalculatorOperation
}

let isArgLengthSupported (args : string[]) =
    match args.Length with
    | 3 -> true
    | _ -> false

let parseOperation (arg : string) =
    match arg with
    | "+" -> CalculatorOperation.Plus
    | "-" -> CalculatorOperation.Minus
    | "*" -> CalculatorOperation.Multiply
    | "/" -> CalculatorOperation.Divide
    | _ -> ArgumentException "Unknown operation" |> raise
    
let parseArgument (arg : string) =
    try float(arg)
    with _ -> ArgumentException "Input string was not a number" |> raise
    
let parseCalcArguments(args : string[]) =
    if not (isArgLengthSupported args) then
        ArgumentException "Wrong argument length" |> raise
    { arg1 = parseArgument(args[0]); arg2 = parseArgument(args[2]); operation = parseOperation(args[1]) }
