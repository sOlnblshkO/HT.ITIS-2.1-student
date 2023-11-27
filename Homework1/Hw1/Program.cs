using Hw1;

var arg1 = args[0];
var operation = args[1];
var arg2 = args[2];

Parser.ParseCalcArguments(new []{arg1, operation, arg2},
    out var val1,
    out var oper,
    out var val2);

var result = Calculator.Calculate(val1, oper, val2);

Console.WriteLine(result);