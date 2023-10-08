module Hw5.Parser

open System
open Hw5.Calculator
open Hw5.MaybeBuilder
open System.Globalization
open Microsoft.FSharp.Core
    
let isArgLengthSupported (args:string[]): Result<'a,'b> =
    if args.Length <> 3 then
        Error Message.WrongArgLength
    else Ok args

(*
    Т.к. в перечислении CalculatorOperation нет значений для невалидной операции,
    то все операции рассматриваются валидными, и смысл в этом методе пропадает.
    Обсудили этот момент с ментором, решили оставить так.
    Реализовывал метод исходя из его названия.
*)
[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline isOperationSupported (arg1, operation, arg2): Result<('a * CalculatorOperation * 'b), Message> =
    match operation with
    | CalculatorOperation.Plus | CalculatorOperation.Minus
    | CalculatorOperation.Divide | CalculatorOperation.Multiply -> Ok (arg1, operation, arg2)
    |_ -> Error Message.WrongArgFormatOperation
   
     
let parseArgs (args: string[]): Result<('a * CalculatorOperation * 'b), Message> =
    let mutable val1 = 0.0
    let mutable val2 = 0.0
    if not(Double.TryParse(args[0], NumberStyles.Float, CultureInfo.InvariantCulture, &val1)
           && Double.TryParse(args[2], NumberStyles.Float, CultureInfo.InvariantCulture, &val2))
        then Error Message.WrongArgFormat
    else
        match args[1] with
        | "+" -> Ok (val1, CalculatorOperation.Plus, val2)
        | "-" -> Ok (val1, CalculatorOperation.Minus, val2)
        | "*" -> Ok (val1, CalculatorOperation.Multiply, val2)
        | "/" -> Ok (val1, CalculatorOperation.Divide, val2)
        | _ -> Error Message.WrongArgFormatOperation
       
    

[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline isDividingByZero (arg1, operation, arg2): Result<('a * CalculatorOperation * 'b), Message> =
     match operation with
     | CalculatorOperation.Divide ->
        if arg2 = 0.0 then
            Error Message.DivideByZero
        else
            Ok (arg1, operation, arg2)
     | _ -> Ok (arg1, operation, arg2)
    
let parseCalcArguments (args: string[]): Result<'a, 'b> =
    maybe {
        let! isArgLengthSupported = isArgLengthSupported args
        let! parseArgs = parseArgs isArgLengthSupported
        let! isOperationSupported = isOperationSupported parseArgs
        let! isDividingByZero = isDividingByZero isOperationSupported
        return isDividingByZero
    }