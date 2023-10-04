open Hw4
open System
[<EntryPoint>]

while true do
    try
        let input = Console.ReadLine()
        let strings = input.Split ' '
        let args = Parser.parseCalcArguments strings
        let result = Calculator.calculate args.arg1 args.operation args.arg2;
        Console.WriteLine(result)
    with
    | :? ArgumentException as ex -> printfn $"{ex.Message}"
    | :? InvalidOperationException as ex -> printfn $"{ex.Message}"