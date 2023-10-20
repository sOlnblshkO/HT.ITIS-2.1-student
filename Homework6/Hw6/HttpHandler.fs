module Hw6.HttpHandler

open Giraffe
open Hw6.MaybeBuilder
open Hw6.Calculator
open Hw6.Parser
let calculatorHandler: HttpHandler =
    fun next ctx ->
       
        let query = ctx.Request.Query
        let result: Result<string, string> = maybe {
            let! argsParsed = parseCalcArguments([|query.Item "value1" ; query.Item "operation" ; query.Item "value2"|])
            let! calculated = calculate argsParsed
            return calculated
        }

        match result with
        | Ok ok -> (setStatusCode 200 >=> text (ok.ToString())) next ctx
        | Error error -> (setStatusCode 400 >=> text error) next ctx