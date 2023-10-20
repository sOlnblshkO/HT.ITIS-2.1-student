module Hw6.App

open System
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection
open Giraffe
open Hw5
open Hw5.Calculator
open System.Globalization

[<CLIMutable>]
type CalcArgs =
    {
        value1: string
        operation: string
        value2: string
    }

[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let parseOp (op: string) =
    match op with
    | "Plus" -> "+"
    | "Minus" -> "-"
    | "Multiply" -> "*"
    | "Divide" -> "/"
    | _ -> op
    
[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let tryParseTwoArgs (val1:string, val2:string) =
    match Double.TryParse(val1, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture) with
    | true, _ ->
         match Double.TryParse(val2, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture) with
         | true, _ -> "All args is correct"
         | false, _ -> val2
    | false, _ -> val1
    
[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let parseArgs (args: CalcArgs): Result<float * CalculatorOperation * float,string> =
    let array = [| args.value1; parseOp args.operation; args.value2 |]
    let newArgs = Parser.parseCalcArguments array
    match newArgs with
    | Error e -> match e with
                 | Message.DivideByZero -> Error "DivideByZero"
                 | Message.WrongArgFormatOperation -> Error $"Could not parse value '{args.operation}'"
                 | Message.WrongArgFormat ->
                     let incorrectArg = tryParseTwoArgs(args.value1, args.value2) // because from the hw5.parser we cannot find out which argument is incorrect
                     Error $"Could not parse value '{incorrectArg}'"
                 | _ -> Error "Unexpected error"
    | Ok x -> Ok x

[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let calculateArgs val1 op val2 =
    try
        let result = calculate val1 op val2
        match result with
        | _ -> Ok result
    with
    | :? ArgumentOutOfRangeException -> Error "Argument exception"
    
 
let calculatorHandler: HttpHandler =
    fun next ctx ->
        let result: Result<string, string> = Hw5.MaybeBuilder.maybe {
            let! args = ctx.TryBindQueryString<CalcArgs>()
            let! val1, op, val2 = parseArgs args 
            let! output = calculateArgs val1 op val2
            return output.ToString()
        }
        match result with
        | Ok ok -> (setStatusCode 200 >=> text (ok.ToString())) next ctx
        | Error error -> (setStatusCode 400 >=> text error) next ctx

let webApp =
    choose [
        GET >=> choose [
             route "/" >=> text "Use //calculate?value1=<VAL1>&operation=<OPERATION>&value2=<VAL2>"
             route "/calculate" >=> calculatorHandler
        ]
        setStatusCode 404 >=> text "Not Found" 
    ]
    
type Startup() =
    member _.ConfigureServices (services : IServiceCollection) =
        services.AddGiraffe() |> ignore

    member _.Configure (app : IApplicationBuilder) (_ : IHostEnvironment) (_ : ILoggerFactory) =
        app.UseGiraffe webApp
        
[<EntryPoint>]
let main _ =
    Host
        .CreateDefaultBuilder()
        .ConfigureWebHostDefaults(fun whBuilder -> whBuilder.UseStartup<Startup>() |> ignore)
        .Build()
        .Run()
    0 