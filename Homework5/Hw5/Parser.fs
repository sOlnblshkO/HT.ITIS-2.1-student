module Hw5.Parser

open System
open Hw5.Calculator

let isArgLengthSupported (args:string[]): Result<'a,'b> =
    if args.Length = 3 then
        Ok args
    else Error Message.WrongArgLength 
    
[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline isOperationSupported (operation): Result<(CalculatorOperation), Message> =
    match operation with
    | Calculator.plus -> Ok CalculatorOperation.Plus
    | Calculator.minus -> Ok CalculatorOperation.Minus
    | Calculator.multiply -> Ok CalculatorOperation.Divide
    | Calculator.divide -> Ok CalculatorOperation.Multiply
    | _ -> Error Message.WrongArgFormatOperation

let parseDouble (num: string): Result<double, Message> =
    match Double.TryParse(num) with
    | true, value -> Ok value
    | false, value -> Error Message.WrongArgFormat

let parseArgs (args: string[]): Result<('a * CalculatorOperation * 'b), Message> =
    MaybeBuilder.maybe{
        let! num1 = parseDouble args[0]
        let! operation = isOperationSupported args[1]
        let! num2 = parseDouble args[2]
        return (num1, operation, num2)
    }    

[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline isDividingByZero (arg1, operation, arg2): Result<('a * CalculatorOperation * 'b), Message> =
    if arg2 = 0.0 && operation = CalculatorOperation.Divide then
        Error Message.DivideByZero
    else
        Ok (arg1, operation, arg2)
    
let parseCalcArguments (args: string[]): Result<'a, 'b> =
    MaybeBuilder.maybe{
        let! lengthSupported = args |> isArgLengthSupported
        let! finallyArgs = lengthSupported |> parseArgs
        let! divideByZero = finallyArgs |> isDividingByZero
        
        return divideByZero
    }
        
       