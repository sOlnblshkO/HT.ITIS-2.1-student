open System
open Hw5

[<EntryPoint>]

while true do
    let input = Console.ReadLine()
    let strings = input.Split ' '
    let args = Parser.parseCalcArguments strings

    match args with
    | Error e -> Console.WriteLine(e)
    | Ok (val1, op, val2) -> Console.WriteLine(Calculator.calculate val1 op val2)