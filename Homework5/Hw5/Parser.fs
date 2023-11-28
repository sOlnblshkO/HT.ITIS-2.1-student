module Hw5.Parser

open System
open System.Globalization
open Hw5.Calculator

let isArgLengthSupported (args:string[]): Result<'a,'b> =
    if args.Length <> 3 then Error Message.WrongArgLength
    else Ok args 

let tryToDouble (arg : string) : Result<'arg, Message> =
    match Double.TryParse (arg.Replace(".", ",")) with
        | true, v -> Ok v
        | _ -> Error Message.WrongArgFormat
    
[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline isOperationSupported (arg1, operation, arg2): Result<('a * CalculatorOperation * 'b), Message> =
    match operation with
        | Calculator.plus -> Ok(arg1, CalculatorOperation.Plus, arg2)
        | Calculator.minus -> Ok(arg1, CalculatorOperation.Minus, arg2)
        | Calculator.multiply -> Ok(arg1, CalculatorOperation.Multiply, arg2)
        | Calculator.divide -> Ok(arg1, CalculatorOperation.Divide, arg2)
        | _ -> Error Message.WrongArgFormatOperation

let parseArgs (args: string[]): Result<('a * CalculatorOperation * 'b), Message> =
    MaybeBuilder.maybe {
        let! checkedArgs =  (args[0], args[1], args[2]) |> isOperationSupported
        let! arg1 = checkedArgs.Item1 |> tryToDouble
        let operation = checkedArgs.Item2
        let! arg2 = checkedArgs.Item3 |> tryToDouble
        
        return (arg1, operation, arg2 )
    }
    
[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline isDividingByZero (arg1, operation, arg2): Result<('a * CalculatorOperation * 'b), Message> =
    if operation = CalculatorOperation.Divide && arg2 = 0.0 then
        Error Message.DivideByZero
    else Ok(arg1, operation, arg2)
        
let parseCalcArguments (args: string[]): Result<'a, 'b> =
    MaybeBuilder.maybe {
        let! isLengthSupported = args |> isArgLengthSupported
        let! parseArgs = args |> parseArgs
        let! dividingByZero = parseArgs |> isDividingByZero
        
        return dividingByZero
    }