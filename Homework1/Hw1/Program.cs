using Hw1;


var arguments = new string[] { "1", "+", "1" };
Parser.ParseCalcArguments(arguments, out var value1, out var operation, out var value2);
var result = Calculator.Calculate(value1, operation, value2);
Console.WriteLine(result);