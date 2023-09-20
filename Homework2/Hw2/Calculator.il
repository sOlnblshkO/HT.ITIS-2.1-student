.class public abstract sealed auto ansi beforefieldinit
  Hw2.Calculator
{
.method public static float64 Calculate(float64, valuetype Hw2.CalculatorOperation, float64) cil managed
    {
    .maxstack 3

    ldarg.1
    switch (Plus, Minus, Multiply, Divide, Other)

    Plus:
    ldarg.0
    ldarg.2
    add
    br.s Continue

    Minus:
    ldarg.0
    ldarg.2
    sub
    br.s Continue

    Multiply:
    ldarg.0
    ldarg.2
    mul
    br.s Continue

    Divide:
    ldarg.0
    ldarg.2
    div
    br.s Continue

    Other:
    newobj instance void [System.Runtime]System.ArgumentOutOfRangeException::.ctor()
    throw

    Continue:
    ret
    }
}



