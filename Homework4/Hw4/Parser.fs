module Hw4.Parser

open System
open Hw4.Calculator


type CalcOptions = {
    arg1: float
    arg2: float
    operation: CalculatorOperation
}

let isArgLengthSupported (args : string[]) =
    Array.length args = 3

let parseOperation (arg : string) =
    match arg with
    | "+" -> CalculatorOperation.Plus
    | "-" -> CalculatorOperation.Minus  
    | "*" -> CalculatorOperation.Multiply
    | "/" -> CalculatorOperation.Divide
    | _ -> InvalidOperationException("Некорректная операция") |> raise

    
let parseCalcArguments(args : string[]) =
    match isArgLengthSupported args with
    | false -> ArgumentException("Некорректное количество аргументов") |> raise
    | true ->
        let op = parseOperation args[1]
        match Double.TryParse args[0] with
        | true, val1 ->
             match Double.TryParse args[2] with
             | true, val2 -> { arg1 = val1; arg2 = val2; operation = op }
             | false, _ -> ArgumentException("Некорректные значения") |> raise
        | false, _ -> ArgumentException("Некорректные значения") |> raise

    
    

    

    
