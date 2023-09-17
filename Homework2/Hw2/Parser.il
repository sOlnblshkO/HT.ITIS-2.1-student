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
    .maxstack 3
    .locals init (
      [0] bool V_0
    )

    
    IL_0000: nop

    IL_0001: ldarg.0      
    IL_0002: call         bool Hw2.Parser::IsArgLengthSupported(string[])
    IL_0007: ldc.i4.0
    IL_0008: ceq
    IL_000a: stloc.0      

    IL_000b: ldloc.0      
    IL_000c: brfalse.s    IL_001a
    
    IL_000e: nop

    IL_000f: ldstr        "There must be 3 arguments!"
    IL_0014: newobj       instance void [System.Runtime]System.ArgumentException::.ctor(string)
    IL_0019: throw

    
    IL_001a: ldarg.1      
    IL_001b: ldarg.0     
    IL_001c: ldc.i4.0
    IL_001d: ldelem.ref
    IL_001e: call         float64 Hw2.Parser::ParseArgument(string)
    IL_0023: stind.r8

   
    IL_0024: ldarg.3      
    IL_0025: ldarg.0      
    IL_0026: ldc.i4.2
    IL_0027: ldelem.ref
    IL_0028: call         float64 Hw2.Parser::ParseArgument(string)
    IL_002d: stind.r8

  
    IL_002e: ldarg.2      
    IL_002f: ldarg.0      
    IL_0030: ldc.i4.1
    IL_0031: ldelem.ref
    IL_0032: call         valuetype Hw2.CalculatorOperation Hw2.Parser::ParseOperation(string)
    IL_0037: stind.i4

    
    IL_0038: ret

  } 

  .method private hidebysig static bool
    IsArgLengthSupported(
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

  .method private hidebysig static valuetype Hw2.CalculatorOperation
    ParseOperation(
      string arg
    ) cil managed
  {
    .maxstack 2
    .locals init (
      [0] valuetype Hw2.CalculatorOperation V_0,
      [1] valuetype Hw2.CalculatorOperation V_1
    )

 
    IL_0000: nop


    IL_0001: ldc.i4.1
    IL_0002: brtrue.s     IL_0005


    IL_0004: nop

    IL_0005: ldarg.0      
    IL_0006: ldstr        "+"
    IL_000b: call         bool [System.Runtime]System.String::op_Equality(string, string)
    IL_0010: brtrue.s     IL_003b
    IL_0012: ldarg.0      
    IL_0013: ldstr        "-"
    IL_0018: call         bool [System.Runtime]System.String::op_Equality(string, string)
    IL_001d: brtrue.s     IL_003f
    IL_001f: ldarg.0     
    IL_0020: ldstr        "*"
    IL_0025: call         bool [System.Runtime]System.String::op_Equality(string, string)
    IL_002a: brtrue.s     IL_0043
    IL_002c: ldarg.0      
    IL_002d: ldstr        "/"
    IL_0032: call         bool [System.Runtime]System.String::op_Equality(string, string)
    IL_0037: brtrue.s     IL_0047
    IL_0039: br.s         IL_004b


    IL_003b: ldc.i4.0
    IL_003c: stloc.0      
    IL_003d: br.s         IL_0056

    IL_003f: ldc.i4.1
    IL_0040: stloc.0      
    IL_0041: br.s         IL_0056


    IL_0043: ldc.i4.2
    IL_0044: stloc.0      
    IL_0045: br.s         IL_0056

    
    IL_0047: ldc.i4.3
    IL_0048: stloc.0      
    IL_0049: br.s         IL_0056

    IL_004b: ldstr        "Incorrect input operation"
    IL_0050: newobj       instance void [System.Runtime]System.InvalidOperationException::.ctor(string)
    IL_0055: throw

    IL_0056: ldc.i4.1
    IL_0057: brtrue.s     IL_005a

    IL_0059: nop

    IL_005a: ldloc.0      
    IL_005b: stloc.1      
    IL_005c: br.s         IL_005e

    IL_005e: ldloc.1      
    IL_005f: ret

  } 

  .method private hidebysig static float64
    ParseArgument(
      string arg
    ) cil managed
  {
    .maxstack 2
    .locals init (
      [0] float64 number,
      [1] bool V_1,
      [2] float64 V_2
    )

    IL_0000: nop

    IL_0001: ldarg.0     
    IL_0002: ldloca.s     number
    IL_0004: call         bool [System.Runtime]System.Double::TryParse(string, float64&)
    IL_0009: stloc.1     

    IL_000a: ldloc.1     
    IL_000b: brfalse.s    IL_0012

    IL_000d: nop

    IL_000e: ldloc.0      
    IL_000f: stloc.2      
    IL_0010: br.s         IL_001d

    IL_0012: ldstr        "Incorrect input number"
    IL_0017: newobj       instance void [System.Runtime]System.ArgumentException::.ctor(string)
    IL_001c: throw

    IL_001d: ldloc.2      
    IL_001e: ret
  } 
}