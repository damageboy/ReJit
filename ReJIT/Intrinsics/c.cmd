@echo off
yasm-1.2.0-win64.exe intrinsincs.asm -o intrinsincs.o -m amd64 -f bin --mapfile=intrinsincs.map
