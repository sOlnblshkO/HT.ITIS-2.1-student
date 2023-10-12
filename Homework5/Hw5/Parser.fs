module Hw5.Parser

open System
open Hw5.Calculator
open System.Globalization

let isArgLengthSupported (args:string[]): Result<'a,'b> =
    match Array.length args = 3 with
    | true -> Ok args
    | false -> Error Message.WrongArgLength
    
[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline isOperationSupported (arg1, operation, arg2): Result<('a * CalculatorOperation * 'b), Message> =
    match operation with
    | "+" -> Ok (arg1, CalculatorOperation.Plus, arg2)
    | "-" -> Ok (arg1, CalculatorOperation.Minus, arg2)
    | "*" -> Ok (arg1, CalculatorOperation.Multiply, arg2)
    | "/" -> Ok (arg1, CalculatorOperation.Divide, arg2)
    | _ -> Error Message.WrongArgFormatOperation

let parseArgs (args: string[]): Result<('a * string * 'b), Message> =
    match Double.TryParse(args[0], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture) with
    | true, val1 ->
         match Double.TryParse(args[2], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture) with
         | true, val2 -> Ok (val1, args[1], val2)
         | false, _ -> Error Message.WrongArgFormat
    | false, _ -> Error Message.WrongArgFormat

[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline isDividingByZero (arg1, operation, arg2): Result<('a * CalculatorOperation * 'b), Message> =
    match operation with
    | CalculatorOperation.Divide ->
        match arg2 with
        | 0.0 -> Error Message.DivideByZero
        | _ -> Ok (arg1, operation, arg2)
    | _ -> Ok (arg1, operation, arg2)
    
let parseCalcArguments (args: string[]): Result<'a, 'b> =
    MaybeBuilder.maybe {
        let! args = isArgLengthSupported args
        let! args = parseArgs args
        let! args = isOperationSupported args
        let! args = isDividingByZero args
        return args
    }