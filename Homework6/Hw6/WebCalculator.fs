module Hw6.WebCalculator


[<CLIMutable>]
type CalculatorModel =
    { value1: double
      operation: string
      value2: double }

let parseOperation (value1: double) (value2: double) (operation: string) =
    match operation with
    | "Plus" -> Ok(string (value1 + value2))
    | "Minus" -> Ok(string (value1 - value2))
    | "Multiply" -> Ok(string (value1 * value2))
    | "Divide" when value2 = 0 -> Ok "DivideByZero" // Honestly an error would be more fitting
    | "Divide" -> Ok(string (value1 / value2))
    | _ -> Error $"Could not parse value '{operation}'"

let calculate (model: CalculatorModel) =
    parseOperation model.value1 model.value2 model.operation
