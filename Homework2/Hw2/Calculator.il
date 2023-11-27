.class public abstract sealed auto ansi beforefieldinit
  Hw2.Calculator
{
      .method public hidebysig static float64
    Calculate(
      float64 value1,
      valuetype Hw2.CalculatorOperation operation,
      float64 value2
    ) cil managed
    
    {
        // Задаю максимальное количество элементов в стеке
        .maxstack 3
        
        // Подгружаю переданные агрументы в стек
        ldarg.0
        ldarg.2
        
        // Делаю switch по operation
        ldarg.1
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
        newobj instance void [System.Runtime]System.InvalidOperationException::.ctor()
        throw
    }
}



