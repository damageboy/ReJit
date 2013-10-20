using System;
using NUnit.Framework;

namespace ReJit.Tests
{    
  public class ReJitTests
  {
    [SetUp]
    public void Setup()
    {
      Intrinsincs.Init();
    }

    [Test]
    public void PopulationCount()
    {
      int count;
      
      Console.WriteLine("Testing POPCNT");
      
      const ushort test16U = (ushort)0x0F0FU;      
      count = Intrinsincs.POPCNT(test16U);
      Assert.That(count, Is.EqualTo(8));
            
      const uint test32U = 0x0F0F0F0FU;
      count = Intrinsincs.POPCNT(test32U);
      Assert.That(count, Is.EqualTo(16));
      
      const ulong test64U = 0x0F0F0F0F0F0F0F0FLU;

      count = Intrinsincs.POPCNT(test64U);
      Assert.That(count, Is.EqualTo(32));     
    }

    [Test]
    public void BitScanForwardSimple()
    {
      int location;
      Console.WriteLine("Testing BSF");

      var test16U = (ushort)0x0001U;      
      for (var i = 0; i < 16; i++) {
        location = Intrinsincs.BSF(test16U);
        Assert.That(location, Is.EqualTo(i));
        test16U <<=1;
      }

      var test32U = 0x00000001U;      
      for (var i = 0; i < 32; i++)
      {
        location = Intrinsincs.BSF(test32U);
        Assert.That(location, Is.EqualTo(i));
        test32U <<= 1;
      }

      var test64U = 0x0000000000000001U;
      for (var i = 0; i < 32; i++)
      {
        location = Intrinsincs.BSF(test64U);
        Assert.That(location, Is.EqualTo(i));
        test64U <<= 1;
      }
    }

    [Test]
    public void BitScanReverseSimple()
    {
      int location;
      Console.WriteLine("Testing BSR");

      var test16U = (ushort)0x0001U;
      for (var i = 0; i < 16; i++)
      {
        location = Intrinsincs.BSR(test16U);
        Assert.That(location, Is.EqualTo(i));
        test16U <<= 1;
      }

      var test32U = 0x00000001U;
      for (var i = 0; i < 32; i++)
      {
        location = Intrinsincs.BSR(test32U);
        Assert.That(location, Is.EqualTo(i));
        test32U <<= 1;
      }

      var test64U = 0x0000000000000001U;
      for (var i = 0; i < 32; i++)
      {
        location = Intrinsincs.BSR(test64U);
        Assert.That(location, Is.EqualTo(i));
        test64U <<= 1;
      }
    }
  }
}
