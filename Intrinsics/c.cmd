@echo off
yasm-1.2.0-win64.exe rdtsc.asm -o rdtsc.o -m amd64 -f bin --mapfile=rdtsc.map
yasm-1.2.0-win64.exe rdtscp.asm -o rdtscp.o -m amd64 -f bin --mapfile=rdtscp.map

yasm-1.2.0-win64.exe cpuid.asm -o cpuid.o -m amd64 -f bin --mapfile=cpuid.map

yasm-1.2.0-win64.exe bswap32.asm -o bswap32.o -m amd64 -f bin --mapfile=bswap32.map
yasm-1.2.0-win64.exe bswap64.asm -o bswap64.o -m amd64 -f bin --mapfile=bswap64.map

yasm-1.2.0-win64.exe bsf16.asm -o bsf16.o -m amd64 -f bin --mapfile=bsf16.map
yasm-1.2.0-win64.exe bsf32.asm -o bsf32.o -m amd64 -f bin --mapfile=bsf32.map
yasm-1.2.0-win64.exe bsf64.asm -o bsf64.o -m amd64 -f bin --mapfile=bsf64.map

yasm-1.2.0-win64.exe bsr16.asm -o bsr16.o -m amd64 -f bin --mapfile=bsr16.map
yasm-1.2.0-win64.exe bsr32.asm -o bsr32.o -m amd64 -f bin --mapfile=bsr32.map
yasm-1.2.0-win64.exe bsr64.asm -o bsr64.o -m amd64 -f bin --mapfile=bsr64.map

yasm-1.2.0-win64.exe popcnt16.asm -o popcnt16.o -m amd64 -f bin --mapfile=popcnt16.map
yasm-1.2.0-win64.exe popcnt32.asm -o popcnt32.o -m amd64 -f bin --mapfile=popcnt32.map
yasm-1.2.0-win64.exe popcnt64.asm -o popcnt64.o -m amd64 -f bin --mapfile=popcnt64.map
