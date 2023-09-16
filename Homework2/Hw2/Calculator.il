.class public abstract sealed auto ansi beforefieldinit
  Hw2.Calculator extends System.Object
{
    .method static public float64 Calculate(float64 value1, valuetype Hw2.CalculatorOperation operation, float64 value2)
        cil managed
    {
        .locals init (
            [0] float64,
            [1] float64
        )
    
        ldc.i4.1
        brtrue.s IL_0005
    
        IL_0005: ldarg.1
        switch (IL_001d, IL_0023, IL_0029, IL_002f)
    
         br.s IL_0035
    
         IL_001d: ldarg.0
         ldarg.2
         add
         stloc.0
         br.s IL_003b
    
         IL_0023:ldarg.0
         ldarg.2
         sub
         stloc.0
         br.s IL_003b
    
         IL_0029: ldarg.0
         ldarg.2
         mul
         stloc.0
         br.s IL_003b
    
         IL_002f: ldarg.0
         ldarg.2
         div
         stloc.0
         br.s IL_003b
    
        IL_0035: newobj instance void [System.Runtime]System.ArgumentOutOfRangeException::.ctor()
        throw
    
        IL_003b: ldc.i4.1
        brtrue.s IL_003f
    
        IL_003f: ldloc.0
        IL_0040: stloc.1
        IL_0041: br.s IL_0043
    
        IL_0043: ldloc.1
        IL_0044: ret
    }
}



