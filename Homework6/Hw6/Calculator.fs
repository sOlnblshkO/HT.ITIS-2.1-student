module Hw6.Calculator

type CalculatorOperation =
     | Plus = 0
     | Minus = 1
     | Multiply = 2
     | Divide = 3

[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let calculate (val1:double, op:CalculatorOperation, val2:double)=
    match op with
    | CalculatorOperation.Plus -> Ok $"{val1 + val2}"
    | CalculatorOperation.Minus -> Ok $"{val1 - val2}"
    | CalculatorOperation.Multiply -> Ok $"{val1 * val2}"
    | CalculatorOperation.Divide ->
        match val2 with
        | 0.0 -> Ok "DivideByZero"
        | _ -> Ok $"{val1 / val2}"
    | _ -> Error "Operation is undefined"     
