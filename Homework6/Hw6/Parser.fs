module Hw6.Parser

open Hw6.Calculator


[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline parseOperation operation: Result<CalculatorOperation, string> =
    match operation with
    | "Plus" -> Ok CalculatorOperation.Plus
    | "Minus" -> Ok CalculatorOperation.Minus
    | "Multiply" -> Ok CalculatorOperation.Multiply
    | "Divide" -> Ok CalculatorOperation.Divide
    | _ -> Error $"Could not parse value '{operation}'"
