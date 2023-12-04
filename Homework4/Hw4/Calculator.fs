module Hw4.Calculator
open System
open Hw4.CalculatorOperations

let calculate (value1 : float) (operation : CalculatorOperation) (value2 : float) =
    match operation with
        | CalculatorOperation.Plus -> value1 + value2
        | CalculatorOperation.Minus -> value1 - value2
        | CalculatorOperation.Multiply -> value1 * value2
        | CalculatorOperation.Divide -> value1 / value2
        | _ -> ArgumentOutOfRangeException($"Value must be from 0 to 3, was given {operation}") |> raise
