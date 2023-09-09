using Hw1;

try
{
    Parser.ParseCalcArguments(args, out var val1, out var operation, out var val2);
    Console.WriteLine(Calculator.Calculate(val1, operation, val2));
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}
