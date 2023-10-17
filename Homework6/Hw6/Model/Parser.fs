module Hw6.Parser

open System
open Hw6.Calculator
open Hw6.MaybeBuilder

let isArgLengthSupported (args:string[]): Result<'a,'b> =
    let isSupported = args.Length = 3
    match isSupported with
    | true -> Ok args
    | false -> Error "Wrong arg length" 
    

let parseOperation(arg:string):Result<CalculatorOperation,string> =
    match arg with
    | "Plus" -> Ok CalculatorOperation.Plus
    | "Minus" -> Ok CalculatorOperation.Minus
    | "Multiply" -> Ok CalculatorOperation.Multiply
    | "Divide" -> Ok CalculatorOperation.Divide
    | "Pow" -> Ok CalculatorOperation.Pow
    | _ -> Error $"Could not parse value '{arg}'" 


[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline isDividingByZero (arg1, operation, arg2): Result<('a * CalculatorOperation * 'b), string> =
    let zero = Convert.ChangeType(0, typeof<'b>) :?> 'b
    match operation with  
    | CalculatorOperation.Divide ->
        match arg2 with
        | _ when arg2 = zero -> Error "DivideByZero"
        | _ -> Ok (arg1,operation,arg2)
    | _ -> Ok (arg1,operation,arg2)

[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline isOperationSupported (arg1, operation, arg2): Result<('a * CalculatorOperation * 'b), string> =
    match operation with
    | CalculatorOperation.Plus -> Ok (arg1, operation, arg2)
    | CalculatorOperation.Minus -> Ok (arg1, operation, arg2)
    | CalculatorOperation.Multiply -> Ok (arg1, operation, arg2)
    | CalculatorOperation.Divide -> Ok (arg1, operation, arg2)
    | _ -> Error $"Could not parse value '{operation}'" 

let convertValue (arg:string) : Result<double,string> = 
    match Double.TryParse arg with
        | (true,value) -> Ok value
        | _ -> Error $"Could not parse value '{arg}'" 

let convertValues (args:string[]) : Result<('a * CalculatorOperation * 'b), string> = 
    maybe
        {
        let! firstValue = convertValue args[0]
        let! secondValue = convertValue args[2]
        let! operation = parseOperation args[1]
        return (firstValue,operation,secondValue)
        }
    

let parseArgs (args: string[]): Result<('a * CalculatorOperation * 'b), string> = 
    maybe
        {
        let! supportedArgs = isArgLengthSupported args           
        let! convertedArgs = convertValues supportedArgs        
        return convertedArgs
        }    
        
let parseCalcArguments (args: string[]): Result<'a, 'b>  =    
    maybe
        {        
        let! parsedArgs = parseArgs args     
        let! parsedSupportedArgs = isOperationSupported parsedArgs
        let! resultArgs = isDividingByZero parsedSupportedArgs
        return resultArgs
        }    