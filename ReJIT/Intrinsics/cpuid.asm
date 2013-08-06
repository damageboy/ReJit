BITS 64
[MAP all]
trampoline_size EQU 41
jit:
; Push the stuff we need to preserve by ABI
push        rbx
; Store the return address in RBX
mov         rbx,qword  [rsp+08]
push        rsi
push        rdi

; Block copy all the LEA insn

lea rsi, [rbx-21]
lea rdi, [rbx-trampoline_size]
mov rcx, 16
rep movsb
lea rsi, [rel start]
mov rcx, end - start
rep movsb
mov rcx,rbx
sub rcx,rdi
mov al,0x90
rep stosb
; Pop in reverse
pop         rdi
pop         rsi
sub         rbx,trampoline_size
mov         qword [rsp+08h],rbx
pop         rbx
ret
start:
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
end:
