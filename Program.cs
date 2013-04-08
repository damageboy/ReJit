using System;
using System.Diagnostics;

namespace HackJit
{
  class Program
  {
    public const int MHZ = 2400000;
    static void Main(string[] args)
    {
      JIT.Init();
      TestCPUID();
      TestBSWAP32();
      //TestBSWAP64();
      TestBSF16();
      TestBSF32();
      TestBSF64();
      TestBSR16();
      TestBSR32();
      TestBSR64();

      if (CPUID.ISSupported(CPUID.CPUIDFeature.POPCNT))
      {
        TestPOPCNT16();
        TestPOPCNT32();
        TestPOPCNT64();
      } else
        Console.WriteLine("POPCNT not supported, skipping");

      TestRDTSC();
      if (CPUID.ISSupported(CPUID.CPUIDFeature.RDTSCP))
        TestRDTSCP();
      else
        Console.WriteLine("RDTSCP not supported, skipping");

      
    }

    private static void TestBSF16()
    {
      var test = (ushort) 0x0100U;

      var location = JIT.BSF(test);
      Console.WriteLine("BSF16 {0}", location);
    }

    private static void TestBSF32()
    {
      var test = 0x00000100U;
      
      var location = JIT.BSF(test);
      Console.WriteLine("BSF32 {0}", location);
    }

    private static void TestBSF64()
    {
      var test = 0x00000100U;

      var location = JIT.BSF(test);
      Console.WriteLine("BSF64 {0}", location);
    }

    private static void TestBSR16()
    {
      var test = (ushort)0x0100U;

      var location = JIT.BSR(test);
      Console.WriteLine("BSR16 {0}", location);
    }

    private static void TestBSR32()
    {
      var test = 0x00000100U;

      var location = JIT.BSR(test);
      Console.WriteLine("BSR32 {0}", location);
    }

    private static void TestBSR64()
    {
      var test = 0x00000100U;

      var location = JIT.BSR(test);
      Console.WriteLine("BSR32 {0}", location);
    }

    private static void TestPOPCNT16()
    {
      var test = (ushort) 0x0F0FU;

      var location = JIT.POPCNT(test);
      Console.WriteLine("POPCNT16 {0}", location);
    }

    private static void TestPOPCNT32()
    {
      var test = 0x0F0F0F0FU;

      var location = JIT.POPCNT(test);
      Console.WriteLine("POPCNT32 {0}", location);
    }

    private static void TestPOPCNT64()
    {
      var test = 0x0F0F0F0F0F0F0F0FLU;

      var location = JIT.POPCNT(test);
      Console.WriteLine("POPCNT64 {0}", location);
    }


    private unsafe static void TestCPUID()
    {
      uint eax, ebx, ecx, edx;
      eax = 0x00;
      ebx = ecx = edx = 0x00;
    
      JIT.CPUID(ref eax, out ebx, out ecx, out edx);
      var x = stackalloc sbyte[12];
      var p = (uint*) x;
      p[0] = ebx;
      p[1] = edx;
      p[2] = ecx;

      var cpuid0s = new string(x, 0, 12);

      Console.WriteLine("CPUID: {0}", cpuid0s);
    }

    private static void TestBSWAP32()
    {
      var before = 0x01020304U;
      Console.WriteLine("Before BSWAP32 0x{0:X8}", before);
      var after = JIT.BSWAP32U(before);      
      Console.WriteLine("After  BSWAP32 0x{0:X8}", after);
    }

    private static void TestBSWAP64()
    {
      Debugger.Break();
      var before = 0x1122334455667788U;
      Console.WriteLine("Before BSWAP64 0x{0:X8}", before);
      var after = JIT.BSWAP64U(before);            
      Console.WriteLine("After  BSWAP64 0x{0:X8}", after);
    }

    private static void TestRDTSCP()
    {
      if (!CPUID.ISSupported(CPUID.CPUIDFeature.InvariantTSC))
        Console.WriteLine("Invariant TSC is not supported, RDTSC isn't reliable as a wall clock");
      const int LOOP = 1000000;
      var sw = Stopwatch.StartNew();
      var start = JIT.RDTSCP();

      for (var i = 0; i < LOOP; i++)
        JIT.RDTSCP();
      var end = JIT.RDTSCP();
      sw.Stop();    
      Console.WriteLine("SW:     {0}ms",sw.ElapsedMilliseconds);
      Console.WriteLine("RDTSCP: {0}ms", (end - start) / MHZ);
      Console.WriteLine("RDTSCP: {0}cycles", (end - start) / LOOP);
    }


    private static void TestRDTSC()
    {
      if (!CPUID.ISSupported(CPUID.CPUIDFeature.InvariantTSC))
        Console.WriteLine("Invariant TSC is not supported, RDTSC isn't reliable as a wall clock");

      const int LOOP = 1000000;

      var sw = Stopwatch.StartNew();
      var start = JIT.RDTSC();
      for (var i = 0; i < LOOP; i++)
        JIT.RDTSC();
      var end = JIT.RDTSC();
      sw.Stop();    
      Console.WriteLine("SW:    {0}ms", sw.ElapsedMilliseconds);
      Console.WriteLine("RDTSC: {0}ms", (end - start) / MHZ);
      Console.WriteLine("Each RDTSC: {0}cycles", (end - start) / LOOP);
    }
  }
}