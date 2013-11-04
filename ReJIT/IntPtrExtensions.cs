using System;

namespace ReJit
{

  public static class IntPtrExtensions
  {
    public static unsafe byte* ToBytePtr(this IntPtr ptr)
    {
      return (byte*) ptr.ToPointer();
    }

  }
}