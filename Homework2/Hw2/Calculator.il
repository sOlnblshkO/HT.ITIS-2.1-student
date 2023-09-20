.class public abstract sealed auto ansi beforefieldinit
  Hw2.Calculator extends System.Object
{
    .method static public float64 Calculate(float64 value1, valuetype Hw2.CalculatorOperation operation, float64 value2)
        cil managed
    {
        .maxstack 3
    
        ldarg.0
        ldarg.2
        ldarg.1
        switch (plus, minus, multiply, divide, exception)
    
         plus: 
         add
         br.s result
    
         minus:
         sub 
         br.s result
    
         multiply: 
         mul
         br.s result
    
         divide:
         div
         br.s result
    
        exception:
        newobj instance void [System.Runtime]System.InvalidOperationException::.ctor()
        throw
    
        result:
        ret
    }
}



