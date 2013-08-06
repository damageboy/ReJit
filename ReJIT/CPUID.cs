using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ReJit
{
  public class CPUID
  {
    static string _vendorString;
    static uint _maxSupported;
    static uint _maxExtendedSupported;
    static Dictionary<uint, uint[]> _values;
    private static readonly Dictionary<CPUIDFeature, CPUIDFeaturesAttribute> _featureInfo;


    static CPUID() 
    {
      uint eax, ebx, ecx, edx;
      eax = 0x00;
      ebx = ecx = edx = 0x00;
      _values = new Dictionary<uint, uint[]>();
      Intrinsincs.CPUID(ref eax, out ebx, out ecx, out edx);
      _maxSupported = eax;
      eax = 0x80000000;
      Intrinsincs.CPUID(ref eax, out ebx, out ecx, out edx);
      _maxExtendedSupported = eax;

      for (var i = 0U; i <= _maxSupported; i++)
      {
        eax = i;
        ebx = ecx = edx = 0x00;
        Intrinsincs.CPUID(ref eax, out ebx, out ecx, out edx);
        _values[i] = new [] { eax, ebx, ecx, edx };
      }

      for (var i = 0x80000000U; i <= _maxSupported; i++)
      {
        eax = i;
        ebx = ecx = edx = 0x00;
        Intrinsincs.CPUID(ref eax, out ebx, out ecx, out edx);
        _values[i] = new[] { eax, ebx, ecx, edx };
      }

      var q = 
        from m in typeof(CPUIDFeature).GetMembers()
        let attr = (CPUIDFeaturesAttribute) m.GetCustomAttributes(typeof (CPUIDFeaturesAttribute), false).Single()
        select new {
          Feature = (CPUIDFeature) Enum.Parse(typeof(CPUIDFeature), m.Name),
          Attr = attr
        };
      _featureInfo = q.ToDictionary(x => x.Feature, x => x.Attr);
    }

    public unsafe string VendorString
    {
      get {
        if (_vendorString == null) {
          uint eax, ebx, ecx, edx;
          eax = 0x00;
          ebx = ecx = edx = 0x00;

          Intrinsincs.CPUID(ref eax, out ebx, out ecx, out edx);
          var x = stackalloc sbyte[12];
          var p = (uint*)x;
          p[0] = _values[0][EBX];
          p[1] = _values[0][EDX];
          p[2] = _values[0][ECX];
      

          _vendorString = new string(x, 0, 12);
        }
        return _vendorString;
      }
    }

    public static bool ISSupported(CPUIDFeature feature)
    {
      var info = _featureInfo[feature];
      return (_values[info.Function][info.Register] & (1U << info.Bit)) == 1;
    }

    public const int EAX = 0;
    public const int EBX = 1;
    public const int ECX = 2;
    public const int EDX = 3;

    [AttributeUsage(AttributeTargets.Field)]
    public class CPUIDFeaturesAttribute : Attribute 
    {
      public CPUIDFeaturesAttribute(uint function, int register, int bit)
      {
        Function = function;
        Register = register;
        Bit = bit;        
      }

      public uint Function { get; private set; }
      public int Register { get; private set; }
      public int Bit { get; private set; }
    }

    public enum CPUIDFeature
    {
      [Description("Streaming SIMD Extensions 3 (SSE3)")]
      [CPUIDFeatures(0x01, ECX, 0)]
      SSE3,
      [Description("PCLMULQDQ instruction")]
      [CPUIDFeatures(0x01, ECX, 1)]
      PCLMULQDQ,
      [Description("64-bit DS Area")]
      [CPUIDFeatures(0x01, ECX, 2)]
      DTES64,
      [Description("MONITOR/MWAIT")]
      [CPUIDFeatures(0x01, ECX, 3)]
      Monitor,
      [Description("CPL Qualified Debug Store")]
      [CPUIDFeatures(0x01, ECX, 4)]
      DSCPL,
      [Description("Virtual Machine Extensions")]
      [CPUIDFeatures(0x01, ECX, 5)]
      VMX,
      [Description("Safer Mode Extensions")]
      [CPUIDFeatures(0x01, ECX, 6)]
      SMX,
      [Description("Enhanced Intel SpeedStep® technology")]
      [CPUIDFeatures(0x01, ECX, 7)]
      EIST,
      [Description("Thermal Monitor 2")]
      [CPUIDFeatures(0x01, ECX, 8)]
      TM2,
      [Description("Supplemental Streaming SIMD Extensions 3 (SSSE3)")]
      [CPUIDFeatures(0x01, ECX, 9)]
      SSSE3,
      [Description("L1 Context ID")]
      [CPUIDFeatures(0x01, ECX, 10)]
      CNXTID,
      [Description("FMA extensions")]
      [CPUIDFeatures(0x01, ECX, 12)]
      FMA,

      [Description("CMPXCHG16B Instruction")]
      [CPUIDFeatures(0x01, ECX, 13)]
      CX16,
      [Description("xTPR Update Control")]
      [CPUIDFeatures(0x01, ECX, 14)]
      XTPRUpdate,
      [Description("Perfmon and Debug Capability")]
      [CPUIDFeatures(0x01, ECX, 15)]
      PDCM,
      [Description("Process-context identifiers")]
      [CPUIDFeatures(0x01, ECX, 17)]
      PCID,
      [Description("Direct Cache Access")]
      [CPUIDFeatures(0x01, ECX, 18)]
      DCA,
      [Description("Streaming SIMD Extensions 4.1 (SSE4.1)")]
      [CPUIDFeatures(0x01, ECX, 19)]
      SSE41,
      [Description("Streaming SIMD Extensions 4.2 (SSE4.2)")]
      [CPUIDFeatures(0x01, ECX, 20)]
      SSE42,
      [Description("x2APIC")]
      [CPUIDFeatures(0x01, ECX, 21)]
      X2APIC,
      [Description("MOVBE Instruction")]
      [CPUIDFeatures(0x01, ECX, 22)]
      MOVBE,
      [Description("POPCNT Instruction")]
      [CPUIDFeatures(0x01, ECX, 23)]
      POPCNT,
      [Description("APIC timer supports TSC Deadline value")]
      [CPUIDFeatures(0x01, ECX, 24)]
      TSCDeadline,
      [Description("AESNI Instructions")]
      [CPUIDFeatures(0x01, ECX, 25)]
      AESNI,
      [Description("XSAVE/XRSTOR processor extended state feature")]
      [CPUIDFeatures(0x01, ECX, 26)]
      XSAVE,      
      [Description("XSETBV/XGETBV instructions to access XCR0,")]
      [CPUIDFeatures(0x01, ECX, 27)]
      OSXSAVE,
      [Description("AVX")]
      [CPUIDFeatures(0x01, ECX, 28)]
      AVX,
      [Description("16-bit floating-point conversion")]
      [CPUIDFeatures(0x01, ECX, 29)]
      F16C,
      [Description("RDRAND Instruction")]
      [CPUIDFeatures(0x01, ECX, 30)]
      RDRAND,


      [Description("Floating Point Unit On-Chip")]
      [CPUIDFeatures(0x01, EDX, 0)]
      FPU,
      [Description("Virtual 8086 Mode Enhancements")]
      [CPUIDFeatures(0x01, EDX, 1)]
      VME,
      [Description("Debugging Extensions")]
      [CPUIDFeatures(0x01, EDX, 2)]
      DE,
      [Description("Page Size Extension")]
      [CPUIDFeatures(0x01, EDX, 3)]
      PSE,
      [Description("Time Stamp Counter")]
      [CPUIDFeatures(0x01, EDX, 4)]
      TSC,
      [Description("Model Specific Registers RDMSR and WRMSR Instructions")]
      [CPUIDFeatures(0x01, EDX, 5)]
      MSR,
      [Description("Physical Address Extension")]
      [CPUIDFeatures(0x01, EDX, 6)]
      PAE,
      [Description("Machine Check Exception")]
      [CPUIDFeatures(0x01, EDX, 7)]
      MCE,
      [Description("CMPXCHG8B Instruction")]
      [CPUIDFeatures(0x01, EDX, 8)]
      CX8,
      [Description("APIC On-Chip")]
      [CPUIDFeatures(0x01, EDX, 9)]
      APIC,
      [Description("SYSENTER and SYSEXIT Instructions")]
      [CPUIDFeatures(0x01, EDX, 11)]
      SYSENTER,
      [Description("Memory Type Range Registers")]
      [CPUIDFeatures(0x01, EDX, 12)]
      MTRR,
      [Description("Page Global Bit")]
      [CPUIDFeatures(0x01, EDX, 13)]
      PGE,
      [Description("Machine Check Architecture")]
      [CPUIDFeatures(0x01, EDX, 14)]
      MCA,
      [Description("Conditional Move Instructions")]
      [CPUIDFeatures(0x01, EDX, 15)]
      CMOV,
      [Description("Page Attribute Table")]
      [CPUIDFeatures(0x01, EDX, 16)]
      PAT,
      [Description("36-Bit Page Size Extension")]
      [CPUIDFeatures(0x01, EDX, 17)]
      PSE36,
      [Description("Processor Serial Number")]
      [CPUIDFeatures(0x01, EDX, 18)]
      PSN,
      [Description("CLFLUSH Instruction")]
      [CPUIDFeatures(0x01, EDX, 19)]
      CLFLUSH,
      [Description("Debug Store")]
      [CPUIDFeatures(0x01, EDX, 21)]
      DS,
      [Description("Thermal Monitor and Software Controlled Clock Facilities")]
      [CPUIDFeatures(0x01, EDX, 22)]
      ACPI,
      [Description("Intel MMX Technology")]
      [CPUIDFeatures(0x01, EDX, 23)]
      MMX,
      [Description("FXSAVE and FXRSTOR Instructions")]
      [CPUIDFeatures(0x01, EDX, 24)]
      FSXR,
      [Description("Streaming SIMD Extensions (SSE)")]
      [CPUIDFeatures(0x01, EDX, 25)]
      SSE,
      [Description("Streaming SIMD Extensions 2 (SSE2)")]
      [CPUIDFeatures(0x01, EDX, 26)]
      SSE2,
      [Description("Self Snoop")]
      [CPUIDFeatures(0x01, EDX, 27)]
      SS,
      [Description("Max APIC IDs reserved field is Valid")]
      [CPUIDFeatures(0x01, EDX, 28)]
      HTT,
      [Description("Thermal Monitor")]
      [CPUIDFeatures(0x01, EDX, 29)]
      TM,
      [Description("Pending Break Enable")]
      [CPUIDFeatures(0x01, EDX, 31)]
      PBE,

      
      [Description("SYSCALL/SYSENTER in 64 bit mode")]
      [CPUIDFeatures(0x80000001, EDX, 11)]
      SYSCALL64,
      [Description("No Execute But")]
      [CPUIDFeatures(0x80000001, EDX, 20)]
      NX,
      [Description("1GB Pages")]
      [CPUIDFeatures(0x80000001, EDX, 26)]
      GBP,
      [Description("RDTSCP and IA32_TSC_AUX")]
      [CPUIDFeatures(0x80000001, EDX, 27)]
      RDTSCP,
      [Description("Intel 64 Architecture available")]
      [CPUIDFeatures(0x80000001, EDX, 29)]
      INTEL64,
      [Description("Invariant TSC Available")]
      [CPUIDFeatures(0x80000007, EDX, 8)]
      InvariantTSC,

    }    
  }
}
