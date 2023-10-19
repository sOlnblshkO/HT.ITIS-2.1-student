module Hw6.Calculator


[<CLIMutable>]
type CalcArgs =
    {
        value1: double
        operation: string
        value2: double
    }

let parseOperation operation =
    match operation with
    | "Plus" -> "+"
    | "Minus" -> "-"
    | "Multiply" -> "*"
    | "Divide" -> "/"
    | _ -> operation
    
let inline calculate(value1:double,  operation:string,  value2:double) =
    match operation with
    | "+" -> Ok $"{value1 + value2}"
    | "-" -> Ok $"{value1 - value2}"
    | "*" -> Ok $"{value1 * value2}"
    | "/" -> match value2 = 0.0 with
             | true -> Ok "Dividing by zero"
             | false -> Ok $"{value1 / value2}"
            
            
