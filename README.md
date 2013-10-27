ReJit
=======

# What #
HackJit aims to add useful x64 (only for now, no x86 since I'm lazy) compiler intrinsincs to the .NET/CLR world through the use of ungodly hacks, but in a safe manner.

Ultimately, by hooking the .NET JIT Engine, ReJit adds a bunch of compiler intrinsincs that get embedded in the calling code.

Since code is better than words:
What ReJit does is turn the calling code that looks like this in c#:
```c#
uint eax = 0, ebx, ecx, edx;
Instrinsincs.CPUID(ref eax, out ebc, out ecx, out edx)
```

```asm
      uint eax, ebx, ecx, edx;
      eax = 0x00;
00007FFBECFE12BF C7 45 7C 00 00 00 00 mov         dword ptr [rbp+7Ch],0  
      ebx = ecx = edx = 0x00;
00007FFBECFE12C6 C7 45 70 00 00 00 00 mov         dword ptr [rbp+70h],0  
00007FFBECFE12CD C7 45 74 00 00 00 00 mov         dword ptr [rbp+74h],0  
00007FFBECFE12D4 C7 45 78 00 00 00 00 mov         dword ptr [rbp+78h],0  

      Intrinsincs.CPUID(ref eax, out ebx, out ecx, out edx);
00007FFBECFE12DB 4C 8D 4D 70          lea         r9,[rbp+70h]  
00007FFBECFE12DF 4C 8D 45 74          lea         r8,[rbp+74h]  
00007FFBECFE12E3 48 8D 55 78          lea         rdx,[rbp+78h]  
00007FFBECFE12E7 48 8D 4D 7C          lea         rcx,[rbp+7Ch]  
00007FFBECFE12EB 49 89 CA             mov         r10,rcx  
00007FFBECFE12EE 49 89 D3             mov         r11,rdx  
00007FFBECFE12F1 8B 01                mov         eax,dword ptr [rcx]  
00007FFBECFE12F3 53                   push        rbx  
00007FFBECFE12F4 0F A2                cpuid  
00007FFBECFE12F6 41 89 08             mov         dword ptr [r8],ecx  
00007FFBECFE12F9 41 89 11             mov         dword ptr [r9],edx  
00007FFBECFE12FC 41 89 02             mov         dword ptr [r10],eax  
00007FFBECFE12FF 41 89 1B             mov         dword ptr [r11],ebx  
00007FFBECFE1302 5B                   pop         rbx  
```

# Using The Instrinsincs #

1. Call `Intrinsincs.Init()` early on in your code
2. Call `Intrinsics.XXX()` somewhere else
3. Profit!

# What Really Happens #

The Intrinsincs are exposed through a class called `Instrinsincs` where each intrinsics is implemented as a "method".

The reason I refer to these implementations as methods with quotation marks is that they are not normal methods in the common sense.

The .NET JIT Engine is hooked in such a way, that when these methods are called, something entirely else will get emitted as machine code... That would be the compiler intrinsinc

So, for example, when a call is made to:
```c#
int bla = SomeClculationIdLikeToDoBitPopulationCountOn();
Instinsincs.POPCNT(bla)
```
The hooked JIT engine will replace the caller (this is very important for performance reasons) with the x64/x86 assmebly operation:
```asm
MOV RCX, [rbp+80h]
POPCNT RCX`
```

# History #

It all started when I read about a hack shown by [@JeroenFrijters](https://twitter.com/JeroenFrijters) of [IKVM.NET](http://www.ikvm.net/) fame: [How to Hack Your Own JIT Intrinsic](http://weblog.ikvm.net/PermaLink.aspx?guid=7dc34fd3-215d-467d-a331-e1de9023e0d1]), but it quickly eveloved from there into something completely different on the implementation side.

# Supported Initrinsics #
The supported intrinsics currently are:
- BSWAP32 (signed/unsigned)
- BSWAP64 (signed/unsigned) Note: Not currently working
- BSR32 (signed/unsigned)
- BSF32 (signed/unsigned)
- BSR64 (signed/unsigned)
- BSF64 (signed/unsigned)
- RDTSC (with/without memory barrier)
- RDTSCP
- CPUID
- BTS
- BTR
- BT
- BTC

# Todo #

- AES*
- MFENCE
- SFENCE
- WBINVD
- CLFLUSH
- INVD
- RDRAND
- PREFETCH0/1/2/NTA
- POPCNT
- PAUSE
- CMPXCHNG8B/CMPXCHNG16B

# Resources (Stuff I learned from) #
[.NET Internals and Code Injection](http://www.ntcore.com/Files/netint_injection.htm) - By Daniel Pistelli
[Can't hook ICorJitCompiler:compileMethod from Managed Code](https://easyhook.codeplex.com/discussions/399968) - Useful CodePlex post by Manfred Frito
[.NET CLR Injection: Modify IL Code during Run-time](http://www.codeproject.com/Articles/463508/NET-CLR-Injection-Modify-IL-Code-during-Run-time) - Impressive project by Jerry Wang

