open System
open Hw5
open Hw5.MaybeBuilder
open Microsoft.FSharp.Core

let args = Array.create 3 ""

Array.set args 0 (Console.ReadLine())
Array.set args 1 (Console.ReadLine())
Array.set args 2 (Console.ReadLine())

let result =
    maybe {
        let! calcOptions = Parser.parseArgs args
        match calcOptions with (arg1, operation, arg2) ->
            let res = Calculator.calculate arg1 operation arg2
            return res
}

printfn "%A" result