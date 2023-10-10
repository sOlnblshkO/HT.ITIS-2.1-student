module Hw5.Parser

open System
open Hw5.Calculator
open Hw5.MaybeBuilder

let isArgLengthSupported (args:string[]): Result<'a,'b> =
    if args.Length = 3 then
        Ok args
    else
        Error Message.WrongArgLength 
     
[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline isOperationSupported (arg1, operation, arg2): Result<('a * CalculatorOperation * 'b), Message> =
    match operation with
    | Calculator.plus -> Ok(arg1, CalculatorOperation.Plus, arg2)
    | Calculator.minus -> Ok(arg1, CalculatorOperation.Minus, arg2)
    | Calculator.multiply -> Ok(arg1, CalculatorOperation.Multiply, arg2)
    | Calculator.divide -> Ok(arg1, CalculatorOperation.Divide, arg2)
    | _ -> Error Message.WrongArgFormatOperation

let FromStringToDouble (arg : string) =
    match Double.TryParse(arg) with
    | true, value -> Ok value
    | _ -> Error Message.WrongArgFormat

let parseArgs (args: string[]): Result<('a * CalculatorOperation * 'b), Message> =
    maybe{
        let! сheckLength = args |> isArgLengthSupported
        let! args1 = args[0] |> FromStringToDouble
        let! args2 =  args[2] |> FromStringToDouble
        let! result = isOperationSupported(args1, args[1], args2)
        return result
    }
    
[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline isDividingByZero (arg1, operation, arg2): Result<('a * CalculatorOperation * 'b), Message> =
    if (operation,arg2) = (CalculatorOperation.Divide, 0.0) then
        Error Message.DivideByZero
    else
        Ok(arg1, operation, arg2)
    
let parseCalcArguments (args: string[]): Result<'a, 'b> =
    maybe{
        let! argsParsed = args |> parseArgs
        let! divideByZero = argsParsed |> isDividingByZero
        return divideByZero
    }