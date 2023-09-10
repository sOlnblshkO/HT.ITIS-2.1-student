using Hw1;

var arg1 = args[0];
var operation = args[1];
var arg2 = args[2];

var arguments = new string[] {arg1, operation, arg2};
Parser.ParseCalcArguments(arguments, out double val1, out CalculatorOperation op, out double val2);
var result = Calculator.Calculate(val1, op, val2);
Console.WriteLine(result);