.class public abstract sealed auto ansi beforefieldinit
  Hw2.Calculator
{
    .method public static float64 Calculate(
        float64 value1, 
        valuetype Hw2.CalculatorOperation operation, 
        float64 value2
    ) cil managed
    {
        ldarg.1
        switch (Plus, Minus, Multiply, Divide)
        newobj instance void [System.Runtime]System.ArgumentOutOfRangeException::.ctor()
        throw 
        
        Plus:
        ldarg.0 
        ldarg.2
        add
        ret
        
        Minus:
        ldarg.0
        ldarg.2
        sub
        ret
        
        Multiply:
        ldarg.0
        ldarg.2
        mul
        ret
        
        Divide: 
        ldarg.0
        ldarg.2
        div
        ret
    }
}



