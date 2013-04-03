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
    public delegate int BSWAP32SignedDelegate(int dummy);
    public delegate uint BSWAP32UnsignedDelegate(uint dummy);
    private static void PrepareBSWAP32()
    {
      var methodSigned = ((BSWAP32SignedDelegate)BSWAP32S).Method.MethodHandle;
      var methodUnsigned = ((BSWAP32UnsignedDelegate)BSWAP32U).Method.MethodHandle;
      RuntimeHelpers.PrepareMethod(methodSigned);
      RuntimeHelpers.PrepareMethod(methodUnsigned);
      var p1 = methodSigned.GetFunctionPointer();
      var p2 = methodUnsigned.GetFunctionPointer();
      var bswap32 = GetOpcodes("bswap32");
      if (IntPtr.Size == 8) {
        Marshal.Copy(bswap32, 0, p1, bswap32.Length);
        Marshal.Copy(bswap32, 0, p2, bswap32.Length);
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
    public delegate long BSWAP64SignedDelegate(long bige, byte dummy);
    public delegate ulong BSWAP64UnsignedDelegate(ulong bige, byte dummy);
    private static void PrepareBSWAP64()
    {
      var methodSigned = ((BSWAP64SignedDelegate)BSWAP64S).Method.MethodHandle;
      var methodUnsigned = ((BSWAP64UnsignedDelegate)BSWAP64U).Method.MethodHandle;
      RuntimeHelpers.PrepareMethod(methodSigned);
      RuntimeHelpers.PrepareMethod(methodUnsigned);
      var p1 = methodSigned.GetFunctionPointer();
      var p2 = methodUnsigned.GetFunctionPointer();
      var bswap64 = GetOpcodes("bswap64");
      if (IntPtr.Size == 8)
      {
        Marshal.Copy(bswap64, 0, p1, bswap64.Length);
        Marshal.Copy(bswap64, 0, p2, bswap64.Length);
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

    #region RDTSCP
    public delegate ulong RDTSCPDelegate(int dummy);
    private static void PrepareRDTSCP()
    {
      var method = ((RDTSCPDelegate)RDTSCP).Method.MethodHandle;
      RuntimeHelpers.PrepareMethod(method);
      var rdtscp = GetOpcodes("rdtscp");
      var p = method.GetFunctionPointer();

      if (IntPtr.Size == 8)
        Marshal.Copy(rdtscp, 0, p, rdtscp.Length);
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
    public delegate void CPUIDDelegate(ref uint eax, out uint ebx, out uint ecx, out uint edx, uint dummy1, ushort dummy2, byte dummy3);
    private static void PrepareCPUID()
    {  
      var method = ((CPUIDDelegate)CPUID).Method.MethodHandle;
      RuntimeHelpers.PrepareMethod(method);
      var p = method.GetFunctionPointer();
      var cpuid = GetOpcodes("cpuid");
      if (IntPtr.Size == 8)
        Marshal.Copy(cpuid, 0, p, cpuid.Length);
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
    public delegate ulong RDTSCDelegate(int dummy1, byte dummy2);
    private static void PrepareRDTSC()
    {
      var method = ((RDTSCDelegate)RDTSC).Method.MethodHandle;
      RuntimeHelpers.PrepareMethod(method);
      var p = method.GetFunctionPointer();
      var rdtsc = GetOpcodes("rdtsc");
      if (IntPtr.Size == 8)
        Marshal.Copy(rdtsc, 0, p, rdtsc.Length);
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
      PrepareRDTSC();    
      PrepareRDTSCP();
    
      //PrepareBSWAP32();
    }
  }
}