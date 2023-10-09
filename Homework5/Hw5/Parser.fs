module Hw5.Parser

open System
open Hw5.Calculator

let isArgLengthSupported (args:string[]): Result<'a,'b> =
    if (args.Length = 3) then
        Ok args
    else Error Message.WrongArgLength
    
[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline isOperationSupported (arg1, operation, arg2): Result<('a * CalculatorOperation * 'b), Message> =
    match operation with
    | Calculator.plus -> Ok (arg1, CalculatorOperation.Plus, arg2)
    | Calculator.minus -> Ok (arg1, CalculatorOperation.Minus, arg2)
    | Calculator.multiply -> Ok (arg1, CalculatorOperation.Multiply, arg2)
    | Calculator.divide -> Ok (arg1, CalculatorOperation.Divide, arg2)
    | _ -> Error Message.WrongArgFormatOperation

let tryParseToDouble (arg: string) = 
    match Double.TryParse(arg) with
    | (true, value) -> Ok value
    | _ -> Error Message.WrongArgFormat


let parseArgs (args: string[]): Result<('a * CalculatorOperation * 'b), Message> =
    maybe
        {
        let! arg1 = tryParseToDouble args[0]
        let! arg2 = tryParseToDouble args[2]
        let! result = isOperationSupported (arg1, args[1] , arg2)
        return result
        }

[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline isDividingByZero (arg1, operation, arg2): Result<('a * CalculatorOperation * 'b), Message> =
    if operation = CalculatorOperation.Divide then
        match arg2 with
        | 0.0 -> Error Message.DivideByZero
        | _ -> Ok (arg1, operation, arg2)
    else Ok (arg1, operation, arg2)
    
let parseCalcArguments (args: string[]): Result<'a, 'b> =
    maybe
        {
        let! argsLengthSupported = args |> isArgLengthSupported
        let! argsParsed = argsLengthSupported |> parseArgs
        let! dividedByZero = argsParsed |> isDividingByZero
        return dividedByZero
        }
    