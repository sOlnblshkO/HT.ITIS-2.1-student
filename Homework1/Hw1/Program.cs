using Hw1;

try
{
    Parser.ParseCalcArguments(args, out var firstValue, out var operation, out var secondValue);
    Console.WriteLine(Calculator.Calculate(firstValue, operation, secondValue));
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}