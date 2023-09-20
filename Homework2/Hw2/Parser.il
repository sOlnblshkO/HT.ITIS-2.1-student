.class public abstract sealed auto ansi beforefieldinit
  Hw2.Parser
{   
    .method public static void ParseCalcArguments(string[] args,
     [out] float64& val1,
     [out] valuetype Hw2.CalculatorOperation& operation,
     [out] float64& val2)
     {
        .maxstack 3

        ldarg.0
        call bool Hw2.Parser::IsArgLengthSupported(string[] args)
        brfalse.s IncorrectInput

        ldarg.0
        ldc.i4.0
        ldelem string
        ldarg.1
        call bool [mscorlib]System.Double::TryParse(string, [out] float64&)
        brfalse.s IncorrectInput

        ldarg.0
        ldc.i4.2
        ldelem string
        ldarg.3
        call bool [mscorlib]System.Double::TryParse(string, [out] float64&)
        brfalse.s IncorrectInput


        ldarg.2
        ldarg.0
        ldc.i4.1
        ldelem string
        call valuetype Hw2.CalculatorOperation Hw2.Parser::ParseOperation(string)
        stind.i4
        ldarg.2
        ldind.i4
        ldc.i4.4
        beq.s IncorrectOperation

        ret

        IncorrectInput:
        ldstr "Incorrect input!"
        newobj instance void [mscorlib]System.ArgumentException::.ctor()
        throw

        IncorrectOperation:
        ldstr "Incorrect operation!"
        newobj instance void [mscorlib]System.InvalidOperationException::.ctor()
        throw
     }
    .method private static bool IsArgLengthSupported(string[]) cil managed
    {
        .maxstack 2
        ldarg.0
        ldlen
        ldc.i4.3
        ceq
        ret
    }
    .method private static valuetype Hw2.CalculatorOperation ParseOperation(string arg)
    {
        .maxstack 2

        ldarg.0
        ldstr "+"
        call bool [mscorlib]System.String::Equals(string, string)
        brtrue.s Plus

        ldarg.0
        ldstr "-"
        call bool [mscorlib]System.String::Equals(string, string)
        brtrue.s Minus

        ldarg.0
        ldstr "*"
        call bool [mscorlib]System.String::Equals(string, string)
        brtrue.s Multiply

        ldarg.0
        ldstr "/"
        call bool [mscorlib]System.String::Equals(string, string)
        brtrue.s Devide

        ldc.i4.4
        ret

        Plus:
        ldc.i4.0
        ret
        Minus:
        ldc.i4.1
        ret
        Multiply:
        ldc.i4.2
        ret
        Devide:
        ldc.i4.3
        ret
    }
}