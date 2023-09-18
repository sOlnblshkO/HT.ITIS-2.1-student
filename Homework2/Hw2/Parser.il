.class public abstract sealed auto ansi beforefieldinit
  Hw2.Parser extends System.Object
{
    .method public static void ParseCalcArguments (
      string[] args,
      [out] float64& val1,
      [out] valuetype Hw2.CalculatorOperation& operation,
      [out] float64& val2
    )
    {
        .maxstack 3
        ldarg.0
        brfalse.s Exeption

        ldarg.0
        call bool Hw2.Parser::IsArgLengthSupported(string[])
        brfalse.s Exeption

        ldarg.0
        ldc.i4.0
        ldelem.ref
        ldarg.1
        call bool [System.Runtime]System.Double::TryParse(string, float64&)
        brfalse.s Exeption

        ldarg.0
        ldc.i4.2
        ldelem.ref
        ldarg.3
        call bool [System.Runtime]System.Double::TryParse(string, float64&)
        brfalse.s Exeption

        ldarg.2
        ldarg.0
        ldc.i4.1
        ldelem.ref
        call valuetype Hw2.CalculatorOperation Hw2.Parser::ParseOperation(string)
        stind.i4

        ret

        Exeption:
        newobj instance void [System.Runtime]System.ArgumentException::.ctor()
        throw
    }
    
    .method private hidebysig static bool
    IsArgLengthSupported(
      string[] args
    )
    {
        .maxstack 2
        ldarg.0
        ldlen
        ldc.i4.3
        ceq
        ret
    }
    
    .method private hidebysig static valuetype Hw2.CalculatorOperation
    ParseOperation(
      string arg
    )
    {
        .maxstack 2
        .locals init ([0] valuetype Hw2.CalculatorOperation)

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

        ldarg.0
        ldstr "_"
        call bool [System.Runtime]System.String::op_Equality(string, string)
        brtrue.s Undefined

        br.s InvalidOperation

        Plus: 
        ldc.i4.0
        br.s Out


        Minus: 
        ldc.i4.1
        br.s Out


        Multiply: 
        ldc.i4.2
        br.s Out


        Divide: 
        ldc.i4.3
        br.s Out


        Undefined:
        newobj instance void [System.Runtime]System.ArgumentOutOfRangeException::.ctor()
        throw

        Out:
        stloc.0
        ldloc.0
        ret

        InvalidOperation:
        newobj instance void [System.Runtime]System.InvalidOperationException::.ctor()
        throw
    }
}


