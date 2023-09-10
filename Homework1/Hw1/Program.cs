using Hw1;

var array = new[] { "15", "+", "5" };

// TODO: implement calculator logic a

Parser.ParseCalcArguments(array, out var arg1, out var operation, out var arg2);

var result = Calculator.Calculate(arg1, operation, arg2) ;

Console.WriteLine(result);