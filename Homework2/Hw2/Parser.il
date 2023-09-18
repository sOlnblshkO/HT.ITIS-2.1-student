.class public abstract sealed auto ansi beforefieldinit
  Hw2.Parser
{
    .method public static void ParseCalcArguments(string[] args,
    [out] float64&, [out] valuetype Hw2.CalculatorOperation&, [out] float64&) cil managed
    {
        .maxstack 4

        ldarg.0
        call bool Hw2.Parser::IsArgLengthSupported(string[])
        brfalse.s ArgsIsNotCorrect
    
        ldarg.0
        ldc.i4.0
        ldelem.ref 
        ldarg.1
        call bool [System.Runtime]System.Double::TryParse(string, float64&)
        brfalse.s IsNotDoubleFirstCheck
    
        ldarg.2
        ldarg.0
        ldc.i4.1
        ldelem.ref
        call valuetype Hw2.CalculatorOperation Hw2.Parser::ParseOperation(string)
        dup
        ldc.i4.4
        ceq
        brtrue.s IsNotCorrectOperation
        stind.i4
    
        ldarg.0
        ldc.i4.2
        ldelem.ref
        ldarg.3
        call bool [System.Runtime]System.Double::TryParse(string, float64&)
        brfalse.s IsNotDoubleSecondCheck
        
        br.s Result
    
        IsNotDoubleSecondCheck:
        ldstr "Second number is not currect"
        newobj instance void [System.Runtime]System.ArgumentException::.ctor(string)
        throw
        
        IsNotDoubleFirstCheck:
        ldstr "First number is not currect"
        newobj instance void [System.Runtime]System.ArgumentException::.ctor(string)
        throw
        
        IsNotCorrectOperation:
        ldstr "Invalid operation"
        newobj instance void [System.Runtime]System.InvalidOperationException::.ctor(string)
        throw
        
        ArgsIsNotCorrect:
        ldstr "Inccorect number of args"
        newobj instance void [System.Runtime]System.ArgumentException::.ctor(string)
        throw
        
        Result: 
        ret
    }
    
    .method private static bool IsArgLengthSupported(string[] args) cil managed
    {
        ldarg.0
        ldlen
        conv.i4
        ldc.i4.3
        ceq
        ret
    }
    
    
    .method private static valuetype Hw2.CalculatorOperation ParseOperation(string arg) cil managed
    {
        .maxstack 2
    
        ldarg.0
        ldstr "+"
        call bool [System.Runtime]System.String::op_Equality(string, string)
        brtrue.s plus
    
        ldarg.0
        ldstr "-"
        call bool [System.Runtime]System.String::op_Equality(string, string)
        brtrue.s minus
    
        ldarg.0
        ldstr "/"
        call bool [System.Runtime]System.String::op_Equality(string, string)
        brtrue.s divide
    
        ldarg.0
        ldstr "*"
        call bool [System.Runtime]System.String::op_Equality(string, string)
        brtrue.s multiply
  
        br.s exception
    
        plus:
        ldc.i4.0
        br.s result
    
        minus:
        ldc.i4.1
        br.s result
        
        divide:
        ldc.i4.3
        br.s result
    
        multiply: 
        ldc.i4.2
        br.s result
        
        exception:
        ldc.i4.4
        br.s result
        
        result:
        ret 
    }
}


