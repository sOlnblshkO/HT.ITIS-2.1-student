.class public abstract sealed auto ansi beforefieldinit
  Hw2.Calculator
{
    .method public static float64 Calculate(float64, valuetype Hw2.CalculatorOperation, float64) cil managed
    {
        .maxstack 3

        Switch:
        ldarg.0
        ldarg.2
        ldarg.1
        switch (Plus, Minus, Multiply, Divide, Other)

        Plus:
        add
        br.s Continue

        Minus:
        sub
        br.s Continue

        Multiply:
        mul
        br.s Continue

        Divide:
        div
        br.s Continue

        Other:
        newobj instance void [System.Runtime]System.InvalidOperationException::.ctor()
        throw

        Continue:
        ret
    }
}
