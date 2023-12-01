open System
open Hw4
open Hw4.CalcOptions
open Hw4.Parser

printf "Enter expression like 1 + 2\n"
let args = Console.ReadLine().Split(" ")

let calcOptions = parseCalcArguments args
let result = Calculator.calculate calcOptions.arg1 calcOptions.operation calcOptions.arg2

printf $"{result}"
