@echo off
yasm-1.2.0-win64.exe rdtsc.asm -o rdtsc.o -m amd64 -f bin --mapfile=rdtsc.map
yasm-1.2.0-win64.exe rdtscp.asm -o rdtscp.o -m amd64 -f bin --mapfile=rdtscp.map

yasm-1.2.0-win64.exe cpuid.asm -o cpuid.o -m amd64 -f bin --mapfile=cpuid.map

yasm-1.2.0-win64.exe bswap32.asm -o bswap32.o -m amd64 -f bin --mapfile=bswap32.map
yasm-1.2.0-win64.exe bswap64.asm -o bswap64.o -m amd64 -f bin --mapfile=bswap64.map


yasm-1.2.0-win64.exe bsf32.asm -o bsf32.o -m amd64 -f bin --mapfile=bsf32.map
yasm-1.2.0-win64.exe bsf64.asm -o bsf64.o -m amd64 -f bin --mapfile=bsf64.map

yasm-1.2.0-win64.exe bsr32.asm -o bsr32.o -m amd64 -f bin --mapfile=bsr32.map
yasm-1.2.0-win64.exe bsr64.asm -o bsr64.o -m amd64 -f bin --mapfile=bsr64.map
