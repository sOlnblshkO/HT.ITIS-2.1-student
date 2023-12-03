module Hw6.CalculatorHandler

open Giraffe

open Hw6.Calculator
open CalcOptions

let calculatorHandler: HttpHandler =
    fun next ctx ->
        let result: Result<string, string> =            
            MaybeBuilder.maybe{
                let! calcOptions = ctx.TryBindQueryString<CalcOptions>()
                let! result = Calculator.calculate (calcOptions.value1,
                                                    calcOptions.operation |> CalculatorOperation.tryToCalculatorOperation,
                                                    calcOptions.value2)   
                return result
            }
            
        match result with
        | Ok ok -> (setStatusCode 200 >=> text (ok.ToString())) next ctx
        | Error error -> (setStatusCode 400 >=> text error) next ctx