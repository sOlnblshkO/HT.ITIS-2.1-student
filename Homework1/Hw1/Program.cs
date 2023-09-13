using Hw1;

while (true)
{
    var input = Console.ReadLine()?.Split(' ');
    try
    {
        Parser.ParseCalcArguments(input, out var val1, out var operation, out var val2);
        var result = Calculator.Calculate(val1, operation, val2);
        Console.WriteLine(result);
    }
    catch (ArgumentException exception)
    {
        Console.WriteLine(exception.Message);
    }
    catch (InvalidOperationException exception)
    {
        Console.WriteLine(exception.Message);
    }
}