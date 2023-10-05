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
   
let parseArgument (arg : string) (order : int) : float = 
    let isArgValid, val1 = Double.TryParse arg
    if not isArgValid then raise (new ArgumentException($"{order} argument invalid"))
    val1

let parseCalcArguments(args : string[]) : CalcOptions =
    if not (isArgLengthSupported args) then raise (new ArgumentException("Invalid argument count"))
    let val1=parseArgument args[0] 1
    let val2=parseArgument args[2] 2
    {CalcOptions.arg1 = val1; CalcOptions.arg2 = val2; CalcOptions.operation = parseOperation args[1]}

