.class public abstract sealed auto ansi beforefieldinit
  Hw2.Parser
{
    .method public static void ParseCalcArguments(
        string[] args, 
        [out] float64& val1, 
        [out] valuetype Hw2.CalculatorOperation& op, 
        [out] float64& val2) cil managed
    {
        .maxstack 3

        ldarg args
        call bool Hw2.Parser::IsArgLengthSupported(string[])
        brfalse ThrowArgumentException

        ldarg val1
        ldarg args
        ldc.i4.0
        ldelem.ref
        call float64 Hw2.Parser::ParseValue(string) 
        stind.r8        

        ldarg op
        ldarg args
        ldc.i4.1
        ldelem.ref 
        call valuetype Hw2.CalculatorOperation Hw2.Parser::ParseOperation(string)
        stind.i1

        ldarg val2
        ldarg args
        ldc.i4.2
        ldelem.ref
        call float64 Hw2.Parser::ParseValue(string)
        stind.r8

        ret

        ThrowArgumentException:
            ldstr ""
            newobj instance void [System.RunTime]System.ArgumentException::.ctor(string)
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

    .method private static float64 ParseValue(string) cil managed
    {
        .maxstack 2
        .locals init (
            float64 result
        )
        ldarg.0
        ldloca.s 0
        call bool float64::TryParse(string, float64&)
        brfalse ThrowArgumentException

        ldloc.0
        ret

        ThrowArgumentException:
            ldstr ""
            newobj instance void [System.Runtime]System.ArgumentException::.ctor(string)
            throw

    }

    .method private static valuetype Hw2.CalculatorOperation ParseOperation(string) cil managed
    {
        .maxstack 2
        // plus
        ldarg.0
        ldstr "+"
        call bool string::Equals(string, string)
        brtrue.s Plus

        // minus
        ldarg.0
        ldstr "-"
        call bool string::Equals(string, string)
        brtrue.s Minus

        // multiply
        ldarg.0
        ldstr "*"
        call bool string::Equals(string, string)
        brtrue.s Multiply

        // divide
        ldarg.0
        ldstr "/"
        call bool string::Equals(string, string)
        brtrue.s Divide

        br.s Undefined

        Plus:
            ldc.i4.0
            ret
        Minus:
            ldc.i4.1
            ret
        Multiply:
            ldc.i4.2
            ret
        Divide:
            ldc.i4.3
            ret
        Undefined:
            newobj instance void [System.Runtime]System.InvalidOperationException::.ctor()
            throw
    }
}