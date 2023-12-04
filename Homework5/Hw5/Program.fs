open System
open Hw5
open Hw5.MaybeBuilder
open Microsoft.FSharp.Core

printf "Enter expression like 3 * 2\n"
let args = Console.ReadLine().Split(" ")

let result =
    maybe {
        let! calcOptions = Parser.parseArgs args
        match calcOptions with (arg1, operation, arg2) ->
            let res = Calculator.calculate arg1 operation arg2
            return res
}

printfn "%A" result