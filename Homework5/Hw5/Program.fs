open System
open Hw5
open Microsoft.FSharp.Core

[<EntryPoint>]
let main args =
    match Parser.parseCalcArguments args with
    | Ok (arg1, operation, arg2) -> Calculator.calculate arg1 operation arg2 |> printfn "%f"
    | Error errorMsg ->
        match errorMsg with
        | Message.WrongArgLength -> "Length should be 3"
        | Message.WrongArgFormat -> "arg parse error"
        | Message.WrongArgFormatOperation -> "operation is not supported"
        | Message.DivideByZero -> "division by 0"
        | _ -> "unknown error"
        |> printfn "error occured: %s"


    0