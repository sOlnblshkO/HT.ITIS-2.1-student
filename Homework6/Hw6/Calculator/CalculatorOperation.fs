module Hw6.Calculator.CalculatorOperation

type CalculatorOperation =
     | Plus = 0
     | Minus = 1
     | Multiply = 2
     | Divide = 3

[<Literal>] 
let plus = "Plus"

[<Literal>] 
let minus = "Minus"

[<Literal>] 
let multiply = "Multiply"

[<Literal>] 
let divide = "Divide"
