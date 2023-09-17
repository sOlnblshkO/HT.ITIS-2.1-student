.class public abstract sealed auto ansi beforefieldinit
  Hw2.Calculator
{
    .method public hidebysig static 
		float64 Calculate (
			float64 value1,
			valuetype Hw2.CalculatorOperation operation,
			float64 value2
		) cil managed 
	{
		.maxstack 2
		.locals init (
			[0] float64,
			[1] float64
		)

		IL_0000: nop
		IL_0001: ldc.i4.1
		IL_0002: brtrue.s IL_0005

		IL_0004: nop

		IL_0005: ldarg.1
		IL_0006: switch (IL_001d, IL_0023, IL_0029, IL_002f)

		IL_001b: br.s IL_0035

		IL_001d: ldarg.0
		IL_001e: ldarg.2
		IL_001f: add
		IL_0020: stloc.0
		IL_0021: br.s IL_003b

		IL_0023: ldarg.0
		IL_0024: ldarg.2
		IL_0025: sub
		IL_0026: stloc.0
		IL_0027: br.s IL_003b

		IL_0029: ldarg.0
		IL_002a: ldarg.2
		IL_002b: mul
		IL_002c: stloc.0
		IL_002d: br.s IL_003b

		IL_002f: ldarg.0
		IL_0030: ldarg.2
		IL_0031: div
		IL_0032: stloc.0
		IL_0033: br.s IL_003b

		IL_0035: newobj instance void [System.Runtime]System.ArgumentOutOfRangeException::.ctor()
		IL_003a: throw

		IL_003b: ldc.i4.1
		IL_003c: brtrue.s IL_003f

		IL_003e: nop

		IL_003f: ldloc.0
		IL_0040: stloc.1
		IL_0041: br.s IL_0043

		IL_0043: ldloc.1
		IL_0044: ret
	}

}



