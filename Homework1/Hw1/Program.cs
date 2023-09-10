using Hw1;

try
{
    Parser.ParseCalcArguments(args, out var firstNum, out var operation, out var secondNum);
    Console.WriteLine(Calculator.Calculate(firstNum, operation, secondNum));
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}