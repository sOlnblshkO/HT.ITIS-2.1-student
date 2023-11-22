using System.Text;

namespace Hw10.Services.MathCalculator.ParserAndValidator;


public static class Parser
{
    public static string ConvertToPostfixForm(string input)
    {
        var output = new StringBuilder();
        var operStack = new Stack<char>();
        var minus = false;

        for (int i = 0; i < input.Length; i++) 
        {
            if (Char.IsDigit(input[i]))
            {
                var number = new StringBuilder();
                while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
                {
                    number.Append(input[i]);
                    if (++i == input.Length)
                        break; 
                }
                if (minus) 
                    output.Append($"-{number}");
                else 
                    output.Append(number) ;
                output.Append(" ");
                i--;
                minus = false;
            }
            
            if (IsOperator(input[i])) 
            {
                if (input[i] == '(')
                {
                    operStack.Push(input[i]);
                    minus = false;
                }
                    
                else if (input[i] == ')') 
                {
                    char s = operStack.Pop();

                    while (s != '(')
                    {
                        output.Append(s.ToString() + ' ') ;
                        s = operStack.Pop();
                    }

                    minus = false;
                }
                else 
                {
                    if (input[i] == '-')
                    {
                        if (i == 0 || input[i - 1] == '(')
                            minus = true;
                        else
                        {
                            if (operStack.Count > 0)
                                if (GetPriority(input[i]) <= GetPriority(operStack.Peek())) 
                                    output.Append(operStack.Pop().ToString() + " ")  ;
                            operStack.Push(char.Parse(input[i].ToString()));
                        }
                    }
                    else
                    {
                        if (operStack.Count > 0)
                            if (GetPriority(input[i]) <= GetPriority(operStack.Peek())) 
                                output.Append(operStack.Pop().ToString() + " ")  ;
                        operStack.Push(char.Parse(input[i].ToString()));
                        minus = false;
                    }
                }
            }
        }
        while (operStack.Count > 0)
            output.Append(operStack.Pop() + " ") ;

        return output.ToString();
    }
    
    private static byte GetPriority(char s)
    {
        return s switch
        {
            '+' => 2,
            '-' => 3,
            '*' => 4,
            '/' => 4,
            _ => 0
        };
    }
    private static bool IsDelimeter(char c) => " =".Contains(c);
    
    private static bool IsOperator(char с) => "+-/*^()".Contains(с);
    
}