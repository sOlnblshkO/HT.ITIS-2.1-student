.class public abstract sealed auto ansi beforefieldinit
  Hw2.Parser
{
    .method public static void ParseCalcArguments(
        string[] args,
        [out] float64& val1,
        [out] valuetype Hw2.CalculatorOperation& operation,
        [out] float64& val2
    ) cil managed
    {
        ldarg.0
        call bool Hw2.Parser::IsArgLengthSupported(string[])
        ldc.i4.0
        beq ArgLengthNotSupported
        
        ldarg.0
        ldc.i4.0
        ldelem.ref
        ldarg.1
        call bool [System.Runtime]System.Double::TryParse(string, float64&)
        brfalse NotNumber
        
        ldarg.0
        ldc.i4.2
        ldelem.ref
        ldarg.3
        call bool [System.Runtime]System.Double::TryParse(string, float64&)
        brfalse NotNumber
        
        ldarg.2
        ldarg.0
        ldc.i4.1
        ldelem.ref 
        call valuetype Hw2.CalculatorOperation Hw2.Parser::ParseOperation(string)
        dup
        ldc.i4.4
        ceq
        brtrue UndefinedOperation
        stind.i4
        ret
        
        ArgLengthNotSupported:
        ldstr "Неправильное количество аргументов"
        newobj instance void [System.Runtime]System.ArgumentException::.ctor(string)
        throw
        
        NotNumber:
        ldstr "Даны не числа"
        newobj instance void [System.Runtime]System.ArgumentException::.ctor(string)
        throw
        
        UndefinedOperation:
        ldstr "Дана неправильная операция"
        newobj instance void [System.Runtime]System.InvalidOperationException::.ctor(string)
        throw
        
    }
    
    .method private static bool IsArgLengthSupported(
        string[] args
    ) cil managed
    {
        ldarg.0
        ldlen 
        ldc.i4.3
        ceq
        ret
    }
    
    .method private static valuetype Hw2.CalculatorOperation ParseOperation(
        string arg
    ) cil managed
    {
        ldarg.0
        ldstr "+"
        call bool [System.Runtime]System.String::Equals(string, string)
        brtrue.s Plus
        
        ldarg.0
        ldstr "-"
        call bool [System.Runtime]System.String::Equals(string, string)
        brtrue.s Minus
        
        ldarg.0
        ldstr "*"
        call bool [System.Runtime]System.String::Equals(string, string)
        brtrue.s Multiply
        
        ldarg.0
        ldstr "/"
        call bool [System.Runtime]System.String::Equals(string, string)
        brtrue.s Divide
        
        // Undefined
        ldc.i4.4
        ret
        
        Plus:
        ldc.i4.0 
        ret
        
        Minus:
        ldc.i4.1
        ret
        
        Multiply:
        ldc.i4.2
        ret
        
        Divide:
        ldc.i4.3
        ret
    }
}


