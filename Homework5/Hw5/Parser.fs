module Hw5.Parser

open System
open System.Globalization
open Hw5.Calculator
open Hw5.MaybeBuilder

let isArgLengthSupported (args:string[]): Result<'a,'b> =
    match args.Length = 3 with
    | true -> Ok (args[0], args[1], args[2])
    | false -> Message.WrongArgLength |> Error
    
[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline isOperationSupported (arg1, operation, arg2): Result<('a * CalculatorOperation * 'b), Message> =
    match operation with
    | "+" -> Ok (arg1, CalculatorOperation.Plus, arg2)
    | "-" -> Ok (arg1, CalculatorOperation.Minus, arg2)
    | "*" -> Ok (arg1, CalculatorOperation.Multiply, arg2)
    | "/" -> Ok (arg1, CalculatorOperation.Divide, arg2)
    | _ -> Message.WrongArgFormatOperation |> Error
    
let parseArgs (arg1: string, operation, arg2: string)
    : Result<('a * CalculatorOperation * 'b), Message> =
    let mutable res1 = 0.0
    let mutable res2 = 0.0
    match Double.TryParse(arg1, NumberStyles.Float, CultureInfo.InvariantCulture, &res1) &&
          Double.TryParse(arg2, NumberStyles.Float, CultureInfo.InvariantCulture, &res2) with
    | true -> Ok (res1, operation, res2)
    | false -> Message.WrongArgFormat |> Error


[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline isDividingByZero (arg1, operation, arg2): Result<('a * CalculatorOperation * 'b), Message> =
    match operation with
    | CalculatorOperation.Divide -> match arg2 with
                                    | 0.0 -> Message.DivideByZero |> Error
                                    | _ -> Ok (arg1, operation, arg2)
    | _ -> Ok (arg1, operation, arg2)
                                    
let parseCalcArguments (args: string[]): Result<'a, 'b> =
    maybe
        {
        let! lengthSupported = isArgLengthSupported args
        let! operationSupported = isOperationSupported lengthSupported
        let! parsedArguments = parseArgs operationSupported
        let! res = isDividingByZero parsedArguments
        return res
        }