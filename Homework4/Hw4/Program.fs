open Hw4
open Hw4.Parser

let args =  [| "1"; "+"; "2" |]

let calcOptions = parseCalcArguments args
let result = Calculator.calculate calcOptions.arg1 calcOptions.operation calcOptions.arg2

printf $"{result}"
