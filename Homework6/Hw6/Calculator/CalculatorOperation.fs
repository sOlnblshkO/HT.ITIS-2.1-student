module Hw6.Calculator.CalculatorOperation

[<Literal>] 
let Plus = "+"

[<Literal>] 
let Minus = "-"

[<Literal>] 
let Multiply = "*"

[<Literal>] 
let Divide = "/"

let tryToCalculatorOperation (arg: string) =
    match arg with
    | "Plus" -> Plus
    | "Minus" -> Minus
    | "Multiply" -> Multiply
    | "Divide" -> Divide
    | _ -> arg