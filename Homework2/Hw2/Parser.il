.class public abstract sealed auto ansi beforefieldinit
  Hw2.Parser
{
	.method public static void ParseCalcArguments(string[] args,
    [out] float64& val1,
    [out] valuetype Hw2.CalculatorOperation& operation,
    [out] float64& val2)
    {
        ldarg.0
        call bool Hw2.Parser::IsArgLengthSupported(string[])
        brfalse notSupported

        ldarg.0
        ldc.i4.0
        ldelem.ref
        ldarg.1
        call bool [mscorlib]System.Double::TryParse(string,float64&)
        brfalse firstArgInvalid

        
        ldarg.0
        ldc.i4.2
        ldelem.ref
        ldarg.3
        call bool [mscorlib]System.Double::TryParse(string,float64&)
        brfalse secondArgInvalid

        ldarg.2
        ldarg.0
        ldc.i4.1
        ldelem.ref
        call valuetype Hw2.CalculatorOperation Hw2.Parser::ParseOperation(string)        
        stind.i4
        br returnNode

        notSupported:
        ldstr "Invalid argument count"
        newobj instance void [mscorlib]System.ArgumentException::.ctor(string)
		throw

        firstArgInvalid:
        ldstr "First argument invalid"
        newobj instance void [mscorlib]System.ArgumentException::.ctor(string)
		throw

        secondArgInvalid:
        ldstr "Second argument invalid"
        newobj instance void [mscorlib]System.ArgumentException::.ctor(string)
		throw

        returnNode:
        ret
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

    .method private static valuetype Hw2.CalculatorOperation ParseOperation(string arg) cil managed
    {
        .locals init (
            [0] valuetype Hw2.CalculatorOperation
        )

        ldarg.0
        ldstr "+"
        call bool [mscorlib]System.String::Equals(string, string)
        brtrue labelPlus
        ldarg.0
        ldstr "-"
        call bool [mscorlib]System.String::Equals(string, string)
        brtrue labelMinus
        ldarg.0
        ldstr "*"
        call bool [mscorlib]System.String::Equals(string, string)
        brtrue labelMultiply
        ldarg.0
        ldstr "/"
        call bool [mscorlib]System.String::Equals(string, string)
        brtrue labelDivide

        ldstr "Invalid operation: "
        ldarg.0
        call string [mscorlib]System.String::Concat(string, string)
        newobj instance void [mscorlib]System.InvalidOperationException::.ctor(string)
        throw

        labelPlus:
            ldc.i4.0
            stloc.0
            br retLabel
    
        labelMinus:
            ldc.i4.1
            stloc.0
            br retLabel
    
        labelMultiply:
            ldc.i4.2
            stloc.0
            br retLabel
    
        labelDivide:
            ldc.i4.3
            stloc.0
            br retLabel
    
        retLabel:
            ldloc.0
            ret
    }

}


