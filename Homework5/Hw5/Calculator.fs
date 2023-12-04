module Hw5.Calculator

open System
open Hw5.CalculatorOperation

[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline calculate value1 operation value2: 'a =
    match operation with
        | CalculatorOperation.Plus -> value1 + value2
        | CalculatorOperation.Minus -> value1 - value2
        | CalculatorOperation.Multiply -> value1 * value2
        | CalculatorOperation.Divide -> value1 / value2
        | _ -> ArgumentOutOfRangeException($"Value must be from 0 to 3, was given {operation}") |> raise