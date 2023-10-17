module Hw6.App

open System
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection
open Giraffe

let (>>=) a f = Result.bind f a

let parseValue (value: string) =
    let success, parsed = Double.TryParse value

    match success with
    | true -> Ok parsed
    | _ -> Error $"Could not parse value '{value}'"

let parseOperation (value1: double) (value2: double) (operation: string) =
    match operation with
    | "Plus" -> Ok(string (value1 + value2))
    | "Minus" -> Ok(string (value1 - value2))
    | "Multiply" -> Ok(string (value1 * value2))
    | "Divide" when value2 = 0 -> Ok "DivideByZero" // Honestly an error would be more fitting
    | "Divide" -> Ok(string (value1 / value2))
    | _ -> Error $"Could not parse value '{operation}'"

let getFromQuery (key: string) (ctx: HttpContext) =
    let strings = ctx.Request.Query[key]

    match strings.Count with
    | 0 -> Error $"'{key}' not present in a query"
    | c -> Ok strings[c - 1]

let collectValue1 ctx =
    ctx |> getFromQuery "value1" >>= parseValue

let collectValue2 ctx value1 =
    match ctx |> getFromQuery "value2" >>= parseValue with
    | Ok value2 -> Ok(value1, value2)
    | Error error -> Error error

let collectOperation ctx (value1, value2) =
    ctx |> getFromQuery "operation" >>= parseOperation value1 value2


let calculatorHandler: HttpHandler =
    fun next ctx ->
        let result = collectValue1 ctx >>= collectValue2 ctx >>= collectOperation ctx

        match result with
        | Ok ok -> (setStatusCode 200 >=> text (ok.ToString())) next ctx
        | Error error -> (setStatusCode 400 >=> text error) next ctx

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
