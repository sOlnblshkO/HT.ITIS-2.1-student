.class public abstract sealed auto ansi beforefieldinit
  Hw2.Parser
{
    .method public static void ParseCalcArguments(string[] args,
    [out] float64& val1, [out] valuetype Hw2.CalculatorOperation& operation, [out] float64& val2) cil managed
    {
        .locals init (
            [0] bool
        )
    
         ldarg.0
         call bool Hw2.Parser::IsArgLengthSupported(string[])
         ldc.i4.0
         ceq
         stloc.0
         ldloc.0
         brtrue.s exception
    
         ldarg.1
         ldarg.0
         ldc.i4.0
         
         ldelem.ref
         call float64 Hw2.Parser::ParseStringToDouble(string)
         stind.r8
         
         ldarg.2
         ldarg.0
         ldc.i4.1
         
         ldelem.ref
         call valuetype Hw2.CalculatorOperation Hw2.Parser::ParseOperation(string)
         stind.i4
         
         ldarg.3
         ldarg.0
         ldc.i4.2
         
         ldelem.ref
         call float64 Hw2.Parser::ParseStringToDouble(string)
         stind.r8
         ret
         
         exception:
         ldstr "There must be three arguments!"
         newobj instance void [System.Runtime]System.ArgumentException::.ctor(string)
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
    
    .method private static float64 ParseStringToDouble(string arg) cil managed
    {
        .locals init
        (
            [0]float64 number,
            [1]bool,
            [2]float64
        )
        
        ldarg.0
        ldloca.s 0
        call bool [System.Runtime]System.Double::TryParse(string,float64&)
        stloc.1
        ldloc.1
        brfalse.s IL__011
        
        ldloc.0
        stloc.2
        br.s IL__001
        
        IL__011: ldstr "Incorrect numeric entry"
        newobj instance void[System.Runtime]System.ArgumentException::.ctor(string)
        throw
        
        IL__001: ldloc.2
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
        
        exception: ldstr "No such operation exists"
        newobj instance void [System.Runtime]System.InvalidOperationException::.ctor(string)
        throw
    
        result:
        ret 
    }
}


