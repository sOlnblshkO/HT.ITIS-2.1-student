.class public abstract sealed auto ansi beforefieldinit
  Hw2.Calculator 
{
    .method public static float64
    Calculate(
      float64 value1,
      valuetype Hw2.CalculatorOperation operation,
      float64 value2
    ) cil managed
    {
        ldarg.0
        ldarg.2
        ldarg.1
        ldc.i4.0
        beq.s Plus

        ldarg.1
        ldc.i4.1
        beq.s Minus

        ldarg.1
        ldc.i4.2
        beq.s Multiply

        ldarg.1
        ldc.i4.3
        beq.s Divide

        ldarg.1
        ldc.i4.4
        beq.s Undefined

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
        pop
        newobj instance void [System.Runtime]System.InvalidOperationException::.ctor()
        throw
    }
}