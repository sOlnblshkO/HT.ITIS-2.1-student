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
			[0] float64
		)
		ldarg.1
		switch (IL_001, IL_002, IL_003, IL_004)

		br.s IL_0002

		IL_001: ldarg.0
		ldarg.2
		add
		stloc.0
		br.s IL_0003

		IL_002: ldarg.0
		ldarg.2
		sub
		stloc.0
		br.s IL_0003

		IL_003: ldarg.0
		ldarg.2
		mul
		stloc.0
		br.s IL_0003

		IL_004: ldarg.0
		ldarg.2
		div
		stloc.0
		br.s IL_0003

		IL_0002: newobj instance void [System.Runtime]System.ArgumentOutOfRangeException::.ctor()
		throw

		IL_0003: ldloc.0
		ret
	}
}



