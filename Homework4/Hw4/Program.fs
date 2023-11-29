open System
open Hw4
open Hw4.CalcOptions
open Hw4.Parser

let args = Array.create 3 ""

Array.set args 0 (Console.ReadLine())
Array.set args 1 (Console.ReadLine())
Array.set args 2 (Console.ReadLine())

let calcOptions = parseCalcArguments args
let result = Calculator.calculate calcOptions.arg1 calcOptions.operation calcOptions.arg2

printf $"{result}"
