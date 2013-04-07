using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace HackJit
{
  static class JIT
  {
    ///Patching instructions like this is not "thread" safe.
  
    #region BSWAP32
    private static void PrepareBSWAP32()
    {
      var methodSigned = typeof(JIT).GetMethod("BSWAP32S", new[] { typeof(int) }).MethodHandle;
      var methodUnsigned = typeof(JIT).GetMethod("BSWAP32U", new[] { typeof(uint) }).MethodHandle;
      RuntimeHelpers.PrepareMethod(methodSigned);
      RuntimeHelpers.PrepareMethod(methodUnsigned);
      var p1 = methodSigned.GetFunctionPointer();
      var p2 = methodUnsigned.GetFunctionPointer();
      var code = GetOpcodes("bswap32");
      if (IntPtr.Size == 8) {
        Marshal.Copy(code, 0, p1, code.Length);
        Marshal.Copy(code, 0, p2, code.Length);
      }
      else
        throw new NotImplementedException();
    }
    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
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
    #endregion

    #region BSWAP64
    private static void PrepareBSWAP64()
    {
      var methodSigned = typeof(JIT).GetMethod("BSWAP64S").MethodHandle;
      var methodUnsigned = typeof(JIT).GetMethod("BSWAP64U").MethodHandle;
      RuntimeHelpers.PrepareMethod(methodSigned);
      RuntimeHelpers.PrepareMethod(methodUnsigned);
      var p1 = methodSigned.GetFunctionPointer();
      var p2 = methodUnsigned.GetFunctionPointer();
      var code = GetOpcodes("bswap64");
      if (IntPtr.Size == 8)
      {
        Marshal.Copy(code, 0, p1, code.Length);
        Marshal.Copy(code, 0, p2, code.Length);
      }
      else
        throw new NotImplementedException();
    }
    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
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
    #endregion

    #region BSF32
    private static void PrepareBSF32()
    {
      var methodSigned = typeof (JIT).GetMethod("BSF32", new[] {typeof (int)}).MethodHandle;
      var methodUnsigned = typeof (JIT).GetMethod("BSF32", new[] {typeof (uint)}).MethodHandle;
      RuntimeHelpers.PrepareMethod(methodSigned);
      RuntimeHelpers.PrepareMethod(methodUnsigned);
      var p1 = methodSigned.GetFunctionPointer();
      var p2 = methodUnsigned.GetFunctionPointer();
      var code = GetOpcodes("bsf32");
      if (IntPtr.Size == 8)
      {
        Marshal.Copy(code, 0, p1, code.Length);
        Marshal.Copy(code, 0, p2, code.Length);
      }
      else
        throw new NotImplementedException();
    }
    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public static int BSF32(int test)
    {
      BSF32(test);
      BSF32(test);
      BSF32(test);
      BSF32(test);
      BSF32(test);
      BSF32(test);
      return BSF32(test);
    }
    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public static uint BSF32(uint test)
    {
      BSF32(test);
      BSF32(test);
      BSF32(test);
      BSF32(test);
      BSF32(test);
      BSF32(test);
      return BSF32(test);
    }
    #endregion

    #region BSR32
    private static void PrepareBSR32()
    {
      var methodSigned = typeof(JIT).GetMethod("BSR32", new[] { typeof(int) }).MethodHandle;
      var methodUnsigned = typeof(JIT).GetMethod("BSR32", new[] { typeof(uint) }).MethodHandle;
      RuntimeHelpers.PrepareMethod(methodSigned);
      RuntimeHelpers.PrepareMethod(methodUnsigned);
      var p1 = methodSigned.GetFunctionPointer();
      var p2 = methodUnsigned.GetFunctionPointer();
      var code = GetOpcodes("bsr32");
      if (IntPtr.Size == 8)
      {
        Marshal.Copy(code, 0, p1, code.Length);
        Marshal.Copy(code, 0, p2, code.Length);
      }
      else
        throw new NotImplementedException();
    }
    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public static int BSR32(int test)
    {
      BSR32(test);
      BSR32(test);
      BSR32(test);
      BSR32(test);
      BSR32(test);
      BSR32(test);
      return BSR32(test);
    }
    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public static uint BSR32(uint test)
    {
      BSR32(test);
      BSR32(test);
      BSR32(test);
      BSR32(test);
      BSR32(test);
      BSR32(test);
      return BSR32(test);
    }
    #endregion

    #region BSF64
    private static void PrepareBSF64()
    {
      var methodSigned = typeof(JIT).GetMethod("BSF64", new[] { typeof(long) }).MethodHandle;
      var methodUnsigned = typeof(JIT).GetMethod("BSF64", new[] { typeof(ulong) }).MethodHandle;
      RuntimeHelpers.PrepareMethod(methodSigned);
      RuntimeHelpers.PrepareMethod(methodUnsigned);
      var p1 = methodSigned.GetFunctionPointer();
      var p2 = methodUnsigned.GetFunctionPointer();
      var code = GetOpcodes("bsf64");
      if (IntPtr.Size == 8)
      {
        Marshal.Copy(code, 0, p1, code.Length);
        Marshal.Copy(code, 0, p2, code.Length);
      }
      else
        throw new NotImplementedException();
    }
    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public static int BSF64(long test)
    {
      BSF64(test);
      BSF64(test);
      BSF64(test);
      BSF64(test);
      BSF64(test);
      BSF64(test);
      return BSF64(test);
    }
    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public static uint BSF64(ulong test)
    {
      BSF64(test);
      BSF64(test);
      BSF64(test);
      BSF64(test);
      BSF64(test);
      BSF64(test);
      return BSF64(test);
    }
    #endregion

    #region BSR64
    private static void PrepareBSR64()
    {
      var methodSigned = typeof(JIT).GetMethod("BSR64", new[] { typeof(long) }).MethodHandle;
      var methodUnsigned = typeof(JIT).GetMethod("BSR64", new[] { typeof(ulong) }).MethodHandle;
      RuntimeHelpers.PrepareMethod(methodSigned);
      RuntimeHelpers.PrepareMethod(methodUnsigned);
      var p1 = methodSigned.GetFunctionPointer();
      var p2 = methodUnsigned.GetFunctionPointer();
      var code = GetOpcodes("bsr64");
      if (IntPtr.Size == 8)
      {
        Marshal.Copy(code, 0, p1, code.Length);
        Marshal.Copy(code, 0, p2, code.Length);
      }
      else
        throw new NotImplementedException();
    }
    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public static int BSR64(long test)
    {
      BSR64(test);
      BSR64(test);
      BSR64(test);
      BSR64(test);
      BSR64(test);
      BSR64(test);
      return BSR64(test);
    }
    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    public static uint BSR64(ulong test)
    {
      BSR64(test);
      BSR64(test);
      BSR64(test);
      BSR64(test);
      BSR64(test);
      BSR64(test);
      return BSR64(test);
    }
    #endregion

    #region RDTSCP
    private static void PrepareRDTSCP()
    {
      var method = typeof(JIT).GetMethod("RDTSCP").MethodHandle;
      RuntimeHelpers.PrepareMethod(method);
      var code = GetOpcodes("rdtscp");
      var p = method.GetFunctionPointer();

      if (IntPtr.Size == 8)
        Marshal.Copy(code, 0, p, code.Length);
      else
        throw new NotImplementedException();
    }
    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
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
    #endregion

    #region CPUID
    private static void PrepareCPUID()
    {
      var method = typeof(JIT).GetMethod("CPUID").MethodHandle;
      RuntimeHelpers.PrepareMethod(method);
      var p = method.GetFunctionPointer();
      var code = GetOpcodes("cpuid");
      if (IntPtr.Size == 8)
        Marshal.Copy(code, 0, p, code.Length);
      else
        throw new NotImplementedException();
    }

    private static byte[] GetOpcodes(string i)
    {
      var assembly = Assembly.GetExecutingAssembly();
      var jitStream = assembly.GetManifestResourceStream("HackJit.Intrinsics." + i + ".o");
      var bytes = new byte[jitStream.Length];
      jitStream.Read(bytes, 0, bytes.Length);
      return bytes;
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
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
    #endregion

    #region RDTSC
    private static void PrepareRDTSC()
    {
      var method = typeof(JIT).GetMethod("RDTSC").MethodHandle;
      RuntimeHelpers.PrepareMethod(method);
      var p = method.GetFunctionPointer();
      var code = GetOpcodes("rdtsc");
      if (IntPtr.Size == 8)
        Marshal.Copy(code, 0, p, code.Length);
      else
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
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
    #endregion

    public static void Init()
    {
      PrepareCPUID();
      PrepareBSWAP32();
      PrepareBSWAP64();
      PrepareBSF32();
      PrepareBSR32();
      PrepareBSF64();
      PrepareBSR64();
      PrepareRDTSC();
      PrepareRDTSCP();
    }
  }
}