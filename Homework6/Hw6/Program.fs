module Hw6.App

open System
open System.Globalization
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection
open Giraffe

let parseArgument (arg: string) =
    match Double.TryParse(arg, NumberStyles.Float, CultureInfo.InvariantCulture) with
    | true, num -> Ok num
    | _ -> Error $"Could not parse value '{arg}'"
    
    
let Calculate (value1: string, operation: string, value2: string): Result<string, string> =
    
    let val1 = parseArgument value1
    let val2 = parseArgument value2
    
    match val1 with
        | Ok val1 ->  match val2 with
                      | Ok val2 -> match operation with
                                     | "Plus" -> Ok ((val1+val2).ToString())
                                     | "Minus" -> Ok ((val1-val2).ToString())
                                     | "Multiply" -> Ok ((val1*val2).ToString())
                                     | "Divide" -> match val2 with
                                                   | 0.0 -> Ok "DivideByZero"
                                                   | _ -> Ok ((val1/val2).ToString())
                                     | _ -> Error $"Could not parse value '{operation}'"
                      | Error errorMessage -> Error errorMessage
        | Error errorMessage -> Error errorMessage
    
let calculatorHandler: HttpHandler =
    fun next ctx ->
        let result: Result<string, string> = Calculate (ctx.Request.Query.Item "value1", ctx.Request.Query.Item "operation", ctx.Request.Query.Item "value2")
        match result with
        | Ok ok -> (setStatusCode 200 >=> text (ok.ToString())) next ctx
        | Error errorMessage -> (setStatusCode 400 >=> text errorMessage) next ctx
        
let webApp =
    choose [
        GET >=> choose [
             route "/" >=> text "/calculate?value1=<VAL1>&operation=<OPERATION>&value2=<VAL2>"
             route "/calculate" >=> calculatorHandler 
            ]
        setStatusCode 404 >=> text "Not Found" 
    ]
    
type Startup() =
    member _.ConfigureServices (services : IServiceCollection) =
        services.AddGiraffe()
                .AddMiniProfiler(fun option -> option.RouteBasePath <- "/profiler") |> ignore
        
        
    
    member _.Configure (app : IApplicationBuilder) (_ : IHostEnvironment) (_ : ILoggerFactory) =
        app.UseMiniProfiler()
            .UseGiraffe webApp
        
[<EntryPoint>]
let main _ =
    Host
        .CreateDefaultBuilder()
        .ConfigureWebHostDefaults(fun whBuilder -> whBuilder.UseStartup<Startup>() |> ignore)
        .Build()
        .Run()
    0