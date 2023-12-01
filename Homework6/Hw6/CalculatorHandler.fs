module Hw6.CalculatorHandler

open Giraffe

open Hw6.Calculator
open Hw6.Parser
open MaybeBuilder

let calculatorHandler: HttpHandler =
    fun next ctx ->
        let result: Result<string, string> =
            let queryParams = ctx.TryBindQueryString<string[]>()
            MaybeBuilder.maybe{
                let! parsedArgs = Parser.parseCalcArguments queryParams
                let! result = Calculator.calculate parsedArgs[0] parseArgs[1] parseArgs[1]
                
                return result
            }
        match result with
        | Ok ok -> (setStatusCode 200 >=> text (ok.ToString())) next ctx
        | Error error -> (setStatusCode 400 >=> text error) next ctx