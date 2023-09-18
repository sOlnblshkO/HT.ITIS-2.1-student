.class public abstract sealed auto ansi beforefieldinit
  Hw2.Calculator
{
	.method public static float64 Calculate(float64 value1, 
	valuetype Hw2.CalculatorOperation operation,
	float64 value2) cil managed
	{
		ldarg.1
		switch(case_Plus,case_Minus,case_Multiply,case_Divide)

		case_Plus:
			ldarg.0
			ldarg.2
			add
			ret

		case_Minus:
			ldarg.0
			ldarg.2
			sub
			ret

		case_Multiply:
			ldarg.0
			ldarg.2
			mul
			ret

		case_Divide:
			ldarg.0
			ldarg.2
			div
			ret

		default_case:
			ldstr "Invalid operation"
			newobj instance void [mscorlib]System.ArgumentException::.ctor(string)
			throw
	}

}



