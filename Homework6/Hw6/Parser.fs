module Hw6.Parser

open Hw6.Calculator

[<CLIMutable>]
type calculatorArgs =
    {
        value1: double
        operation: string
        value2: double
    }

let parseArgs (args: calculatorArgs) =
    match args.operation with
    | "Plus" -> Ok (args.value1, CalculatorOperation.Plus, args.value2)
    | "Minus" -> Ok (args.value1, CalculatorOperation.Minus, args.value2)
    | "Multiply" -> Ok (args.value1, CalculatorOperation.Multiply, args.value2)
    | "Divide" -> Ok (args.value1, CalculatorOperation.Divide, args.value2)
    | _ -> Error $"Could not parse value '{args.operation}'"  