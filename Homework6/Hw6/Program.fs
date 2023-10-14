module Hw6.App

open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection
open Giraffe

[<CLIMutable>]
type calcArgs =
    { value1: double
      operation: string
      value2: double }

[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let calculate (value1: double, operation: string, value2: double) =
    match operation with
    | "+" -> Ok $"{value1 + value2}"
    | "-" -> Ok $"{value1 - value2}"
    | "*" -> Ok $"{value1 * value2}"
    | "/" -> if value2 <> 0.0 then Ok $"{value1 / value2}"
             else Ok "DivideByZero"
    | _ -> Error $"Could not parse value '{operation}'"

type MaybeBuilder() =
    member this.Bind(a, f): Result<'e,'d> =
        match a with
        | Ok ok -> f ok
        | Error err -> Error err
    member this.Return x: Result<'a,'b> =
        Ok x
        
let maybe = MaybeBuilder()

let parseOperation operation =
    match operation with
    | "Plus" -> "+"
    | "Minus" -> "-"
    | "Multiply" -> "*"
    | "Divide" -> "/"
    | _ -> operation
    
let calculatorHandler : HttpHandler =
    fun next ctx ->
        let result = maybe {
            let! args = ctx.TryBindQueryString<calcArgs>()
            let! parsed = calculate (args.value1, parseOperation args.operation, args.value2)
            return parsed
        }
        match result with
        | Ok ok -> (setStatusCode 200 >=> text (ok.ToString())) next ctx
        | Error error -> (setStatusCode 400 >=> text error) next ctx

let webApp =
    choose [
        GET >=> choose [
             route "/calculate" >=> calculatorHandler
             route "/" >=> text "Use //calculate?value1=<VAL1>&operation=<OPERATION>&value2=<VAL2>"
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