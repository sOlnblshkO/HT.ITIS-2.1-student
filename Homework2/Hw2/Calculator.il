.class public abstract sealed auto ansi beforefieldinit
  Hw2.Calculator
{
    .method static public float64 Calculate(float64 value1, valuetype Hw2.CalculatorOperation operation, float64 value2)
    {
        ldarg value1
        ldarg value2
        
        ldarg operation
        switch (Plus, Minus, Multiply, Divide, Undefined)
        
        Plus: 
        add
        ret
        
        Minus:
        sub
        ret
        
        Multiply:
        mul
        ret
        
        Divide:
        div
        ret
        
        Undefined:
        ldstr "Undefined operation"
        newobj instance void [System.Runtime]System.ArgumentOutOfRangeException::.ctor(string)
        throw
    }
}



