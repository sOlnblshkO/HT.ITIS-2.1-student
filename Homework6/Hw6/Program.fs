module Hw6.App

open Hw6
open Microsoft.FSharp.Core
open Parser
open MaybeBuilder
open Calculator
open CalculatorOperation
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection
open Giraffe

let convertOperation input =
    match input with
    | "Plus" -> "+"
    | "Minus" -> "-"
    | "Multiply" -> "*"
    | "Divide" -> "/"
    | _ -> input

let calc (input : Result<(float * CalculatorOperation * float), string>) = 
    match input with
    | Ok parsedData -> Ok (parsedData |||> calculate)
    | Error message -> Error message
        
let calculatorHandler: HttpHandler =
    fun next ctx ->
        let result = maybe {
            let! val1 = ctx.GetQueryStringValue "value1"
            let! operation = ctx.GetQueryStringValue "operation"
            let! val2 = ctx.GetQueryStringValue "value2"
            let args = [|val1 |> string; operation |> convertOperation; val2 |> string;|]
            let! calculation = args |> parseCalcArguments |> calc
            return calculation
        }    
        
        match result with
        | Ok ok -> (setStatusCode 200 >=> text (ok.ToString())) next ctx
        | Error error ->
            if error.Equals("DivideByZero") then (setStatusCode 200 >=> text error) next ctx
            else (setStatusCode 400 >=> text error) next ctx

    
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
        .Run
        ()
    0