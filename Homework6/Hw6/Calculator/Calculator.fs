module Hw6.Calculator.Calculator

open Hw6.Calculator
open CalculatorOperation
open System

let inline calculate value1 operation value2: 'a =
    match operation with
        | CalculatorOperation.Plus -> value1 + value2
        | CalculatorOperation.Minus -> value1 - value2
        | CalculatorOperation.Multiply -> value1 * value2
        | CalculatorOperation.Divide -> value1 / value2
        | _ -> ArgumentOutOfRangeException($"Value must be from 0 to 3, was given {operation}") |> raise