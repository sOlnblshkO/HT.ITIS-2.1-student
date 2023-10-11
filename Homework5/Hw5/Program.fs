open System
open Hw5

let args = [|"15"; "+"; "25"|]

let object =
    let argsParsed = Parser.parseCalcArguments(args)
    match argsParsed with
    | Ok (arg1, operation, arg2) ->
        let result = Calculator.calculate arg1 operation arg2
        result |> printfn "%f"
    | Error message -> message.ToString() |> printfn "%s" 