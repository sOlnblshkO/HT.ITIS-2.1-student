module Hw4.Parser

open System
open Hw4.Calculator


type CalcOptions =
    { arg1: float
      arg2: float
      operation: CalculatorOperation }

let isArgLengthSupported (args: string[]) = args.Length = 3

let parseOperation (arg: string) =
    match arg with
    | "+" -> CalculatorOperation.Plus
    | "-" -> CalculatorOperation.Minus
    | "*" -> CalculatorOperation.Multiply
    | "/" -> CalculatorOperation.Divide
    | _ -> CalculatorOperation.Undefined

let parseCalcArguments (args: string[]) =
    if (not (isArgLengthSupported args)) then
        ArgumentException("Incorrect number of arguments") |> raise

    let operation = parseOperation args[1]

    if (operation = CalculatorOperation.Undefined) then
        ArgumentException("Undefined operation") |> raise

    match Double.TryParse args[0] with
    | true, arg1 ->
        match Double.TryParse args[2] with
        | true, arg2 ->
            { arg1 = arg1
              arg2 = arg2
              operation = operation }
        | false, _ -> ArgumentException("Third argument is not a real number") |> raise
    | false, _ -> ArgumentException("First argument is not a real number") |> raise
