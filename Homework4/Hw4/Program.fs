open Hw4
open Hw4.Parser

let args: string[] = [|"3"; "+"; "4"|]
let parsedArguments = Parser.parseCalcArguments(args)

printfn $"%f{Calculator.calculate parsedArguments.arg1 parsedArguments.operation parsedArguments.arg2}"
