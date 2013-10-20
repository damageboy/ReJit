using DiStorm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using OperandType = DiStorm.OperandType;

namespace ReJit
{
  [AttributeUsage(AttributeTargets.Method)]
  internal class ReJitAttribute : Attribute
  {
    internal ReJitAttribute(string replacement)
    {
      Replacement = replacement;
    }

    internal string Replacement { get; private set; }
  }

  public static class Intrinsincs
  {
    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("bswap32")]
    public static int BSWAP32S(int bige)
    {
      BSWAP32S(bige);
      BSWAP32S(bige);
      BSWAP32S(bige);
      BSWAP32S(bige);
      BSWAP32S(bige);
      BSWAP32S(bige);
      return BSWAP32S(bige);
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("bswap32")]
    public static uint BSWAP32U(uint bige)
    {
      BSWAP32U(bige);
      BSWAP32U(bige);
      BSWAP32U(bige);
      BSWAP32U(bige);
      BSWAP32U(bige);
      BSWAP32U(bige);
      return BSWAP32U(bige);
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("bswap64")]
    public static long BSWAP64S(long bige, byte dummy = 0x66)
    {
      BSWAP64S(bige);
      BSWAP64S(bige);
      BSWAP64S(bige);
      BSWAP64S(bige);
      BSWAP64S(bige);
      BSWAP64S(bige);
      return BSWAP64S(bige);
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("bswap64")]
    public static ulong BSWAP64U(ulong bige, byte dummy = 0x66)
    {
      BSWAP64U(bige);
      BSWAP64U(bige);
      BSWAP64U(bige);
      BSWAP64U(bige);
      BSWAP64U(bige);
      BSWAP64U(bige);
      return BSWAP64U(bige);
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("bsf16")]
    public static int BSF(short test)
    {
      BSF(test);
      BSF(test);
      BSF(test);
      BSF(test);
      BSF(test);
      BSF(test);
      return BSF(test);
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("bsf16")]
    public static int BSF(ushort test)
    {
      BSF(test);
      BSF(test);
      BSF(test);
      BSF(test);
      BSF(test);
      BSF(test);
      return BSF(test);
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("bsr16")]
    public static int BSR(short test)
    {
      BSR(test);
      BSR(test);
      BSR(test);
      BSR(test);
      BSR(test);
      BSR(test);
      return BSR(test);
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("bsr16")]
    public static int BSR(ushort test)
    {
      BSR(test);
      BSR(test);
      BSR(test);
      BSR(test);
      BSR(test);
      BSR(test);
      return BSR(test);
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("bsf32")]
    public static int BSF(int test)
    {
      BSF(test);
      BSF(test);
      BSF(test);
      BSF(test);
      BSF(test);
      BSF(test);
      return BSF(test);
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("bsf32")]
    public static int BSF(uint test)
    {
      BSF(test);
      BSF(test);
      BSF(test);
      BSF(test);
      BSF(test);
      BSF(test);
      return BSF(test);
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("bsr32")]
    public static int BSR(int test)
    {
      BSR(test);
      BSR(test);
      BSR(test);
      BSR(test);
      BSR(test);
      BSR(test);
      return BSR(test);
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("bsr32")]
    public static int BSR(uint test)
    {
      BSR(test);
      BSR(test);
      BSR(test);
      BSR(test);
      BSR(test);
      BSR(test);
      return BSR(test);
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("bsf64")]
    public static int BSF(long test)
    {
      BSF(test);
      BSF(test);
      BSF(test);
      BSF(test);
      BSF(test);
      BSF(test);
      return BSF(test);
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("bsf64")]
    public static uint BSF(ulong test)
    {
      BSF(test);
      BSF(test);
      BSF(test);
      BSF(test);
      BSF(test);
      BSF(test);
      return BSF(test);
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("bsr64")]
    public static int BSR(long test)
    {
      BSR(test);
      BSR(test);
      BSR(test);
      BSR(test);
      BSR(test);
      BSR(test);
      return BSR(test);
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("bsr64")]
    public static uint BSR(ulong test)
    {
      BSR(test);
      BSR(test);
      BSR(test);
      BSR(test);
      BSR(test);
      BSR(test);
      return BSR(test);
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("popcnt16")]
    public static int POPCNT(short test)
    {
      POPCNT(test);
      POPCNT(test);
      POPCNT(test);
      POPCNT(test);
      POPCNT(test);
      POPCNT(test);
      return POPCNT(test);
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("popcnt16")]
    public static int POPCNT(ushort test)
    {
      POPCNT(test);
      POPCNT(test);
      POPCNT(test);
      POPCNT(test);
      POPCNT(test);
      POPCNT(test);
      return POPCNT(test);
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("popcnt32")]
    public static int POPCNT(int test)
    {
      POPCNT(test);
      POPCNT(test);
      POPCNT(test);
      POPCNT(test);
      POPCNT(test);
      POPCNT(test);
      return POPCNT(test);
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("popcnt32")]
    public static int POPCNT(uint test)
    {
      POPCNT(test);
      POPCNT(test);
      POPCNT(test);
      POPCNT(test);
      POPCNT(test);
      POPCNT(test);
      return POPCNT(test);
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("popcnt64")]
    public static int POPCNT(long test)
    {
      POPCNT(test);
      POPCNT(test);
      POPCNT(test);
      POPCNT(test);
      POPCNT(test);
      POPCNT(test);
      return POPCNT(test);
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("popcnt64")]
    public static int POPCNT(ulong test)
    {
      POPCNT(test);
      POPCNT(test);
      POPCNT(test);
      POPCNT(test);
      POPCNT(test);
      POPCNT(test);
      return POPCNT(test);
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("rdtscp")]
    public static ulong RDTSCP(int dummy = 666)
    {
      RDTSCP(dummy);
      RDTSCP(dummy);
      RDTSCP(dummy);
      RDTSCP(dummy);
      RDTSCP(dummy);
      RDTSCP(dummy);
      return RDTSCP(dummy);
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("cpuid")]
    public static void CPUID(ref uint eax, out uint ebx, out uint ecx, out uint edx, uint dummy1 = 66, ushort dummy2 = 77, byte dummy3 = 88)
    {
      CPUID(ref eax, out ebx, out ecx, out edx);
      CPUID(ref eax, out ebx, out ecx, out edx);
      CPUID(ref eax, out ebx, out ecx, out edx);
      CPUID(ref eax, out ebx, out ecx, out edx);
      CPUID(ref eax, out ebx, out ecx, out edx);
      CPUID(ref eax, out ebx, out ecx, out edx);
      CPUID(ref eax, out ebx, out ecx, out edx);
      CPUID(ref eax, out ebx, out ecx, out edx);
      CPUID(ref eax, out ebx, out ecx, out edx);
      CPUID(ref eax, out ebx, out ecx, out edx);
      CPUID(ref eax, out ebx, out ecx, out edx);
      CPUID(ref eax, out ebx, out ecx, out edx);
      CPUID(ref eax, out ebx, out ecx, out edx);
      CPUID(ref eax, out ebx, out ecx, out edx);
      CPUID(ref eax, out ebx, out ecx, out edx);
      CPUID(ref eax, out ebx, out ecx, out edx);
      CPUID(ref eax, out ebx, out ecx, out edx);
      CPUID(ref eax, out ebx, out ecx, out edx);
      CPUID(ref eax, out ebx, out ecx, out edx);
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("cpuid")]
    public static void CPUIDNG(ref uint eax, out uint ebx, out uint ecx, out uint edx, uint dummy1 = 66, ushort dummy2 = 77, byte dummy3 = 88)
    {
      // Make the compiler happy
      eax = ebx = ecx = edx = 0;
      DoReJit();
    }

    private static void DoReJit()
    {
      Register[] registerParams;
      BufferParam[] slackParams;
      GetParameterCounts(out registerParams, out slackParams);

      var sf = new StackFrame(2);
      var m = sf.GetMethod();
      var mh = m.MethodHandle;
      var p = mh.GetFunctionPointer();
      //var p = GetMethodAddress(m);
      var offset = sf.GetNativeOffset();
    replay:
      var copy = new byte[offset];
      Marshal.Copy(p, copy, 0, offset);
      var ci = new CodeInfo((long)p, copy, DecodeType.Decode64Bits, 0);
      var dc = new DecomposedResult(100);
      DiStorm3.Decompose(ci, dc);

      // Attempt to detect and handle edit-and-continue crap while debugging
      var ins = dc.Instructions[0];
      if (ins.Opcode == Opcode.JMP && ins.Operands[0].Type == OperandType.Pc)
      {
        p = new IntPtr(p.ToInt64() + ins.Size + (long) ins.Imm.Imm);
        goto replay;
      }

      foreach (var rp in registerParams)
        FindParam(dc, rp);

      foreach (var sp in slackParams)
        FindSlackParam(dc, sp);

      Console.WriteLine(offset);
    }

    public static IntPtr GetMethodAddress(MethodBase method)
    {
      if ((method is DynamicMethod))
      {
        unsafe
        {
          byte* ptr = (byte*) GetDynamicMethodRuntimeHandle(method).ToPointer();
          if (IntPtr.Size == 8)
          {
            ulong* address = (ulong*) ptr;
            address += 6;
            return new IntPtr(address);
          }
          else
          {
            uint* address = (uint*) ptr;
            address += 6;
            return new IntPtr(address);
          }
        }
      }

      RuntimeHelpers.PrepareMethod(method.MethodHandle);

      unsafe
      {
        // Some dwords in the met
        int skip = 10;

        // Read the method index.
        UInt64* location = (UInt64*) (method.MethodHandle.Value.ToPointer());
        int index = (int) (((*location) >> 32) & 0xFF);

        if (IntPtr.Size == 8)
        {
          // Get the method table
          ulong* classStart = (ulong*) method.DeclaringType.TypeHandle.Value.ToPointer();
          ulong* address = classStart + index + skip;
          return new IntPtr(address);
        }
        else
        {
          // Get the method table
          uint* classStart = (uint*) method.DeclaringType.TypeHandle.Value.ToPointer();
          uint* address = classStart + index + skip;
          return new IntPtr(address);
        }
      }
    }

    private static IntPtr GetDynamicMethodRuntimeHandle(MethodBase method)
    {
        if (method is DynamicMethod)
        {
            FieldInfo fieldInfo = typeof(DynamicMethod).GetField("m_method",
                                  BindingFlags.NonPublic | BindingFlags.Instance);
            return ((RuntimeMethodHandle)fieldInfo.GetValue(method)).Value;
        }
        return method.MethodHandle.Value;
    }

    private static void FindParam(DecomposedResult dc, Register reg)
    {
      foreach (var x in dc.Instructions.Reverse())
      {
        if (x.Opcode != Opcode.LEA && x.Opcode != Opcode.MOV)
          continue;
        var op = x.Operands[0];
        if (op.Type != OperandType.Reg || op.Register != reg)
          continue;
        var s = String.Format("Found assignment to {0}: {1:X} {2} {3}", reg, x.Address.ToInt64(), x.Opcode, op.Register);
        Console.WriteLine(s);
        return;
      }
    }

    private static void FindSlackParam(DecomposedResult dc, BufferParam reg)
    {
      foreach (var x in dc.Instructions.Reverse())
      {
        if (x.Opcode != Opcode.LEA && x.Opcode != Opcode.MOV)
          continue;
        var op = x.Operands[1];
        if (op.Type != OperandType.Imm || !CompareImmToObject(reg.RawDefaultValue, x.Imm))
          continue;
        var s = String.Format("Found assignment to {0}: {1:X} {2} {3}", reg.RawDefaultValue, x.Address.ToInt64(), x.Opcode, x.Imm.Imm);
        Console.WriteLine(s);
        return;
      }
    }

    private static bool CompareImmToObject(object rawDefaultValue, DecomposedInst.ImmVariant imm)
    {
      switch (Type.GetTypeCode(rawDefaultValue.GetType()))
      {
        case TypeCode.SByte:
          if (imm.Size != 1)
            return false;
          return (sbyte) imm.Imm == (sbyte) rawDefaultValue;
        case TypeCode.Byte:
          if (imm.Size != 1)
            return false;
          return (byte)imm.Imm == (byte)rawDefaultValue;
        case TypeCode.Int16:
          if (imm.Size != 2)
            return false;
          return (short)imm.Imm == (short)rawDefaultValue;
        case TypeCode.UInt16:
          if (imm.Size != 2)
            return false;
          return (ushort)imm.Imm == (ushort)rawDefaultValue;
        case TypeCode.Int32:
          if (imm.Size != 4)
            return false;
          return (int)imm.Imm == (int)rawDefaultValue;
        case TypeCode.UInt32:
          if (imm.Size != 4)
            return false;
          return (uint)imm.Imm == (uint)rawDefaultValue;
        case TypeCode.Int64:
          if (imm.Size != 8)
            return false;
          return (long)imm.Imm == (long)rawDefaultValue;
        case TypeCode.UInt64:
          if (imm.Size != 8)
            return false;
          return (ulong)imm.Imm == (ulong)rawDefaultValue;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private static void GetParameterCounts(out Register[] intrinsincParams, out BufferParam[] slackParams)
    {
      var sf = new StackFrame(2);

      intrinsincParams =
        sf.GetMethod()
          .GetParameters()
          .TakeWhile((prm, i) => !prm.IsOptional && i < 4)
          .Select((prm, i) => IndexToReg(i, prm.ParameterType)).ToArray();

      slackParams =
        sf.GetMethod()
        .GetParameters()
        .Where(prm => prm.IsOptional)
        .Select(prm => new BufferParam { Type = prm.ParameterType, RawDefaultValue = prm.RawDefaultValue }).ToArray();

      Console.WriteLine("real-params {0}", String.Join(", ", intrinsincParams.Select(x => x)));
      Console.WriteLine("buffer params looking for {0}", String.Join(", ", slackParams.Select(x => x.RawDefaultValue)));
    }

    private static Register IndexToReg(int i, Type code)
    {
      switch (i)
      {
        case 0:
          switch (ParamTypeToRegSize(code)) {
            case 8:
              return Register.R_CL;
            case 16:
              return Register.R_CX;
            case 32:
              return Register.R_ECX;
            case 64:
              return Register.R_RCX;
          }
          break;
        case 1:
          switch (ParamTypeToRegSize(code)) {
            case 8:
              return Register.R_DL;
            case 16:
              return Register.R_DX;
            case 32:
              return Register.R_EDX;
            case 64:
              return Register.R_RDX;
          }
          break;
        case 2:
          switch (ParamTypeToRegSize(code)) {
            case 8:
              return Register.R_R8B;
            case 16:
              return Register.R_R8W;
            case 32:
              return Register.R_R8D;
            case 64:
              return Register.R_R8;
          }
          break;
        case 3:
          switch (ParamTypeToRegSize(code))
          {
            case 8:
              return Register.R_R9B;
            case 16:
              return Register.R_R9W;
            case 32:
              return Register.R_R9D;
            case 64:
              return Register.R_R9;
          }
          break;
      }
      throw new ArgumentOutOfRangeException("i", "no more than 4 parameters can be passed as registers in x64 ABI");
    }

    private static int ParamTypeToRegSize(Type code)
    {
      if (code.IsByRef)
        return 64;

      switch (Type.GetTypeCode(code))
      {
        case TypeCode.Boolean:
          return 32;
        case TypeCode.Char:
          return 16;
        case TypeCode.SByte:
          return 8;
        case TypeCode.Byte:
          return 8;
        case TypeCode.Int16:
          return 16;
        case TypeCode.UInt16:
          return 16;
        case TypeCode.Int32:
          return 32;
        case TypeCode.UInt32:
          return 32;
        case TypeCode.Int64:
          return 64;
        case TypeCode.UInt64:
          return 64;
        case TypeCode.Single:
        case TypeCode.Double:
        case TypeCode.Empty:
        case TypeCode.Object:
        case TypeCode.DBNull:
        case TypeCode.Decimal:
        case TypeCode.DateTime:
        case TypeCode.String:
        default:
          throw new ArgumentOutOfRangeException("code");
      }
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("rdtsc")]
    public static ulong RDTSC(int dummy1 = 666, byte dummy2 = 66)
    {
      RDTSC();
      RDTSC();
      RDTSC();
      RDTSC();
      RDTSC();
      RDTSC();
      return RDTSC();
    }

    private static byte[] GetOpcodes(string i)
    {
      var assembly = Assembly.GetExecutingAssembly();
      var jitStream = assembly.GetManifestResourceStream("ReJit.Intrinsics." + i + ".o");
      var bytes = new byte[jitStream.Length];
      jitStream.Read(bytes, 0, bytes.Length);
      return bytes;
    }

    public static void Init()
    {
      var candidates = typeof(Intrinsincs).GetMethods(BindingFlags.Static | BindingFlags.Public);

      foreach (var m in candidates) {
        var hja = (ReJitAttribute) m.GetCustomAttributes(typeof(ReJitAttribute), false).SingleOrDefault();
        if (hja == null)
          continue;
        Console.WriteLine("Attempting {0}", m.Name);
        var mh = m.MethodHandle;
        RuntimeHelpers.PrepareMethod(mh);
        var p = mh.GetFunctionPointer();
        //var code = GetOpcodes(hja.Replacement);
        //if (IntPtr.Size == 8)
        //  Marshal.Copy(code, 0, p, code.Length);
        //else
        //  throw new NotImplementedException();
      }
    }
  }

  internal class BufferParam
  {
    public Type Type;
    public object RawDefaultValue;
  }
}