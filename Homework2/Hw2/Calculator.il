.class public abstract sealed auto ansi beforefieldinit
  Hw2.Calculator extends System.Object
{
    .method public static float64 Calculate(float64 val1, valuetype Hw2.CalculatorOperation op, float64 val2)
    {
        ldarg.1
        switch(
            plus,
            minus,
            multiply,
            divide
        )
        newobj instance void [mscorlib]System.ArgumentOutOfRangeException::.ctor()
        throw
        
        plus:
        ldarg.0
        ldarg.2
        add
        ret
        
        minus:
        ldarg.0
        ldarg.2
        sub
        ret
        
        multiply:
        ldarg.0
        ldarg.2
        mul
        ret
        
        divide:
        ldarg.0
        ldarg.2
        div
        ret
    }
}



