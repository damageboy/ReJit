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
using diStorm;
using EasyHook;
using HookJitCompile;
using NLog;
using OperandType = diStorm.OperandType;

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

  public static class Intrinsincs {
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();

    private static readonly Dictionary<string, byte[]> _intrinsincs = new Dictionary<string, byte[]>();
    private static readonly Dictionary<long, byte[]> _stubMap = new Dictionary<long, byte[]>();

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
    [ReJit("_rdtscp")]
    public static ulong RDTSCP(int dummy = 666) { DoReJit(); return 0; }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("rdtsc_fenced")]
    public static ulong RDTSCFenced(int dummy = 666) { DoReJit(); return 0; }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [ReJit("_rdtsc")]
    public static ulong RDTSC(int dummy = 666) { DoReJit(); return 0; }

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

      byte* paramsMemStart = null;
      byte* paramsMemEnd = null;
      byte* slackMemStart = null;
      byte* slackMemEnd = null;
      int availableSpace;
      var memStart = (byte*) p.ToPointer() + offset;
      var memEnd = memStart;
replay:
      var ci = new CodeInfo((long)p, (byte*) p.ToPointer(), offset, DecodeType.Decode64Bits, 0);
      using (var dc = DiStorm3.Decompose(ci, 100)) {
        // Attempt to detect and handle edit-and-continue crap while debugging
        var ins = dc.Instructions[0];
        if (ins.Opcode == Opcode.JMP && ins.Operands[0].Type == OperandType.Pc) {
          p = new IntPtr(p.ToInt64() + ins.Size + (long) ins.Imm.Imm);
          Log.Debug("Detected edit-and-continue, decompiling the real stuff");
          dc.Dispose();
          goto replay;
        }

        // OK, now we need to go in reverse over all of the instructions leading up to the CALL
        // and split the opcodes leading up to the CALL into opcodes responsible for setting up the input params (up to 4)
        // and buffer params that are actually forced upon the normal JIT to allocate more space for ReJit

        // The call is 5 bytes, that's a constant
        availableSpace = dc.Instructions.Last().Size;
        // Sometimes the Jit pisses nops on us, so we're happy to use it
        while (*(memStart++) == 0x90)
          availableSpace++;
        memStart = (byte*) p.ToPointer() + offset - 5;

        if (availableSpace >= replacement.Length) {
          Log.Debug("CALL had {0} bytes of space while replace is {1} bytes long, patching and finishing up",
            availableSpace, replacement.Length);
          ShoveBytesPaddedWithNOPs(memStart, availableSpace, replacement);
          return;
        }

        var targetsLeft = registerParams.Length + slackParams.Length;
        Log.Debug("Need to look for {0} load instructions", targetsLeft);

        // We go over the existing instructions in reverse one by one, until we find all of our relevant
        // bits, we skip the first one since it's the CALL that brought us here
        foreach (var x in dc.Instructions.Reverse().Skip(1)) {
          foreach (var reg in registerParams) {
            if (x.Opcode != Opcode.LEA && x.Opcode != Opcode.MOV)
              continue;
            var op = x.Operands[0];
            if (op.Type != OperandType.Reg || op.Register != reg)
              continue;
            Log.Debug("Found assignment to {0}: {1:X} {2} {3}", reg, x.Address.ToInt64(), x.Opcode, op.Register);
            if (paramsMemStart == null && paramsMemEnd == null)
              paramsMemStart = paramsMemEnd = memStart;
            memStart -= x.Size;
            paramsMemStart -= x.Size;
            targetsLeft--;
            break;
          }
          foreach (var reg in slackParams) {
            if (x.Opcode != Opcode.LEA && x.Opcode != Opcode.MOV)
              continue;
            var op = x.Operands[1];
            if (op.Type != OperandType.Imm || !CompareImmToObject(reg.RawDefaultValue, x.Imm))
              continue;
            Log.Debug("Found assignment to {0}: {1:X} {2} {3}", reg.RawDefaultValue, x.Address.ToInt64(), x.Opcode,
              x.Imm.Imm);
            if (slackMemEnd == null && slackMemStart == null)
              slackMemEnd = slackMemStart = memStart;
            memStart -= x.Size;
            slackMemStart -= x.Size;
            targetsLeft--;
            break;
          }
          if (targetsLeft == 0)
            break;
        }
        if (slackMemEnd != paramsMemStart)
          throw new Exception("slack and params are overlapping");
      }

      var paramsLen = (int) ((ulong)paramsMemEnd - (ulong)paramsMemStart);
      var slackLen = (int) ((ulong)slackMemEnd - (ulong)slackMemStart);
      availableSpace += slackLen;
      Log.Debug("Code area is {0} bytes (0x{1:X}-0x{2:X}", (ulong)memEnd - (ulong)memStart, (ulong)memStart, (ulong)memEnd);
      Log.Debug("Params is {0} bytes (0x{1:X}-0x{2:X}", paramsLen, (ulong)paramsMemStart, (ulong)paramsMemEnd);
      Log.Debug("Slack is {0} bytes (0x{1:X}-0x{2:X}", slackLen, (ulong)slackMemStart, (ulong)slackMemEnd);
      Log.Debug("Available space is {0} bytes, replacement is {1} bytes", availableSpace, replacement.Length);
      if (availableSpace < replacement.Length)
        throw new Exception("Cannot re-jit since there's not enough space for the replacement opcodes");
      ShoveBytes(slackMemStart, paramsLen, paramsMemStart);
      ShoveBytesPaddedWithNOPs(slackMemStart + paramsLen, availableSpace, replacement);
      Log.Debug("Done patching call-site for {0}@0x{1:X}", m.Name, (ulong) slackMemStart);
    }

    private static unsafe void ShoveBytesPaddedWithNOPs(byte* dst, int length, byte[] replacement)
    {
      if (replacement.Length > length)
        throw new Exception("Not enough room for replacement");
      foreach (var r in replacement)
        *(dst++) = r;

      if (replacement.Length == length)
        return;

      var i = length - replacement.Length;
      while(i-- > 0)
        *(dst++) = 0x90;
    }

    private static unsafe void ShoveBytes(byte* dst, int length, byte *src)
    {
      for (var i = 0; i < length; i++)
        *(dst++) = src[i];
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

    private static bool CompareImmToObject(object rawDefaultValue, DecomposedInstruction.ImmVariant imm)
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

      Log.Debug("real-params {0}", String.Join(", ", intrinsincParams.Select(x => x)));
      Log.Debug("buffer params looking for {0}", String.Join(", ", slackParams.Select(x => x.RawDefaultValue)));
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
        default:
          throw new ArgumentOutOfRangeException("code");
      }
    }

    public static void Init()
    {
      PreJitOurselves();
      ReadEmbeddedInstrinsincs();
      ScanForIntrinsincStubs();
      InstallJitHook();
    }

    private static void PreJitOurselves()
    {
      var candidates = typeof(Intrinsincs).GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
      foreach (var m in candidates) {
        // We don't want to do the Insrinsincs stubs, non of our business here
        var hja = (ReJitAttribute) m.GetCustomAttributes(typeof (ReJitAttribute), false).SingleOrDefault();
        if (hja != null)
          continue;
        RuntimeHelpers.PrepareMethod(m.MethodHandle);
      }
    }

    private static void InstallJitHook()
    {
      var hook = GetManagedJitCompiler();
      hook.HookClrJitCompiler();
    }

    private static void ScanForIntrinsincStubs()
    {
      var candidates = typeof(Intrinsincs).GetMethods(BindingFlags.Static | BindingFlags.Public);

      foreach (var m in candidates) {
        var hja = (ReJitAttribute) m.GetCustomAttributes(typeof (ReJitAttribute), false).SingleOrDefault();
        if (hja == null)
          continue;
        var mh = m.MethodHandle;
        RuntimeHelpers.PrepareMethod(mh);
        var p = mh.GetFunctionPointer();
        if (!_intrinsincs.ContainsKey(hja.Replacement))
          throw new Exception(string.Format("There's no intrinsinc registered for {0}", hja.Replacement));
        _stubMap[p.ToInt64()] = _intrinsincs[hja.Replacement];
      }
      Log.Debug("Found {0} matching intrinsinc stubs", _stubMap.Count);
    }

    private static void ReadEmbeddedInstrinsincs()
    {
      var assembly = Assembly.GetExecutingAssembly();
      var dotO = assembly.GetManifestResourceStream("ReJit.Intrinsics.intrinsincs.o");
      if (dotO == null || dotO.Length == 0)
        throw new BadImageFormatException("Can't find embedded intrinsincs inside assembly");
      var dotMap = assembly.GetManifestResourceStream("ReJit.Intrinsics.intrinsincs.map");
      if (dotMap == null || dotMap.Length == 0)
        throw new BadImageFormatException("Can't find embedded map inside assembly");

      var tmp = new byte[dotO.Length];
      dotO.Read(tmp, 0, tmp.Length);

      string prevName = null;
      var prevOffset = 0;
      foreach (var line in ReadFrom(dotMap).SkipWhile(s => s != "-- Symbols --------------------------------------------------------------------").Skip(5)) {
        var fileOffset = 0;
        string name;
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
      Log.Debug("Found {0} embedded intrinsincs", _intrinsincs.Count);
    }

    static IEnumerable<string> ReadFrom(Stream s)
    {
      string line;
      using (var reader = new StreamReader(s)) {
        while ((line = reader.ReadLine()) != null)
        yield return line;
      }
    }

    [DllImport("clrjit.dll", SetLastError = true, PreserveSig = true, CallingConvention = CallingConvention.StdCall)]
    private static extern IntPtr getJit();

    public static ClrJitCompilerHook GetManagedJitCompiler()
    {
      ClrJitCompilerHook clrJitCompiler = null;
      var jit = getJit();
      var jitCompileMethod = GetVTableAddresses(jit, 1).First();
      clrJitCompiler = new ClrJitCompilerHook(jit, jitCompileMethod);
      return clrJitCompiler;
    }

    private static IntPtr[] GetVTableAddresses(IntPtr pointer, int numberOfMethods)
    {
      var vtblAddresses = new List<IntPtr>();
      var vTable = Marshal.ReadIntPtr(pointer);
      for (var i = 0; i < numberOfMethods; i++)
        vtblAddresses.Add(Marshal.ReadIntPtr(vTable, i * IntPtr.Size));
      return vtblAddresses.ToArray();
    }
  }

  [UnmanagedFunctionPointer(CallingConvention.StdCall, SetLastError = true)]
  public unsafe delegate CorJitResult CompileMethodDelegate(IntPtr thisJit, [In] IntPtr corJitInfo, [In] CorMethodInfo *corMethodInfo, CorJitFlag flags, [Out] byte **ilCode, [Out] IntPtr *ilCodeSize);

  public class ClrJitCompilerHook
  {
    private IntPtr _clrJitPointer;
    private readonly IntPtr _compileMethod;
    private LocalHook _localJitHook;
    private readonly CompileMethodDelegate _hookMethod;
    private CompileMethodDelegate _jitCompileMethod;
    private static Logger Log = LogManager.GetCurrentClassLogger();

    internal unsafe ClrJitCompilerHook(IntPtr clrJit, IntPtr compileMethod)
    {
      _clrJitPointer = clrJit;
      _compileMethod = compileMethod;
      _hookMethod = ClrJitCompilerCalled;
      _jitCompileMethod = (CompileMethodDelegate)Marshal.GetDelegateForFunctionPointer(_compileMethod, typeof(CompileMethodDelegate));
    }

    public ClrJitCompilerHook() { }

    internal unsafe CorJitResult ClrJitCompilerCalled(IntPtr thisJit, IntPtr corJitInfo, CorMethodInfo *corMethodInfo, CorJitFlag flags, byte **ilCode, IntPtr *ilCodeSize)
    {
      var result = _jitCompileMethod(thisJit, corJitInfo, corMethodInfo, flags, ilCode, ilCodeSize);
      Log.Debug("OnJitCompileCalled! 0x{0:X} -> 0x{1:X}", (ulong)corMethodInfo->methodHandle, (ulong) *ilCode);
      return result;
    }

    public unsafe void HookClrJitCompiler()
    {
      _localJitHook = LocalHook.Create(_compileMethod, _hookMethod, this);
      _localJitHook.ThreadACL.SetExclusiveACL(new Int32[] {0});
    }
  }

  internal class BufferParam
  {
    public Type Type;
    public object RawDefaultValue;
  }
}