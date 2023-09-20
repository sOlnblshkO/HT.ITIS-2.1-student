.class public abstract sealed auto ansi beforefieldinit
  Hw2.Parser
{
    .method static public void ParseCalcArguments(string[] args,
    [out] float64& val1,
    [out] valuetype Hw2.CalculatorOperation& operation,
    [out] float64& val2)
    {
        ldarg args
        brfalse ArgsIsNull
        
        ldarg args
        call bool Hw2.Parser::IsArgLengthSupported(string[])
        brfalse ArgsWrongLength
        

        ldarg args
        ldc.i4.0
        ldelem.ref
        ldarg val1
        call bool float64::TryParse(string, float64&)
        brfalse IncorrectValue
        
        ldarg args
        ldc.i4.2
        ldelem.ref
        ldarg val2
        call bool float64::TryParse(string, float64&)
        brfalse IncorrectValue
        
        ldarg operation
        ldarg args
        ldc.i4.1
        ldelem.ref
        call valuetype Hw2.CalculatorOperation Hw2.Parser::ParseOperation(string)
        stind.i1
  
        ret
        
        ArgsIsNull:
        ldstr "Пустой ввод"
        ldstr "args"
        newobj instance void [System.Runtime]System.ArgumentNullException::.ctor(string, string)
        throw
        
        ArgsWrongLength:
        ldstr "Некорректное количество аргументов"
        newobj instance void [System.Runtime]System.ArgumentException::.ctor(string)
        throw
        
        IncorrectValue:
        ldstr "Некорректные значения"
        newobj instance void [System.Runtime]System.ArgumentException::.ctor(string)
        throw
    }
    
    .method static private bool IsArgLengthSupported(string[] args)
     {
        ldarg args
        ldlen
        ldc.i4.3
        ceq
        ret 
     }

     .method static private valuetype Hw2.CalculatorOperation ParseOperation(string arg)
     {
        ldarg arg
        ldstr "+"
        call bool string::Equals(string, string)
        brtrue Plus
        
        ldarg arg
        ldstr "-"
        call bool string::Equals(string, string)
        brtrue Minus
        
        ldarg arg
        ldstr "*"
        call bool string::Equals(string, string)
        brtrue Multiply
        
        ldarg arg
        ldstr "/"
        call bool string::Equals(string, string)
        brtrue Divide
        
        ldstr "Некорректная операция"
        newobj instance void [System.Runtime]System.InvalidOperationException::.ctor(string)
        throw
        
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


