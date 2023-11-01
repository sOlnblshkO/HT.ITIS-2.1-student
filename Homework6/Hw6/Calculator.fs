module Hw6.Calculator

open System
open CalculatorOperation

[<Literal>]
let Plus = "+"

[<Literal>]
let Minus = "-"

[<Literal>]
let Multiply = "*"

[<Literal>]
let Divide = "/"

[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline calculate value1 operation value2: 'a =
    match operation with
    | CalculatorOperation.Plus -> value1 + value2
    | CalculatorOperation.Minus -> value1 - value2
    | CalculatorOperation.Multiply -> value1 * value2
    | CalculatorOperation.Divide -> value1 / value2
