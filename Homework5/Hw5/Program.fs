open System
open Hw5
open Hw5.Parser
open Hw5.Calculator

let createMessage message =
    match message with
    | Message.WrongArgLength -> "You have to pass 3 arguments"
    | Message.WrongArgFormat -> "Argument could not be converted to the double type"
    | Message.WrongArgFormatOperation -> "Unknown operation"
    | Message.DivideByZero -> "Divide by zero"
    | Message.SuccessfulExecution -> "Well done"
    | _ -> "Unknown error"

[<EntryPoint>]
let Main args =
    match parseCalcArguments args with
    | Ok (arg1,operation,arg2) -> calculate arg1 operation arg2 |> printfn "%f"
    | Error message -> createMessage message |> printfn "%s"
    0
    


        