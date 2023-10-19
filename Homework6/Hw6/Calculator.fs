module Hw6.Calculator

open System


type CalculatorOperation =
     | Plus = 0
     | Minus = 1
     | Multiply = 2
     | Divide = 3

[<Literal>] 
let plus = "+"

[<Literal>] 
let minus = "-"

[<Literal>] 
let multiply = "*"

[<Literal>] 
let divide = "/"

[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline calculate value1 operation value2: _ =
    match operation with
    | CalculatorOperation.Plus -> Ok $"{value1 + value2}"
    | CalculatorOperation.Minus -> Ok $"{value1 - value2}"
    | CalculatorOperation.Multiply -> Ok $"{value1 * value2}"
    | CalculatorOperation.Divide ->
        match value2 with
        | 0.0 -> Ok "DivideByZero"
        | _ -> Ok $"{value1 / value2}"
    | _ -> ArgumentOutOfRangeException() |> raise



