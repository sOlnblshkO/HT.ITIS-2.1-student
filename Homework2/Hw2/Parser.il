.class public abstract sealed auto ansi beforefieldinit
  Hw2.Parser
{
	.method public static void ParseCalcArguments(string[] args,
    [out] float64& val1,
    [out] valuetype Hw1.CalculatorOperation& operation,
    [out] float64& val2) cil managed
    {
        
    }

    .method private static bool IsArgLengthSupported(string[] args) cil managed
    {
        ldarg.0
        ldlen
        conv.i4
        ldc.i4.3
        ceq
        ret
    }

}


