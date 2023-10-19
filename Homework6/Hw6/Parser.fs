module Hw6.Parser

open System
open Hw6.MaybeBuilder
open CalculatorOperation
open System.Globalization
open Hw6.Calculator

let (|ParseOpr|_|) arg =
    match arg with
    | Plus -> Some CalculatorOperation.Plus
    | Minus -> Some CalculatorOperation.Minus
    | Multiply -> Some CalculatorOperation.Multiply
    | Divide -> Some CalculatorOperation.Divide
    | _ -> None
    
let (|Double|_|) (arg:string) =
    match Double.TryParse(arg, NumberStyles.AllowDecimalPoint, Globalization.CultureInfo.InvariantCulture) with
    | true, double -> Some double
    | _ -> None
    
let parseArgs (args: string[]): Result<('a * string * 'b), String> =
    match args[0] with
    | Double val1 ->
        match args[2] with
        | Double val2 -> Ok (val1, args[1], val2)
        | _ -> Error $"Could not parse value '{args[2]}'"
    | _ -> Error $"Could not parse value '{args[0]}'"
    

let inline isOperationSupported (arg1, operation, arg2): Result<('a * CalculatorOperation * 'b), String> =
    match operation with
    | ParseOpr operation -> Ok (arg1, operation, arg2)
    | _ -> Error $"Could not parse value '{operation}'"


let inline isDividingByZero (arg1, operation, arg2): Result<('a * CalculatorOperation * 'b), String> =
    if arg2 = 0.0 && operation = CalculatorOperation.Divide then Error "DivideByZero"
    else Ok (arg1, operation, arg2)
    
let parseCalcArguments (args: string[]): Result<'a, 'b> =
    maybe {
        let! argsParse = args |> parseArgs 
        let! operationParse = argsParse |> isOperationSupported 
        let! checkDividindByZero = operationParse |> isDividingByZero 
        return checkDividindByZero
    }