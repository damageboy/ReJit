BITS 64
[MAP all]

bt16:
bt ax,cx
setc al

bt32:
bt eax,ecx
setc al

btc16:
btc ax,cx
setc al

btc32:
btc eax,ecx
setc al

bts16:
bts ax,cx
setc al

bts32:
bts eax,ecx
setc al

btr16:
btr ax,cx
setc al

btr32:
btr eax,ecx
setc al

bsf16:
bsf ax,cx

bsf32:
bsf eax,ecx

bsf64:
bsf rax,rcx

bsr16:
bsr ax,cx

bsr32:
bsr eax,ecx

bsr64:
bsr rax,rcx

bswap32:
bswap ecx
mov eax,ecx

bswap64:
bswap rcx
mov rax,rcx

_cpuid:
mov         r10,rcx
mov         r11,rdx
mov eax, dword [rcx]
push rbx
cpuid
mov [r8], ecx
mov [r9], edx  
mov [r10], eax
mov [r11], ebx
pop rbx

popcnt16:
popcnt ax,cx

popcnt32:
popcnt eax,ecx

popcnt64:
popcnt rax,rcx

rdtsc_fenced:
sfence
rdtsc
shl rdx,32
or rax,rdx

_rdtsc:
rdtsc
shl rdx,32
or rax,rdx

_rdtscp:
rdtscp
shl rdx,32
or rax,rdx