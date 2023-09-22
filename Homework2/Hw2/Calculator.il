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
        ldarg.1  
        switch       (Plus, Minus, Multiply, Divide)
        br.s         Exception
        
        Plus:
        ldarg.0      
        ldarg.2      
        add
        stloc.0      
        br.s         End
        
        Minus:
        ldarg.0      
        ldarg.2      
        sub
        stloc.0      
        br.s         End
        
        Multiply:
        ldarg.0     
        ldarg.2      
        mul
        stloc.0      
        br.s         End
        
        Divide:
        ldarg.0      
        ldarg.2      
        div
        stloc.0     
        br.s         End
        
        Exception:
        newobj       instance void [System.Runtime]System.InvalidOperationException::.ctor()
        throw
        
        End:
        ldloc.0     
        stloc.1      
        ldloc.1      
        ret
    }
} 
