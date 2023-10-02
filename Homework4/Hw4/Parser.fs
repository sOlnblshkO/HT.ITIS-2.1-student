module Hw4.Parser

open System
open Hw4.Calculator


type CalcOptions = {
    arg1: float
    arg2: float
    operation: CalculatorOperation
}

let isArgLengthSupported (args : string[]) =
    if (args.Length = 3)
    then true
    else raise (ArgumentException("Wrong args length"))

let parseOperation (arg : string) =
    match arg with
    | "+" -> CalculatorOperation.Plus
    | "-" -> CalculatorOperation.Minus
    | "*" -> CalculatorOperation.Multiply
    | "/" -> CalculatorOperation.Divide
    | _ -> ArgumentException("Wrong operation") |> raise
    
let parseCalcArguments(args : string[]) =
    isArgLengthSupported(args) |> ignore
    let operation = parseOperation(args[1])
    let (isArg1Converted, arg1) = Double.TryParse(args[0])
    let (isArg2Converted, arg2) = Double.TryParse(args[2])
    
    if (not isArg1Converted || not isArg2Converted)
    then ArgumentException("Wrong args") |> raise
    
    let calcOptions = {
        arg1 = arg1
        arg2 = arg2
        operation = operation
    }
    
    calcOptions
     
