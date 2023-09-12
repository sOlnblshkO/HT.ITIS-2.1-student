using Hw1;

while (true)
{
    var input = Console.ReadLine()?.Split(' ');
    Parser.ParseCalcArguments(input, out var val1, out var operation, out var val2);
    var result = Calculator.Calculate(val1, operation, val2);
    Console.WriteLine(result);
}