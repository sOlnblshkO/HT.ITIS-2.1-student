.class public abstract sealed auto ansi beforefieldinit
  Hw2.Calculator
{
    .method public static float64 Calculate(float64, valuetype Hw2.CalculatorOperation, float64) cil managed
    {
        .maxstack 3
        ldarg.0
        ldarg.2
        ldarg.1
        switch (Plus, Minus, Multiply, Divide, Undefined)
        
        Plus:       
            add
            ret
        Minus:
            sub
            ret
        Multiply:
            mul
            ret
        Divide:
            div
            ret
        Undefined:
            ldstr "Wrong operation"
            newobj instance void [System.Runtime]System.ArgumentOutOfRangeException::.ctor(string)
            throw
    }
}



