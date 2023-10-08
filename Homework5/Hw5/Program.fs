open Hw5

[<EntryPoint>]
let main args =
    match Parser.parseCalcArguments args with
    | Ok (arg1, operation, arg2) -> Calculator.calculate arg1 operation arg2 |> printfn "%f"
    | Error errorValue ->
        match errorValue with
        | Message.DivideByZero -> "Division by zero"
        | Message.WrongArgFormat -> "Parsing error"
        | Message.WrongArgLength -> "Wrong number of arguments"
        | Message.WrongArgFormatOperation -> "Operation not supported"
        | _ -> "Unknown error"
        |> printfn "Error: %s"


    0
