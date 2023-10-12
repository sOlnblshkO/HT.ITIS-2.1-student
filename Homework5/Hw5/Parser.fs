module Hw5.Parser

open System
open Hw5.Calculator
open Hw5.MaybeBuilder

let isArgLengthSupported (args:string[]): Result<'a,'b> =
    let tupleArgs = (args[0],args[1],args[2])
    match (args.Length = 3) with
    |true -> Ok tupleArgs  
    |false -> Error Message.WrongArgLength
    
[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline isOperationSupported (arg1, operation, arg2): Result<('a * CalculatorOperation * 'b), Message> =
    match operation with
    | Calculator.plus -> Ok (arg1, CalculatorOperation.Plus, arg2)
    | Calculator.minus -> Ok (arg1, CalculatorOperation.Minus, arg2)
    | Calculator.multiply -> Ok (arg1, CalculatorOperation.Multiply, arg2)
    | Calculator.divide -> Ok (arg1, CalculatorOperation.Divide, arg2)
    | _ -> Error Message.WrongArgFormatOperation


let stringDoubleParser (arg : string) =
    match Double.TryParse(arg) with
    | true, value -> Ok value
    | _ -> Error Message.WrongArgFormat
    
let parseArgs (args: string[]): Result<('a * CalculatorOperation * 'b), Message> =
    maybe{
        let! tupleArgs = isArgLengthSupported args 
        let! arg1 = stringDoubleParser args[0]
        let! arg2 =  stringDoubleParser args[2]
        let! _, operation, _ = isOperationSupported tupleArgs
        
        return arg1,operation,arg2
    }

[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline isDividingByZero (arg1, operation, arg2): Result<('a * CalculatorOperation * 'b), Message> =
    match operation, arg2 with
    | CalculatorOperation.Divide, 0.0 -> Error Message.DivideByZero
    | _ -> Ok (arg1, operation, arg2)
    
let parseCalcArguments (args: string[]): Result<'a, 'b> =
    maybe {
        let! tupleArgs = args |> parseArgs
        let! finalArgs = tupleArgs |> isDividingByZero
        
        return finalArgs
    }