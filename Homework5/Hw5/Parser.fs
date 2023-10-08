module Hw5.Parser

open System
open Hw5.Calculator
open Hw5.MaybeBuilder

// REASON: Bug in codecov. Although all match paths get covered,
// it doesn't want to admit full coverage.
[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let isArgLengthSupported (args: string[]) : Result<(string * string * string), Message> =
    match args with
    | [| arg1; operation; arg2 |] -> Ok(arg1, operation, arg2)
    | _ -> Error Message.WrongArgLength

[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline isOperationSupported (arg1, operation, arg2) : Result<('a * CalculatorOperation * 'b), Message> =
    match operation with
    | Calculator.plus -> Ok(arg1, CalculatorOperation.Plus, arg2)
    | Calculator.minus -> Ok(arg1, CalculatorOperation.Minus, arg2)
    | Calculator.divide -> Ok(arg1, CalculatorOperation.Divide, arg2)
    | Calculator.multiply -> Ok(arg1, CalculatorOperation.Multiply, arg2)
    | _ -> Error Message.WrongArgFormatOperation

let parseDouble (a: string) =
    let success, parsed = Double.TryParse a
    if success then Ok parsed else Error Message.WrongArgFormat

let parseArgs (args: string[]) : Result<(double * CalculatorOperation * double), Message> =
    maybe {
        let! args = isArgLengthSupported args
        let! arg1, operation, arg2 = isOperationSupported args
        let! arg1 = parseDouble arg1
        let! arg2 = parseDouble arg2
        return (arg1, operation, arg2)
    }

[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline isDividingByZero (arg1, operation, arg2) : Result<('a * CalculatorOperation * 'b), Message> =
    if
        (operation = CalculatorOperation.Divide)
        && (arg2 = LanguagePrimitives.GenericZero)
    then
        Error Message.DivideByZero
    else
        Ok(arg1, operation, arg2)

let parseCalcArguments (args: string[]) : Result<(double * CalculatorOperation * double), Message> =
    maybe {
        let! args = parseArgs args
        let! args = isDividingByZero args
        return args
    }
