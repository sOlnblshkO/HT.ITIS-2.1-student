module Hw6.App

open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection
open Giraffe

open Hw6.WebCalculator

let (>>=) a f = Result.bind f a

let calculatorHandler: HttpHandler =
    fun next ctx ->
        let result = ctx.TryBindQueryString<CalculatorModel>() >>= calculate

        match result with
        | Ok ok -> (setStatusCode 200 >=> text (ok.ToString())) next ctx
        | Error error -> (setStatusCode 400 >=> text (error.Replace("System.Double", "double"))) next ctx

let webApp =
    choose
        [ GET
          >=> choose
                  [ route "/"
                    >=> text "Use /calculate?value1=<VAL1>&operation=<OPERATION>&value2=<VAL2>"
                    route "/calculate" >=> calculatorHandler ]
          setStatusCode 404 >=> text "Not Found" ]

type Startup() =
    member _.ConfigureServices(services: IServiceCollection) = services.AddGiraffe() |> ignore

    member _.Configure (app: IApplicationBuilder) (_: IHostEnvironment) (_: ILoggerFactory) = app.UseGiraffe webApp

[<EntryPoint>]
let main _ =
    Host
        .CreateDefaultBuilder()
        .ConfigureWebHostDefaults(fun whBuilder -> whBuilder.UseStartup<Startup>() |> ignore)
        .Build()
        .Run()

    0
