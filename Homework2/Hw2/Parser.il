.class public abstract sealed auto ansi beforefieldinit
  Hw2.Parser extends System.Object
{
    .method private static valuetype Hw2.CalculatorOperation ParseOperation(string arg) cil managed
    {
        ldarg.0
        ldstr "+"
        call bool [System.Runtime]System.String::op_Equality(string, string)
        brtrue.s PlusCase
        
        ldarg.0
        ldstr "-"
        call bool [System.Runtime]System.String::op_Equality(string, string)
        brtrue.s MinusCase
        
        ldarg.0
        ldstr "/"
        call bool [System.Runtime]System.String::op_Equality(string, string)
        brtrue.s DivideCase
        
        ldarg.0
        ldstr "*"
        call bool [System.Runtime]System.String::op_Equality(string, string)
        brtrue.s MultiplyCase
        
        ldc.i4.4
        ret
    
        PlusCase:
        ldc.i4.0
        ret
    
        MinusCase:
        ldc.i4.1
        ret
    
        MultiplyCase:
        ldc.i4.2
        ret
        
        DivideCase:
        ldc.i4.3
        ret
    }
    .method private static bool IsArgLengthSupported(string[] args) cil managed
    {
        ldarg.0
        ldlen
        ldc.i4.3
        ceq
        ret
    }
    .method public static void ParseCalcArguments(string[] args,
        [out] float64& val1,
        [out] valuetype Hw2.CalculatorOperation& op,
        [out] float64& val2) cil managed
    {
        ldarg.0
        call bool Hw2.Parser::IsArgLengthSupported(string[])
        brfalse ArgumentException
        
        ldarg.0
        ldc.i4.1
        ldelem.ref
        call valuetype Hw2.CalculatorOperation Hw2.Parser::ParseOperation(string)
        ldc.i4.4
        ceq
        brtrue InvalidOperationException
        
        ldarg.0 
        ldc.i4.0 
        ldelem.ref 
        ldarg.1 
        call bool [System.Runtime]System.Double::TryParse(string, float64&) 
        brfalse ArgumentException
        
        SecondTryParse:
        ldarg.0
        ldc.i4.2
        ldelem.ref
        ldarg.3
        call bool [System.Runtime]System.Double::TryParse(string, float64&)
        brfalse ArgumentException
        
        CalculationOperationDefining:
        ldarg.2
        ldarg.0
        ldc.i4.1
        ldelem.ref
        call valuetype Hw2.CalculatorOperation Hw2.Parser::ParseOperation(string)
        stind.i4
        ret
        
        InvalidOperationException:
        ldstr "unknown operation"
        newobj instance void [System.Runtime]System.InvalidOperationException::.ctor(string)
        throw
        
        ArgumentException:
        ldstr "Either array length is not 3 or one of the numbers isn't actually a number"
        newobj instance void [System.Runtime]System.ArgumentException::.ctor(string)
        throw
    }    
}


