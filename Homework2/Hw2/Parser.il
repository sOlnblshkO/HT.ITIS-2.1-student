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
        brfalse.s IL_0019
    
       ldstr "There must be three arguments!"
         newobj instance void [System.Runtime]System.ArgumentException::.ctor(string)
         throw
    
        IL_0019: ldarg.1
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
        .locals init (
            [0] valuetype Hw2.CalculatorOperation,
            [1] valuetype Hw2.CalculatorOperation
        )
    
        ldc.i4.1
        brtrue.s IL_0005
    
        IL_0005: ldarg.0
        ldstr "+"
        call bool [System.Runtime]System.String::op_Equality(string, string)
        brtrue.s IL_003b
    
        ldarg.0
        ldstr "-"
        call bool [System.Runtime]System.String::op_Equality(string, string)
        brtrue.s IL_003f
    
        ldarg.0
        ldstr "/"
        call bool [System.Runtime]System.String::op_Equality(string, string)
        brtrue.s IL_0043
    
        ldarg.0
        ldstr "*"
        call bool [System.Runtime]System.String::op_Equality(string, string)
        brtrue.s IL_0047
    
        br.s IL_004b
    
        IL_003b: ldc.i4.0
        stloc.0
        br.s IL_0056
    
        IL_003f: ldc.i4.1
        stloc.0
        br.s IL_0056
    
        IL_0043: ldc.i4.3
        stloc.0
        br.s IL_0056
    
        IL_0047: ldc.i4.2
        stloc.0
        br.s IL_0056
    
        IL_004b: ldstr "No such operation exists"
        newobj instance void [System.Runtime]System.InvalidOperationException::.ctor(string)
        throw
    
        IL_0056: ldc.i4.1
        brtrue.s IL_005a
    
        IL_005a: ldloc.0
        stloc.1
        br.s IL_005e
    
        IL_005e: ldloc.1
        ret
    }
}


