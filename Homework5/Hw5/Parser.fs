module Hw5.Parser

open System
open Hw5.Calculator
open Hw5.MaybeBuilder
open System.Globalization
open Microsoft.FSharp.Core
    
let isArgLengthSupported (args:string[]): Result<string[], Message> =
    if args.Length <> 3 then
        Error Message.WrongArgLength
    else Ok args
    
let parseOperation (arg1, operation, arg2) : Result<Double * CalculatorOperation * Double, Message> =
    match operation with
    | "+" -> Ok (arg1, CalculatorOperation.Plus, arg2)
    | "-" -> Ok (arg1, CalculatorOperation.Minus, arg2)
    | "*" -> Ok (arg1, CalculatorOperation.Multiply, arg2)
    | "/" -> Ok (arg1, CalculatorOperation.Divide, arg2)
    | _ -> Error Message.WrongArgFormatOperation
     
let parseArgs (args: string[]): Result<Double * string * Double, Message> =
    let mutable val1 = 0.0
    let mutable val2 = 0.0
    if not(Double.TryParse(args[0], NumberStyles.Float, CultureInfo.InvariantCulture, &val1)
           && Double.TryParse(args[2], NumberStyles.Float, CultureInfo.InvariantCulture, &val2))
        then Error Message.WrongArgFormat
    else
        Ok (val1, args[1], val2)
       

[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline isDividingByZero (arg1, operation, arg2): Result<Double * CalculatorOperation * Double, Message> =
     match operation with
     | CalculatorOperation.Divide ->
        if abs(arg2) < Double.Epsilon then
            Error Message.DivideByZero
        else
            Ok (arg1, operation, arg2)
     | _ -> Ok (arg1, operation, arg2)
    
let parseCalcArguments (args: string[]): Result<Double * CalculatorOperation * Double, Message> =
    maybe {
        let! isArgLengthSupported = isArgLengthSupported args
        let! parseArgs = parseArgs isArgLengthSupported
        let! parseOperation = parseOperation parseArgs
        let! isDividingByZero = isDividingByZero parseOperation
        return isDividingByZero
    }