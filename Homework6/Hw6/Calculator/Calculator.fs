module Hw6.Calculator.Calculator

open Hw6.Calculator.CalculatorOperation

[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline calculate (value1 : float,  operation : string,  value2 : float): 'a =
    match operation with
        | Plus -> Ok $"{value1 + value2}"
        | Minus -> Ok $"{value1 - value2}"
        | Multiply -> Ok $"{value1 * value2}"
        | Divide -> if value2 <> 0.0 then Ok $"{value1 / value2}"
                                        else Ok("DivideByZero")
        | _ -> Error $"Could not parse value '{operation}'\nAvailable operations : +, -, *, /"