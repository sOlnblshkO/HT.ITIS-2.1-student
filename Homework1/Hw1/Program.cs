using Hw1;

Parser.ParseCalcArguments(args,
    out var val1,
    out var calculatorOperation,
    out var val2);
Console.WriteLine(Calculator.Calculate(val1,calculatorOperation,val2));