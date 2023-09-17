.class public abstract sealed auto ansi beforefieldinit
  Hw2.Parser
{
    .method public hidebysig static 
		void ParseCalcArguments (
			string[] args,
			[out] float64& val1,
			[out] valuetype Hw2.CalculatorOperation& operation,
			[out] float64& val2
		) cil managed 
	{
		.maxstack 4
		.locals init (
			[0] bool
		)

		IL_0000: nop
		IL_0001: ldarg.0
		IL_0002: ldc.i4.0
		IL_0003: ldelem.ref
		IL_0004: ldarg.0
		IL_0005: ldc.i4.2
		IL_0006: ldelem.ref
		IL_0007: ldarg.1
		IL_0008: ldarg.3
		IL_0009: call bool Hw2.Parser::IsArgTypeSupported(string, string, float64&, float64&)
		IL_000e: brfalse.s IL_001b

		IL_0010: ldarg.0
		IL_0011: call bool Hw2.Parser::IsArgLengthSupported(string[])
		IL_0016: ldc.i4.0
		IL_0017: ceq
		IL_0019: br.s IL_001c

		IL_001b: ldc.i4.1

		IL_001c: stloc.0
		IL_001d: ldloc.0
		IL_001e: brfalse.s IL_002c

		IL_0020: nop
		IL_0021: ldstr "Inappropriate array format"
		IL_0026: newobj instance void [System.Runtime]System.ArgumentException::.ctor(string)
		IL_002b: throw

		IL_002c: ldarg.2
		IL_002d: ldarg.0
		IL_002e: ldc.i4.1
		IL_002f: ldelem.ref
		IL_0030: call valuetype Hw2.CalculatorOperation Hw2.Parser::ParseOperation(string)
		IL_0035: stind.i4
		IL_0036: ret
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

		IL_0000: nop
		IL_0001: ldarg.3
		IL_0002: ldc.r8 0.0
		IL_000b: stind.r8
		IL_000c: ldarg.0
		IL_000d: ldarg.2
		IL_000e: call bool [System.Runtime]System.Double::TryParse(string, float64&)
		IL_0013: brfalse.s IL_001e

		IL_0015: ldarg.1
		IL_0016: ldarg.3
		IL_0017: call bool [System.Runtime]System.Double::TryParse(string, float64&)
		IL_001c: br.s IL_001f

		IL_001e: ldc.i4.0

		IL_001f: stloc.0
		IL_0020: br.s IL_0022

		IL_0022: ldloc.0
		IL_0023: ret
	}
	
	.method private hidebysig static 
		bool IsArgLengthSupported (
			string[] args
		) cil managed 
	{
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldlen
		IL_0002: conv.i4
		IL_0003: ldc.i4.3
		IL_0004: ceq
		IL_0006: ret
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

		IL_0000: nop
		IL_0001: ldc.i4.1
		IL_0002: brtrue.s IL_0005

		IL_0004: nop

		IL_0005: ldarg.0
		IL_0006: ldstr "+"
		IL_000b: call bool [System.Runtime]System.String::op_Equality(string, string)
		IL_0010: brtrue.s IL_003b

		IL_0012: ldarg.0
		IL_0013: ldstr "-"
		IL_0018: call bool [System.Runtime]System.String::op_Equality(string, string)
		IL_001d: brtrue.s IL_003f

		IL_001f: ldarg.0
		IL_0020: ldstr "*"
		IL_0025: call bool [System.Runtime]System.String::op_Equality(string, string)
		IL_002a: brtrue.s IL_0043

		IL_002c: ldarg.0
		IL_002d: ldstr "/"
		IL_0032: call bool [System.Runtime]System.String::op_Equality(string, string)
		IL_0037: brtrue.s IL_0047

		IL_0039: br.s IL_004b

		IL_003b: ldc.i4.0
		IL_003c: stloc.0
		IL_003d: br.s IL_005c

		IL_003f: ldc.i4.1
		IL_0040: stloc.0
		IL_0041: br.s IL_005c

		IL_0043: ldc.i4.2
		IL_0044: stloc.0
		IL_0045: br.s IL_005c

		IL_0047: ldc.i4.3
		IL_0048: stloc.0
		IL_0049: br.s IL_005c

		IL_004b: ldstr "Unavailable operation: "
		IL_0050: ldarg.0
		IL_0051: call string [System.Runtime]System.String::Concat(string, string)
		IL_0056: newobj instance void [System.Runtime]System.InvalidOperationException::.ctor(string)
		IL_005b: throw

		IL_005c: ldc.i4.1
		IL_005d: brtrue.s IL_0060

		IL_005f: nop

		IL_0060: ldloc.0
		IL_0061: stloc.1
		IL_0062: br.s IL_0064

		IL_0064: ldloc.1
		IL_0065: ret
	}

}


