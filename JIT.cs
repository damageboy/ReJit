using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace HackJit
{
  [AttributeUsage(AttributeTargets.Method)]
  internal class HackJitAttribute : Attribute
  {
    internal HackJitAttribute(string replacement)
    {
      Replacement = replacement;
    }
    internal string Replacement { get; private set; }
  }

  static class JIT
  {
    [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
    [HackJit("bswap32")]
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
    [HackJit("bswap32")]
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
    [HackJit("bswap64")]
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
    [HackJit("bswap64")]
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
    [HackJit("bsf16")]
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
    [HackJit("bsf16")]
    public static uint BSF(ushort test)
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
    [HackJit("bsr16")]
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
    [HackJit("bsr16")]
    public static uint BSR(ushort test)
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
    [HackJit("bsf32")]
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
    [HackJit("bsf32")]
    public static uint BSF(uint test)
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
    [HackJit("bsr32")]
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
    [HackJit("bsr32")]
    public static uint BSR(uint test)
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
    [HackJit("bsf64")]
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
    [HackJit("bsf64")]
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
    [HackJit("bsr64")]
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
    [HackJit("bsr64")]
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
    [HackJit("popcnt16")]
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
    [HackJit("popcnt16")]
    public static uint POPCNT(ushort test)
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
    [HackJit("popcnt32")]
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
    [HackJit("popcnt32")]
    public static uint POPCNT(uint test)
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
    [HackJit("popcnt64")]
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
    [HackJit("popcnt64")]
    public static uint POPCNT(ulong test)
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
    [HackJit("rdtscp")]
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
    [HackJit("cpuid")]
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
    [HackJit("rdtsc")]
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
      var jitStream = assembly.GetManifestResourceStream("HackJit.Intrinsics." + i + ".o");
      var bytes = new byte[jitStream.Length];
      jitStream.Read(bytes, 0, bytes.Length);
      return bytes;
    }

    public static void Init()
    {
      var candidates = typeof(JIT).GetMethods(BindingFlags.Static | BindingFlags.Public);

      foreach (var m in candidates) {
        var hja = (HackJitAttribute) m.GetCustomAttributes(typeof(HackJitAttribute), false).SingleOrDefault();
        if (hja == null)
          continue;
        var mh = m.MethodHandle;
        RuntimeHelpers.PrepareMethod(mh);
        var p = mh.GetFunctionPointer();
        var code = GetOpcodes(hja.Replacement);
        if (IntPtr.Size == 8)
          Marshal.Copy(code, 0, p, code.Length);
        else
          throw new NotImplementedException();
      }
    }
  }
}