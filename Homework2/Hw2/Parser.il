.class public abstract sealed auto ansi beforefieldinit
  Hw2.Parser
{
    .method public hidebysig static void
        ParseCalcArguments(
          string[] args,
          [out] float64& val1,
          [out] valuetype Hw2.CalculatorOperation& operation,
          [out] float64& val2
        ) cil managed
    {
        ldarg.0
        call void Hw2.Parser::IsArgLengthSupported(string[])
        ldarg.0
        call void Hw2.Parser::ParseArgument(string[])
        
        ldarg.1
        ldarg.0
        ldc.i4 0
        ldelem.ref
        call float64 [System.Runtime]System.Double::Parse(string)
        stind.r8
        ldarg.3
        ldarg.0
        ldc.i4 2
        ldelem.ref
        call float64 [System.Runtime]System.Double::Parse(string)
        stind.r8
        ldarg.2
        ldarg.0
        ldc.i4 1
        ldelem.ref
        call valuetype Hw2.CalculatorOperation Hw2.Parser::ParseOperation(string)
        stind.i4
        ret
    }
        
   .method private static void ParseArgument(string[] args)
    {
        .locals init (float64 outValue)
            
        ldarg.0
        ldc.i4.0
        ldelem.ref
        ldloca.s outValue
        call bool [mscorlib]System.Double::TryParse(string, float64&)
        brfalse exc
        ldarg.0
        ldc.i4.2
        ldelem.ref
        ldloca.s outValue
        call bool [mscorlib]System.Double::TryParse(string, float64&)
        brfalse exc
        ret
        
        exc:
        ldstr "Wrong args"
        newobj instance void [System.Runtime]System.ArgumentException::.ctor(string)
        throw
    }
        
   .method private static void IsArgLengthSupported(string[] args)
    {
        ldarg.0
        ldlen
        ldc.i4 3
        beq res
        ldstr "Wrong args length"
        newobj instance void [System.Runtime]System.ArgumentException::.ctor(string)
        throw
        
        res:
        ret
    }
    
    .method private static valuetype Hw2.CalculatorOperation ParseOperation(string arg)
    {
        ldarg.0
        ldstr "+"    
        call bool [mscorlib]System.String::Equals(string, string)
        brtrue return_plus
    
        ldarg.0
        ldstr "-"    
        call bool [mscorlib]System.String::Equals(string, string)
        brtrue return_minus
    
        ldarg.0
        ldstr "*"    
        call bool [mscorlib]System.String::Equals(string, string)
        brtrue return_multiply
    
        ldarg.0
        ldstr "/"    
        call bool [mscorlib]System.String::Equals(string, string)
        brtrue return_divide
    
        ldstr "Wrong operation"
        newobj instance void [System.Runtime]System.InvalidOperationException::.ctor(string)
        throw
    
        return_plus:
        ldc.i4 0
        ret
    
        return_minus:
        ldc.i4 1
        ret
    
        return_multiply:
        ldc.i4 2
        ret
    
        return_divide:
        ldc.i4 3
        ret
    }
}


