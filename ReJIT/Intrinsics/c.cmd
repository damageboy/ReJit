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

yasm-1.2.0-win64.exe bt16.asm -o bt16.o -m amd64 -f bin --mapfile=bt16.map
yasm-1.2.0-win64.exe bt32.asm -o bt32.o -m amd64 -f bin --mapfile=bt32.map
rem yasm-1.2.0-win64.exe bt16.asm -o bt64.o -m amd64 -f bin --mapfile=bt64.map

yasm-1.2.0-win64.exe btc16.asm -o btc16.o -m amd64 -f bin --mapfile=btc16.map
yasm-1.2.0-win64.exe btc32.asm -o btc32.o -m amd64 -f bin --mapfile=btc32.map
rem yasm-1.2.0-win64.exe btc16.asm -o btc64.o -m amd64 -f bin --mapfile=btc64.map

yasm-1.2.0-win64.exe btr16.asm -o btr16.o -m amd64 -f bin --mapfile=btr16.map
yasm-1.2.0-win64.exe btr32.asm -o btr32.o -m amd64 -f bin --mapfile=btr32.map
rem yasm-1.2.0-win64.exe btr16.asm -o btr64.o -m amd64 -f bin --mapfile=btr64.map

yasm-1.2.0-win64.exe bts16.asm -o bts16.o -m amd64 -f bin --mapfile=bts16.map
yasm-1.2.0-win64.exe bts32.asm -o bts32.o -m amd64 -f bin --mapfile=bts32.map
rem yasm-1.2.0-win64.exe bts16.asm -o bts64.o -m amd64 -f bin --mapfile=bts64.map
