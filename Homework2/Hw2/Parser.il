.class public abstract sealed auto ansi beforefieldinit
  Hw2.Parser
{
    .method public hidebysig static void
    ParseCalcArguments(
      string[] args,
      [out] float64& val1,
      [out] valuetype Hw2.CalculatorOperation& operation,
      [out] float64& val2
    ) cil managed
    {
    .maxstack 4
    .locals init (
      [0] bool V_0
    )
        ldarg.0      
        ldc.i4.0
        ldelem.ref
        ldarg.0      
        ldc.i4.2
        ldelem.ref
        ldarg.1      
        ldarg.3      
        call         
        bool Hw2.Parser::IsArgTypeSupported(string, string, float64&, float64&)
        brfalse.s IL_001
        ldarg.0      
        call         
        bool Hw2.Parser::IsArgLengthSupported(string[])
        ldc.i4.0
        ceq
        br.s IL_002
        IL_001: ldc.i4.1
        IL_002: stloc.0      
        
        ldloc.0      
        brfalse.s IL_003
        
        ldstr "Inappropriate array format"
        newobj       
        instance void [System.Runtime]System.ArgumentException::.ctor(string)
        throw
               
        IL_003: ldarg.2     
        ldarg.0      
        ldc.i4.1
        ldelem.ref
        call         
        valuetype Hw2.CalculatorOperation Hw2.Parser::ParseOperation(string)
        stind.i4
           
        ret
    }
	
	.method public hidebysig static 
		bool IsArgTypeSupported (
			string arg1,
			string arg2,
			[out] float64& val1,
			[out] float64& val2
		) cil managed 
	{
		.maxstack 2
		.locals init (
			[0] bool
		)

        ldarg.3
        ldc.r8 0.0
        stind.r8
        ldarg.0
        ldarg.2
        call bool [System.Runtime]System.Double::TryParse(string, float64&)
        brfalse.s IL_001

		ldarg.1
		ldarg.3
		call bool [System.Runtime]System.Double::TryParse(string, float64&)
		br.s IL_002

		IL_001: ldc.i4.0

		IL_002: stloc.0
		br.s IL_003

		IL_003: ldloc.0
		ret
	}
	
	.method private hidebysig static 
		bool IsArgLengthSupported (
			string[] args
		) cil managed 
	{
		.maxstack 8

		ldarg.0
		ldlen
		conv.i4
		ldc.i4.3
		ceq
		ret
	}
	
	.method private hidebysig static 
		valuetype Hw2.CalculatorOperation ParseOperation (
			string arg
		) cil managed 
	{
		.maxstack 2
		.locals init (
			[0] valuetype Hw2.CalculatorOperation,
			[1] valuetype Hw2.CalculatorOperation
		)

		ldc.i4.1
		brtrue.s IL_001

		IL_001: ldarg.0
		ldstr "+"
		call bool [System.Runtime]System.String::op_Equality(string, string)
		brtrue.s IL_0001

		ldarg.0
		ldstr "-"
		call bool [System.Runtime]System.String::op_Equality(string, string)
		brtrue.s IL_0002

		ldarg.0
		ldstr "*"
		call bool [System.Runtime]System.String::op_Equality(string, string)
		brtrue.s IL_0003

		ldarg.0
		ldstr "/"
		call bool [System.Runtime]System.String::op_Equality(string, string)
		brtrue.s IL_0004

		br.s IL_001b

		IL_0001: ldc.i4.0
		stloc.0
		br.s IL_002c

		IL_0002: ldc.i4.1
		stloc.0
		br.s IL_002c

		IL_0003: ldc.i4.2
		stloc.0
		br.s IL_002c

		IL_0004: ldc.i4.3
		stloc.0
		br.s IL_002c

		IL_001b: ldstr "Unavailable operation: "
		ldarg.0
		call string [System.Runtime]System.String::Concat(string, string)
		newobj instance void [System.Runtime]System.InvalidOperationException::.ctor(string)
		throw

		IL_002c: ldc.i4.1
		brtrue.s IL_003c

		IL_003c: ldloc.0
        stloc.1
		br.s IL_004c

		IL_004c: ldloc.1
		ret
	}

}


