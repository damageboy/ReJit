using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HackJit
{
  public class CPUID
  {
    static string _vendorString;
    static uint _eax01, _ebx01, _ecx01, _edx01;
    static uint _eax80000001, _ebx80000001, _ecx80000001, _edx80000001;
    static uint _eax80000007, _ebx80000007, _ecx80000007, _edx80000007;
    static bool _f01;
    static bool _f80000001;
    static bool _f80000007;
    static uint _maxSupported;
    static uint _maxExtendedSupported;

    static CPUID() 
    {
      uint eax, ebx, ecx, edx;
      eax = 0x00;
      ebx = ecx = edx = 0x00;
      JIT.CPUID(ref eax, out ebx, out ecx, out edx);
      _maxSupported = eax;
      eax = 0x80000000;
      JIT.CPUID(ref eax, out ebx, out ecx, out edx);
      _maxExtendedSupported = eax;
    }

    public unsafe string VendorString
    {
      get {
        if (_vendorString == null) {
          uint eax, ebx, ecx, edx;
          eax = 0x00;
          ebx = ecx = edx = 0x00;

          JIT.CPUID(ref eax, out ebx, out ecx, out edx);
          var x = stackalloc sbyte[12];
          var p = (uint*)x;
          p[0] = ebx;
          p[1] = edx;
          p[2] = ecx;
      

          _vendorString = new string(x, 0, 12);
        }
        return _vendorString;
      }
    }

    private static void Get01()
    {
      if (_f01)
        return;
      if (_maxSupported < 0x01)
        return;
      _eax01 = 0x01;
      JIT.CPUID(ref _eax01, out _ebx01, out _ecx01, out _edx01);
      _f01 = true;
    }

    private static void Get80000001()
    {
      if (_f80000001)
        return;
      if (_maxExtendedSupported < 0x80000001)
        return;
      _eax01 = 0x80000001;
      JIT.CPUID(ref _eax80000001, out _ebx80000001, out _ecx80000001, out _edx80000001);
      _f80000001 = true;
    }


    private static void Get80000007()
    {
      if (_f80000007)
        return;
      if (_maxExtendedSupported < 0x80000007)
        return;
      _eax01 = 0x80000007;
      JIT.CPUID(ref _eax80000007, out _ebx80000007, out _ecx80000007, out _edx80000007);
      _f80000007 = true;
    }

    public static bool SSE3Supported     { get { Get01(); return (_ecx01 & (1 << 0)) == 1; } }
    public static bool MonitorSupported  { get { Get01(); return (_ecx01 & (1 << 3)) == 1; } }
    public static bool VMXSupported      { get { Get01(); return (_ecx01 & (1 << 5)) == 1; } }
    public static bool SMXSupported      { get { Get01(); return (_ecx01 & (1 << 6)) == 1; } }
    public static bool EISTSupported     { get { Get01(); return (_ecx01 & (1 << 7)) == 1; } }
    public static bool SSSE3Supported    { get { Get01(); return (_ecx01 & (1 << 9)) == 1; } }
    public static bool CX16Supported     { get { Get01(); return (_ecx01 & (1 << 13)) == 1; } }
    public static bool PCIDSupported     { get { Get01(); return (_ecx01 & (1 << 17)) == 1; } }
    public static bool DCASupported      { get { Get01(); return (_ecx01 & (1 << 18)) == 1; } }
    public static bool SSE41Supported    { get { Get01(); return (_ecx01 & (1 << 19)) == 1; } }
    public static bool SSE42Supported    { get { Get01(); return (_ecx01 & (1 << 20)) == 1; } }
    public static bool X2APICSupported   { get { Get01(); return (_ecx01 & (1 << 21)) == 1; } }
    public static bool MOVBESupported    { get { Get01(); return (_ecx01 & (1 << 22)) == 1; } }
    public static bool POPCNTSupported   { get { Get01(); return (_ecx01 & (1 << 23)) == 1; } }
    
    public static bool AESNISupported    { get { Get01(); return (_ecx01 & (1 << 25)) == 1; } }
    public static bool AVXSupported      { get { Get01(); return (_ecx01 & (1 << 28)) == 1; } }
    public static bool F16CSupported     { get { Get01(); return (_ecx01 & (1 << 29)) == 1; } }
    public static bool RDRANDSupported   { get { Get01(); return (_ecx01 & (1 << 30)) == 1; } }

    public static bool FPUSupported      { get { Get01(); return (_edx01 & (1 << 0))  == 1; } }
    public static bool VMESupported      { get { Get01(); return (_edx01 & (1 << 1))  == 1; } }
    public static bool DESupported       { get { Get01(); return (_edx01 & (1 << 2))  == 1; } }
    public static bool PSESupported      { get { Get01(); return (_edx01 & (1 << 3))  == 1; } }
    public static bool TSCSupported      { get { Get01(); return (_edx01 & (1 << 4))  == 1; } }
    public static bool MSRSupported      { get { Get01(); return (_edx01 & (1 << 5))  == 1; } }
    public static bool PAESupported      { get { Get01(); return (_edx01 & (1 << 6))  == 1; } }
    public static bool MCESupported      { get { Get01(); return (_edx01 & (1 << 7))  == 1; } }
    public static bool CX8Supported      { get { Get01(); return (_edx01 & (1 << 8))  == 1; } }
    public static bool APICSupported     { get { Get01(); return (_edx01 & (1 << 9))  == 1; } }
    public static bool SysenterSupported { get { Get01(); return (_edx01 & (1 << 11)) == 1; } }
    public static bool MTRRSupported     { get { Get01(); return (_edx01 & (1 << 12)) == 1; } }
    public static bool PGESupported      { get { Get01(); return (_edx01 & (1 << 13)) == 1; } }
    public static bool MCASupported      { get { Get01(); return (_edx01 & (1 << 14)) == 1; } }
    public static bool CMOVSupported     { get { Get01(); return (_edx01 & (1 << 15)) == 1; } }
    public static bool PATSupported      { get { Get01(); return (_edx01 & (1 << 16)) == 1; } }
    public static bool PSE36Supported    { get { Get01(); return (_edx01 & (1 << 17)) == 1; } }
    public static bool PSNSupported      { get { Get01(); return (_edx01 & (1 << 18)) == 1; } }
    public static bool CLFLUSHSupported  { get { Get01(); return (_edx01 & (1 << 19)) == 1; } }
    public static bool DSSupported       { get { Get01(); return (_edx01 & (1 << 21)) == 1; } }
    public static bool ACPISupported     { get { Get01(); return (_edx01 & (1 << 22)) == 1; } }
    public static bool MMXSupported      { get { Get01(); return (_edx01 & (1 << 23)) == 1; } }
    public static bool FXSRSupported     { get { Get01(); return (_edx01 & (1 << 24)) == 1; } }
    public static bool SSESupported      { get { Get01(); return (_edx01 & (1 << 25)) == 1; } }
    public static bool SSE2Supported     { get { Get01(); return (_edx01 & (1 << 26)) == 1; } }
    public static bool SSSupported       { get { Get01(); return (_edx01 & (1 << 27)) == 1; } }
    public static bool HTTSupported      { get { Get01(); return (_edx01 & (1 << 28)) == 1; } }
    public static bool TMSupported       { get { Get01(); return (_edx01 & (1 << 29)) == 1; } }
    public static bool PBESupported      { get { Get01(); return (_edx01 & (1 << 31)) == 1; } }

    public static bool SEP64Supported   { get { Get80000001(); return (_edx01 & (1 << 11)) == 1; } }
    public static bool NXSupported      { get { Get80000001(); return (_edx01 & (1 << 20)) == 1; } }
    public static bool GBPSupported     { get { Get80000001(); return (_edx01 & (1 << 26)) == 1; } }
    public static bool RDTSCPSupported  { get { Get80000001(); return (_edx01 & (1 << 27)) == 1; } }
    public static bool Intel64Supported { get { Get80000001(); return (_edx01 & (1 << 29)) == 1; } }
    

    public static bool InvariantTSCSupported { get { Get80000007(); return (_edx01 & (1 << 8)) == 1; } }


      
  }
}
