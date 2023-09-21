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
        .locals init (bool a)
        
        ldarg.0      
        call         bool Hw2.Parser::IsArgLengthSupported(string[])
        ldc.i4.0
        ceq
        stloc.0      
        
        ldloc.0      
        brfalse.s    Complete
        
        ldstr        "Wrong number of arguments"
        newobj       instance void [System.Runtime]System.ArgumentException::.ctor(string)
        throw
        
        Complete:
        ldarg.1     
        ldarg.0     
        ldc.i4.0
        ldelem.ref
        call         float64 Hw2.Parser::ParseNumber(string)
        stind.r8
        
        ldarg.3      
        ldarg.0      
        ldc.i4.2
        ldelem.ref
        call         float64 Hw2.Parser::ParseNumber(string)
        stind.r8
        
        ldarg.2     
        ldarg.0     
        ldc.i4.1
        ldelem.ref
        call         valuetype Hw2.CalculatorOperation Hw2.Parser::ParseOperation(string)
        stind.i4
        
        ret
    }
  
    .method private static float64 ParseNumber(string number) cil managed
    {
        .maxstack 2
        .locals init (
          [0] float64 result,
          [1] bool,
          [2] float64
        )
        ldarg.0   
        ldloca.s     result
        call         bool [System.Runtime]System.Double::TryParse(string, float64&)
        stloc.1      
    
        ldloc.1     
        brfalse.s    ParseFailed
    
        ldloc.0      
        stloc.2      
        ldloc.2     
        ret
    
        ParseFailed:
        ldstr        "Incorrect number: "
        ldarg.0
        call         string [System.Runtime]System.String::Concat(string, string)
        newobj       instance void [System.Runtime]System.ArgumentException::.ctor(string)
        throw
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
        .locals init (
          [0] valuetype Hw2.CalculatorOperation
        )
        
        ldarg.0  
        ldstr        "+"
        call         bool [System.Runtime]System.String::op_Equality(string, string)
        brtrue.s     Plus
        ldarg.0      
        ldstr        "-"
        call         bool [System.Runtime]System.String::op_Equality(string, string)
        brtrue.s     Minus
        ldarg.0      
        ldstr        "*"
        call         bool [System.Runtime]System.String::op_Equality(string, string)
        brtrue.s     Multiply
        ldarg.0     
        ldstr        "/"
        call         bool [System.Runtime]System.String::op_Equality(string, string)
        brtrue.s     Divide
        br.s         Default
        
        Plus:
        ldc.i4.0
        stloc.0      
        br.s         End
        
        Minus:
        ldc.i4.1
        stloc.0      
        br.s         End
        
        Multiply:
        ldc.i4.2
        stloc.0      
        br.s         End
        
        Divide:
        ldc.i4.3
        stloc.0      
        br.s         End
        
        Default:
        ldstr        "Incorrect operation: "
        ldarg.0      
        call         string [System.Runtime]System.String::Concat(string, string)
        newobj       instance void [System.Runtime]System.InvalidOperationException::.ctor(string)
        throw
        
        End:
        ldloc.0      
        ret
  }
}


