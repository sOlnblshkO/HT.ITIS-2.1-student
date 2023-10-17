module Hw6.App

open System
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection
open Giraffe
open Microsoft.AspNetCore.Http
open Hw6.MaybeBuilder
open Hw6.Parser
open Hw6.Calculator

let getValuesFromRequest(context:HttpContext):string[] = 
    let query = context.Request.Query    
    let operation = query["operation"].ToString()
    let value1 = query["value1"].ToString()
    let value2 = query["value2"].ToString()
    [|value1;operation;value2|]

let calculatorHandler : HttpHandler =
    fun next ctx ->
        let result: Result<string, Message> = 
            maybe
                {
                    let args = getValuesFromRequest ctx
                    let! parsedArgs = parseCalcArguments args
                    let result = calculate parsedArgs
                    return result.ToString()
                }

        match result with
        | Ok ok -> (setStatusCode 200 >=> text (ok.ToString())) next ctx
        | Error error -> (setStatusCode 400 >=> text (error.ToString())) next ctx

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