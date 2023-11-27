.class public abstract sealed auto ansi beforefieldinit
  Hw2.Parser
{
    .method public hidebysig static void
    ParseCalcArguments(
      string[] args,
      [out] float64&,
      [out] valuetype Hw2.CalculatorOperation&,
      [out] float64&
    ) cil managed
    {
        // Выделяем место на стеке
        .maxstack 4
        
        // Подгружаем args на стек
        ldarg.0
        // Проверяем длину
        call bool Hw2.Parser::IsArgLengthSupported(string[])
        // Сравниваем получившееся значение с false, и в  случае, когда метод IsArgLengthSupported возвращает false
        // Перекидываем в точку ArgsLengthIsNotSupported
        brfalse.s ArgsLengthIsNotSupported
    
        // Подгружаем args на стек
        ldarg.0
        // Подгружаем константу 0 на стек(индекс args)
        ldc.i4.0
        // Подгружаем ссылку на args[0]
        ldelem.ref 
        ldarg.1
        call bool [System.Runtime]System.Double::TryParse(string, float64&)
        brfalse.s IsNotDoubleFirstCheck
    
        ldarg.2
        ldarg.0
        ldc.i4.1
        ldelem.ref
        call valuetype Hw2.CalculatorOperation Hw2.Parser::ParseOperation(string)
        stind.i4
        
        ldarg.0
        ldc.i4.2
        ldelem.ref
        ldarg.3
        call bool [System.Runtime]System.Double::TryParse(string, float64&)
        brfalse.s IsNotDoubleSecondCheck
        
        br.s Result
    
        // Выкидываем исключения
        IsNotDoubleSecondCheck:
        ldstr "val2 is not number"
        newobj instance void [System.Runtime]System.ArgumentException::.ctor(string)
        throw
        
        IsNotDoubleFirstCheck:
        ldstr "val1 is not number"
        newobj instance void [System.Runtime]System.ArgumentException::.ctor(string)
        throw
        
        ArgsLengthIsNotSupported:
        ldstr "Need 3 arguments"
        newobj instance void [System.Runtime]System.ArgumentException::.ctor(string)
        throw
        
        Result: 
        ret
    }
    
      .method private hidebysig static bool
    IsArgLengthSupported(
      string[] args 
    ) cil managed
    {
        // Помещаем переданный аргумент в стек
        ldarg.0
        
        // Узнаём длину массива
        ldlen
        
        // Переводим длину массива в int32
        conv.i4
        
        // Помещаем тройку в стек
        ldc.i4.3
        
        // Сравниваем 3 и длину массива
        ceq
        ret
    }
    
    .method private hidebysig static valuetype Hw2.CalculatorOperation
    ParseOperation(
      string arg
    ) cil managed
    {
         .maxstack 2
        
        // Загружаем в стек передаваемый аргумент
        ldarg.0
        // Загружаем в стек строку "+"
        ldstr "+"
        call bool [System.Runtime]System.String::op_Equality(string, string)
        // Если значение метода - true, то переходим к инструкции Plus
        brtrue.s Plus
        
        // Аналогично 
        ldarg.0
        ldstr "-"
        call bool [System.Runtime]System.String::op_Equality(string, string)
        brtrue.s Minus
    
        ldarg.0
        ldstr "/"
        call bool [System.Runtime]System.String::op_Equality(string, string)
        brtrue.s Divide
    
        ldarg.0
        ldstr "*"
        call bool [System.Runtime]System.String::op_Equality(string, string)
        brtrue.s Multiply
  
        br.s Undefined 
    
        Plus:
        ldc.i4.0
        br.s Result
    
        Minus:
        ldc.i4.1
        br.s Result
        
        Divide:
        ldc.i4.3
        br.s Result
    
        Multiply: 
        ldc.i4.2
        br.s Result
        
        // В случае Undefined выбрасываем исключение
        Undefined:
        ldstr "Expected operation(+, -, *, /)"
        newobj instance void [System.Runtime]System.InvalidOperationException::.ctor(string)
        throw
        
        Result:
        ret 
    }
}


