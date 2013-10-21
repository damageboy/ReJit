using DiStorm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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
    private static readonly Dictionary<string, byte[]> _intrinsincs = new Dictionary<string, byte[]>();

    #region Stubs

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("bswap32")]
    public static int BSWAP32S(int bige) { DoReJit(); return 0; }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("bswap32")]
    public static uint BSWAP32U(uint bige) { DoReJit(); return 0; }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("bswap64")]
    public static long BSWAP64S(long bige, byte dummy = 0x66) { DoReJit(); return 0; }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("bswap64")]
    public static ulong BSWAP64U(ulong bige, byte dummy = 0x66) { DoReJit(); return 0;
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("bsf16")]
    public static int BSF(short test) { DoReJit(); return 0; }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("bsf16")]
    public static int BSF(ushort test) { DoReJit(); return 0; }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("bsr16")]
    public static int BSR(short test) { DoReJit(); return 0; }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("bsr16")]
    public static int BSR(ushort test) { DoReJit(); return 0; }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("bsf32")]
    public static int BSF(int test) { DoReJit(); return 0; }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("bsf32")]
    public static int BSF(uint test) { DoReJit(); return 0; }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("bsr32")]
    public static int BSR(int test) { DoReJit(); return 0; }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("bsr32")]
    public static int BSR(uint test) { DoReJit(); return 0; }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("bsf64")]
    public static int BSF(long test) { DoReJit(); return 0; }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("bsf64")]
    public static uint BSF(ulong test) { DoReJit(); return 0; }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("bsr64")]
    public static int BSR(long test) { DoReJit(); return 0; }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("bsr64")]
    public static uint BSR(ulong test) { DoReJit(); return 0; }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("popcnt16")]
    public static int POPCNT(short test) { DoReJit(); return 0; }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("popcnt16")]
    public static int POPCNT(ushort test) { DoReJit(); return 0; }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("popcnt32")]
    public static int POPCNT(int test) { DoReJit(); return 0; }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("popcnt32")]
    public static int POPCNT(uint test) { DoReJit(); return 0; }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("popcnt64")]
    public static int POPCNT(long test) { DoReJit(); return 0; }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("popcnt64")]
    public static int POPCNT(ulong test) { DoReJit(); return 0; }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("rdtscp")]
    public static ulong RDTSCP(int dummy = 666) { DoReJit(); return 0; }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("_cpuid")]
    public static void CPUID(ref uint eax, out uint ebx, out uint ecx, out uint edx, uint dummy1 = 66, ushort dummy2 = 77, byte dummy3 = 88)
    {
      // Make the compiler happy
      eax = ebx = ecx = edx = 0;
      DoReJit();
    }

    #endregion Stubs

    private static unsafe void DoReJit()
    {
      Register[] registerParams;
      BufferParam[] slackParams;
      byte[] replacement;
      GetRejitProperties(out registerParams, out slackParams, out replacement);

      var sf = new StackFrame(2);
      var m = sf.GetMethod();

      var mh = m.MethodHandle;
      var p = mh.GetFunctionPointer();
      var offset = sf.GetNativeOffset();
    replay:
      var copy = new byte[offset];
      Marshal.Copy(p, copy, 0, offset);
      var ci = new CodeInfo((long)p, copy, DecodeType.Decode64Bits, 0);
      var dc = new DecomposedResult(100);
      DiStorm3.Decompose(ci, dc);

      // Attempt to detect and handle edit-and-continue crap while debugging
      var ins = dc.Instructions[0];
      if (ins.Opcode == Opcode.JMP && ins.Operands[0].Type == OperandType.Pc) {
        p = new IntPtr(p.ToInt64() + ins.Size + (long) ins.Imm.Imm);
        goto replay;
      }

      // OK, now we need to go in reverse over all of the instructions leading up to the CALL
      // and split the opcodes leading up to the CALL into opcodes responsible for setting up the input params (up to 4)
      // and buffer params that are actually forced upon the normal JIT to allocate more space for ReJit

      // The call is 5 bytes, that's a constant
      var availableSpace = dc.Instructions.Last().Size;
      var mem = (byte *) p.ToPointer() + offset;
      // Sometimes the Jit pisses nops on us, so we're happy to use it
      while (*(mem++) == 0x90)
        availableSpace++;
      mem = (byte *) p.ToPointer() + offset - 5;

      if (availableSpace >= replacement.Length) {
        ShoveBytes(mem, replacement);
        return;
      }

      foreach (var rp in registerParams)
        FindParam(dc, rp);

      foreach (var sp in slackParams)
        FindSlackParam(dc, sp);

      Console.WriteLine(offset);
    }

    private static unsafe void ShoveBytes(byte* mem, byte[] replacement)
    {
      foreach (var r in replacement)
        *(mem++) = r;
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
        case TypeCode.Byte:
          return imm.Imm == (byte)rawDefaultValue;
        case TypeCode.UInt16:
          return imm.Imm == (ushort)rawDefaultValue;
        case TypeCode.UInt32:
          return imm.Imm == (uint)rawDefaultValue;
        case TypeCode.UInt64:
          return imm.Imm == (ulong)rawDefaultValue;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

#if DISTORM_IMM_SIZE_WORKING
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
#endif

    private static void GetRejitProperties(out Register[] intrinsincParams, out BufferParam[] slackParams, out byte[] replacement)
    {
      var sf = new StackFrame(2);

      var m = sf.GetMethod();
      intrinsincParams =
        m
         .GetParameters()
         .TakeWhile((prm, i) => !prm.IsOptional && i < 4)
         .Select((prm, i) => IndexToReg(i, prm.ParameterType)).ToArray();

      slackParams =
        m
         .GetParameters()
         .Where(prm => prm.IsOptional)
         .Select(prm => new BufferParam { Type = prm.ParameterType, RawDefaultValue = prm.RawDefaultValue }).ToArray();

      var hja = (ReJitAttribute)m.GetCustomAttributes(typeof(ReJitAttribute), false).SingleOrDefault();

      if (hja == null)
        throw new Exception("Cannot rejit a method that is not marked with a valid rejit attribute");

      if (!_intrinsincs.TryGetValue(hja.Replacement, out replacement))
        throw new Exception(string.Format("Cannot rejit method {0} since replacement {1} doesn't seem to exist", m.Name, hja.Replacement));

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

    public static void Init()
    {
      ReadEmbeddedInstrinsincs();
    }

    private static void ReadEmbeddedInstrinsincs()
    {
      var assembly = Assembly.GetExecutingAssembly();
      var dotO = assembly.GetManifestResourceStream("ReJit.Intrinsics.intrinsincs.o");
      var dotMap = assembly.GetManifestResourceStream("ReJit.Intrinsics.intrinsincs.map");
      var tmp = new byte[dotO.Length];
      dotO.Read(tmp, 0, tmp.Length);

      string prevName = null, name = null;
      int prevOffset = 0, fileOffset = 0;
      foreach (var line in ReadFrom(dotMap).SkipWhile(s => s != "-- Symbols --------------------------------------------------------------------").Skip(5)) {
        if (string.IsNullOrWhiteSpace(line)) {
          name = null;
          fileOffset = tmp.Length;
        }
        else {
          var parts = line.Split(new [] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
          fileOffset = Int32.Parse(parts[0], NumberStyles.HexNumber);
          name = parts[2];
        }
        if (prevName != null) {
          var b = new byte[fileOffset - prevOffset];
          Array.Copy(tmp, prevOffset, b, 0, fileOffset - prevOffset);
          _intrinsincs[prevName] = b;
        }
        prevName = name;
        prevOffset = fileOffset;
      }
    }

    static IEnumerable<string> ReadFrom(Stream s)
    {
      string line;
      using (var reader = new StreamReader(s)) {
        while ((line = reader.ReadLine()) != null)
        yield return line;
      }
    }
  }

  internal class BufferParam
  {
    public Type Type;
    public object RawDefaultValue;
  }
}