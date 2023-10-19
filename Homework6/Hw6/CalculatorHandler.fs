module Hw6.CalculatorHandler

open Giraffe
open MaybeBuilder
open Calculator

let calculatorHandler : HttpHandler =
    fun next ctx ->
        let result = maybe {
            let! args = ctx.TryBindQueryString<CalcArgs>()
            let! parsed = calculate(args.value1, parseOperation args.operation, args.value2)
            return parsed
        }
        match result with
        | Ok ok -> (setStatusCode 200 >=> text (ok.ToString())) next ctx
        | Error error -> (setStatusCode 400 >=> text error) next ctx