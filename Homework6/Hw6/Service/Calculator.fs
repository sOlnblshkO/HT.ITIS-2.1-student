module Hw6.Service.Calculator

[<CLIMutable>]
type calcArgs =
    { value1: double
      operation: string
      value2: double }

let parseOperation operation =
    match operation with
    | "Plus" -> "+"
    | "Minus" -> "-"
    | "Multiply" -> "*"
    | "Divide" -> "/"
    | _ -> operation

[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let calculate (value1: double, operation: string, value2: double) =
    match operation with
    | "+" -> Ok $"{value1 + value2}"
    | "-" -> Ok $"{value1 - value2}"
    | "*" -> Ok $"{value1 * value2}"
    | "/" -> if value2 <> 0.0 then Ok $"{value1 / value2}"
             else Ok "DivideByZero"
    | _ -> Error $"Could not parse value '{operation}'"