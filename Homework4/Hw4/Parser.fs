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
    | _ -> ArgumentException("Ошибочный оператор") |> raise
    
let parseArgument (arg : string) =
    match Double.TryParse(arg) with
    | (true, number) -> number
    | _ -> ArgumentException("Это не число") |> raise
    
    
let parseCalcArguments(args : string[]) =
    if not (isArgLengthSupported args) then
        ArgumentException("Ошибочное число аргументов") |> raise
    
    let val1 = parseArgument(args[0])
    let oper = parseOperation(args[1])
    let val2 = parseArgument(args[2])
    
    { arg1 = val1; operation = oper; arg2 = val2 }
  
