open System
open Hw5
open Hw5.Calculator
open Microsoft.FSharp.Core

let args = Console.ReadLine().Split(' ')

let result = Parser.parseCalcArguments args

let f = 
    match result with
    | Ok successValue ->
        match successValue with
        | (arg1, operation, arg2) ->
            Console.WriteLine(calculate arg1 operation arg2)
    | Error message ->
        Console.WriteLine($"Oooops, error: {message}")




      