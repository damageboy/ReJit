using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
      
      var test_16u = (ushort)0x0F0FU;      
      count = Intrinsincs.POPCNT(test_16u);
      Assert.That(count, Is.EqualTo(8));
            
      var test_32u = 0x0F0F0F0FU;
      count = Intrinsincs.POPCNT(test_32u);
      Assert.That(count, Is.EqualTo(16));
      
      var test_64u = 0x0F0F0F0F0F0F0F0FLU;

      count = Intrinsincs.POPCNT(test_64u);
      Assert.That(count, Is.EqualTo(32));     
    }

    [Test]
    public void BitScanForwardSimple()
    {
      int location;
      Console.WriteLine("Testing BSF");

      var test_16u = (ushort)0x0001U;      
      for (var i = 0; i < 16; i++) {
        location = Intrinsincs.BSF(test_16u);
        Assert.That(location, Is.EqualTo(i));
        test_16u <<=1;
      }

      var test_32u = 0x00000001U;      
      for (var i = 0; i < 32; i++)
      {
        location = Intrinsincs.BSF(test_32u);
        Assert.That(location, Is.EqualTo(i));
        test_32u <<= 1;
      }

      var test_64u = 0x0000000000000001U;
      for (var i = 0; i < 32; i++)
      {
        location = Intrinsincs.BSF(test_64u);
        Assert.That(location, Is.EqualTo(i));
        test_64u <<= 1;
      }
    }

    [Test]
    public void BitScanReverseSimple()
    {
      int location;
      Console.WriteLine("Testing BSR");

      var test_16u = (ushort)0x0001U;
      for (var i = 0; i < 16; i++)
      {
        location = Intrinsincs.BSR(test_16u);
        Assert.That(location, Is.EqualTo(i));
        test_16u <<= 1;
      }

      var test_32u = 0x00000001U;
      for (var i = 0; i < 32; i++)
      {
        location = Intrinsincs.BSR(test_32u);
        Assert.That(location, Is.EqualTo(i));
        test_32u <<= 1;
      }

      var test_64u = 0x0000000000000001U;
      for (var i = 0; i < 32; i++)
      {
        location = Intrinsincs.BSR(test_64u);
        Assert.That(location, Is.EqualTo(i));
        test_64u <<= 1;
      }
    }
  }
}
