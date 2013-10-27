using System.Reflection;
using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace ReJit.Sample
{
  class SampleProgram
  {
    public const int MHZ = 2400000;
    private static Logger Log = LogManager.GetCurrentClassLogger();

    [Conditional("DEBUG")]
    static void SetupLogging()
    {
      var config = new LoggingConfiguration();
      var consoleTarget = new ConsoleTarget {
        Layout = @"${date:format=HH\:MM\:ss} ${message}${onexception:${newline}${exception:format=tostring}}"
      };
      config.AddTarget("console", consoleTarget);

      var rule = new LoggingRule("*", LogLevel.Debug, consoleTarget);
      config.LoggingRules.Add(rule);

      LogManager.Configuration = config;
    }

    static public void Main(string[] args)
    {      
      SetupLogging();
      DumpAllMethodHandles();
      Intrinsincs.Init();
      var samplesThread = new Thread(RunSamples);
      samplesThread.Start();
      samplesThread.Join();
    }

    static public void RunSamples()
    {
      TestCPUID();
      TestBSWAP32();
      //TestBSWAP64();
      TestBSF16();
      TestBSF32();
      TestBSF64();
      TestBSR16();
      TestBSR32();
      TestBSR64();

      TestRDTSC();
      if (CPUID.ISSupported(CPUID.CPUIDFeature.RDTSCP))
        TestRDTSCP();
      else
        Console.WriteLine("RDTSCP not supported, skipping");

    }

    private static void DumpAllMethodHandles()
    {
      var candidates = typeof(SampleProgram).GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
      foreach (var m in candidates)
        Log.Debug("{0} -> 0x{1:X}", m.Name, m.MethodHandle.Value.ToInt64());      
    }

    private static void TestBSF16()
    {
      var test = (ushort) 0x0100U;

      var location = Intrinsincs.BSF(test);
      Console.WriteLine("BSF16 {0}", location);
    }

    private static void TestBSF32()
    {
      var test = 0x00000100U;

      var location = Intrinsincs.BSF(test);
      Console.WriteLine("BSF32 {0}", location);
    }

    private static void TestBSF64()
    {
      var test = 0x00000100U;

      var location = Intrinsincs.BSF(test);
      Console.WriteLine("BSF64 {0}", location);
    }

    private static void TestBSR16()
    {
      var test = (ushort)0x0100U;

      var location = Intrinsincs.BSR(test);
      Console.WriteLine("BSR16 {0}", location);
    }

    private static void TestBSR32()
    {
      var test = 0x00000100U;

      var location = Intrinsincs.BSR(test);
      Console.WriteLine("BSR32 {0}", location);
    }

    private static void TestBSR64()
    {
      var test = 0x00000100U;

      var location = Intrinsincs.BSR(test);
      Console.WriteLine("BSR32 {0}", location);
    }

    private unsafe static void TestCPUID()
    {
      uint eax, ebx, ecx, edx;
      eax = 0x00;
      ebx = ecx = edx = 0x00;

      Intrinsincs.CPUID(ref eax, out ebx, out ecx, out edx);
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
      var after = Intrinsincs.BSWAP32U(before);
      Console.WriteLine("After  BSWAP32 0x{0:X8}", after);
    }

    private static void TestBSWAP64()
    {
      Debugger.Break();
      var before = 0x1122334455667788U;
      Console.WriteLine("Before BSWAP64 0x{0:X8}", before);
      var after = Intrinsincs.BSWAP64U(before);
      Console.WriteLine("After  BSWAP64 0x{0:X8}", after);
    }

    private static void TestRDTSCP()
    {
      if (!CPUID.ISSupported(CPUID.CPUIDFeature.InvariantTSC))
        Console.WriteLine("Invariant TSC is not supported, RDTSC isn't reliable as a wall clock");
      const int LOOP = 1000000;
      var sw = Stopwatch.StartNew();
      var start = Intrinsincs.RDTSCP();

      for (var i = 0; i < LOOP; i++)
        Intrinsincs.RDTSCP();
      var end = Intrinsincs.RDTSCP();
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
      var start = Intrinsincs.RDTSC();
      for (var i = 0; i < LOOP; i++)
        Intrinsincs.RDTSC();
      var end = Intrinsincs.RDTSC();
      sw.Stop();
      Console.WriteLine("SW:    {0}ms", sw.ElapsedMilliseconds);
      Console.WriteLine("RDTSC: {0}ms", (end - start) / MHZ);
      Console.WriteLine("Each RDTSC: {0}cycles", (end - start) / LOOP);
    }
  }
}