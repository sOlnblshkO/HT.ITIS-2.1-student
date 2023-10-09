module Hw5.Parser

open System
open Hw5.Calculator
open Hw5.MaybeBuilder
let isArgLengthSupported (args:string[]): Result<'a,'b> =
    if(args.Length = 3) then
        Ok args
    else
        Error Message.WrongArgLength
    
[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline isOperationSupported (arg1, operation, arg2): Result<('a * CalculatorOperation * 'b), Message> =
    match operation with
    | Calculator.plus -> Ok (arg1, CalculatorOperation.Plus, arg2)
    | Calculator.minus -> Ok (arg1,CalculatorOperation.Minus,arg2)
    | Calculator.multiply -> Ok (arg1,CalculatorOperation.Multiply,arg2)
    | Calculator.divide -> Ok (arg1,CalculatorOperation.Divide,arg2)
    | _ -> Error (Message.WrongArgFormatOperation)
    
let parseArgument (arg: string) =
    match Double.TryParse(arg) with
    | (true, num) -> Ok num
    | _ -> Error Message.WrongArgFormat
    
let parseArgs (args: string[]): Result<('a * CalculatorOperation * 'b), Message> =
    maybe {
        let! parsedArgs = (args[0],args[1], args[2]) |> isOperationSupported
        let! arg1 = parsedArgs.Item1 |> parseArgument
        let! arg2 = parsedArgs.Item3 |> parseArgument
        return (arg1, parsedArgs.Item2, arg2)
    }

[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline isDividingByZero (arg1, operation, arg2): Result<('a * CalculatorOperation * 'b), Message> =
    if arg2 = 0.0 && operation = CalculatorOperation.Divide then
        Error (Message.DivideByZero)
    else
        Ok (arg1, operation, arg2)
    
let parseCalcArguments (args: string[]): Result<'a, 'b> =
    maybe { 
        let! argLengthSupported = args |> isArgLengthSupported 
        let! getParseArgs = argLengthSupported |> parseArgs
        let! dividingNotByZero = getParseArgs |> isDividingByZero

        return dividingNotByZero
    }    