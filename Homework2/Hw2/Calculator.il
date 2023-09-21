// Type: Hw2.Calculator 
// Assembly: Hw2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 49B24A68-E31C-462E-95A9-4C71A11C59FC
// Location: C:\Users\anton\RiderProjects\HT-ITIS.2.1-student\Homework2\Hw2\bin\Debug\net6.0\Hw2.dll
// No sequence point data available (there is no matching pdb for this assembly)

.class public abstract sealed auto ansi beforefieldinit
  Hw2.Calculator
    extends [System.Runtime]System.Object
{
    .method public hidebysig static float64
    Calculate(
      float64 value1,
      valuetype Hw2.CalculatorOperation operation,
      float64 value2
    ) cil managed
    {
        .maxstack 2
        .locals init (
          [0] float64 a,
          [1] float64 b
        )
        IL_0005: ldarg.1  
        IL_0006: switch       (Plus, Minus, Multiply, Divide)
        IL_001b: br.s         Exception
        
        Plus:
        IL_001d: ldarg.0      
        IL_001e: ldarg.2      
        IL_001f: add
        IL_0020: stloc.0      
        IL_0021: br.s         End
        
        Minus:
        IL_0023: ldarg.0      
        IL_0024: ldarg.2      
        IL_0025: sub
        IL_0026: stloc.0      
        IL_0027: br.s         End
        
        Multiply:
        IL_0029: ldarg.0     
        IL_002a: ldarg.2      
        IL_002b: mul
        IL_002c: stloc.0      
        IL_002d: br.s         End
        
        Divide:
        IL_002f: ldarg.0      
        IL_0030: ldarg.2      
        IL_0031: div
        IL_0032: stloc.0     
        IL_0033: br.s         End
        
        Exception:
        IL_0035: newobj       instance void [System.Runtime]System.InvalidOperationException::.ctor()
        IL_003a: throw
        
        End:
        IL_003f: ldloc.0     
        IL_0040: stloc.1      
        IL_0043: ldloc.1      
        IL_0044: ret
    }
} 
