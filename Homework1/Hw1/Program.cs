using Hw1;

var arg1 = args[0];
var operation = args[1];
var arg2 = args[2];

// TODO: implement calculator logic
var result = Calculator.Calculate(double.Parse(arg1), CalculatorOperation.Plus, double.Parse(arg2));
Console.WriteLine(result);