.class public abstract sealed auto ansi beforefieldinit
  Hw2.Calculator
{
    .method public static float64 Calculate(
        float64 value1,
        valuetype Hw2.CalculatorOperation operation,
        float64 value2) 
    {
        ldarg.1
        ldc.i4 0
        beq plus
        
        ldarg.1
        ldc.i4 1
        beq minus
        
        ldarg.1
        ldc.i4 2
        beq multiply
        
        ldarg.1
        ldc.i4 3
        beq divide
        
        newobj instance void [mscorlib]System.InvalidOperationException::.ctor()
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