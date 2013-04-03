HackJit
=======

HackJit aims to add useful x64 (only for now, no x86 since I'm lazy).
The method is based on a hack shown by [@JeroenFrijters](https://twitter.com/JeroenFrijters) of [IKVM.NET](http://www.ikvm.net/) fame: [How to Hack Your Own JIT Intrinsic](http://weblog.ikvm.net/PermaLink.aspx?guid=7dc34fd3-215d-467d-a331-e1de9023e0d1](http://weblog.ikvm.net/PermaLink.aspx?guid=7dc34fd3-215d-467d-a331-e1de9023e0d1 "How to Hack Your Own JIT Intrinsic")

The intrinsincs are exposed through a class called 
`JIT` where each intrinsics is implemented as a "method".

The reason I refer to these implementations as methods with quotation marks is that they are not normal methods in the common sense.

When the method is first executed, it patches the caller's code to use the x64 assembly instead of calling the method.
So in reality the first call to the "method" from each call site actually triggers a code path that modifies the code into using said intrinsics.

The supported intrinsics currently are:
- BSWAP32 (signed/unsigned)
- BSWAP64 (signed/unsigned)
- RDTSC (with memory barrier)
- RDTSCP
- CPUID
