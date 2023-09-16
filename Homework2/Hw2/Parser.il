.class public abstract sealed auto ansi beforefieldinit
  Hw2.Parser
{
    .method public static void ParseCalcArguments(string[], [out] float64&, [out] valuetype Hw2.CalculatorOperation&, [out] float64&) cil managed
    {
        .maxstack 4
        
        ldarg.0
        call bool Hw2.Parser::IsArgLengthSupported(string[])
        brfalse.s ArgLengthNotSupported

        ldarg.0
        ldc.i4.0
        ldelem.ref
        ldarg.1
        call bool [System.Runtime]System.Double::TryParse(string, float64&)
        brfalse.s FirstIsNotDouble

        ldarg.2
        ldarg.0
        ldc.i4.1
        ldelem.ref
        call valuetype Hw2.CalculatorOperation Hw2.Parser::ParseOperation(string)
        dup
        ldc.i4.4
        ceq
        brtrue.s SecondIsNotOperation
        stind.i4

        ldarg.0
        ldc.i4.2
        ldelem.ref
        ldarg.3
        call bool [System.Runtime]System.Double::TryParse(string, float64&)
        brfalse.s ThirdIsNotDouble

        br.s Continue

        ArgLengthNotSupported:
        ldstr "Incorrect number of arguments"
        newobj instance void [System.Runtime]System.ArgumentException::.ctor(string)
        throw

        FirstIsNotDouble:
        ldstr "First argument is not a real number"
        newobj instance void [System.Runtime]System.ArgumentException::.ctor(string)
        throw

        SecondIsNotOperation:
        ldstr "Undefined operation"
        newobj instance void [System.Runtime]System.InvalidOperationException::.ctor(string)
        throw

        ThirdIsNotDouble:
        ldstr "Third argument is not a real number"
        newobj instance void [System.Runtime]System.ArgumentException::.ctor(string)
        throw

        Continue:
        ret
    }

    .method private static bool IsArgLengthSupported(string[]) cil managed
    {
        .maxstack 2
        ldarg.0
        ldlen
        ldc.i4 3
        ceq
        ret
    }

    .method private static valuetype Hw2.CalculatorOperation ParseOperation(string) cil managed
    {
        .maxstack 2
        ldarg.0
        ldstr "+"
        call bool [System.Runtime]System.String::op_Equality(string, string)
        brtrue.s Plus
        ldarg.0
        ldstr "-"
        call bool [System.Runtime]System.String::op_Equality(string, string)
        brtrue.s Minus
        ldarg.0
        ldstr "*"
        call bool [System.Runtime]System.String::op_Equality(string, string)
        brtrue.s Multiply
        ldarg.0
        ldstr "/"
        call bool [System.Runtime]System.String::op_Equality(string, string)
        brtrue.s Divide
        br.s Other

        Plus:
        ldc.i4.0
        br.s Continue

        Minus:
        ldc.i4.1
        br.s Continue

        Multiply:
        ldc.i4.2
        br.s Continue

        Divide:
        ldc.i4.3
        br.s Continue

        Other:
        ldc.i4.4
        br.s Continue

        Continue:
        ret
    }
}
