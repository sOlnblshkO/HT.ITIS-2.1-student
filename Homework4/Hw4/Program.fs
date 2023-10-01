open System
open Hw4

let args = Console.ReadLine().Split(' ')
let parsedArgs = Parser.parseCalcArguments(args)
let result = Calculator.calculate(parsedArgs.arg1)(parsedArgs.operation)(parsedArgs.arg2)
Console.WriteLine(result)