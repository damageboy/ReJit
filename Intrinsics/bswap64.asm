BITS 64
[MAP all]
trampoline_size EQU 12
jit:
; Push the stuff we need to preserve by ABI
push        rbx
; Store the return address in RBX
mov         rbx,qword  [rsp+08]
push        rsi
push        rdi

; Block copy all the LEA insn

lea rsi, [rbx-10]
lea rdi, [rbx-trampoline_size]
mov rcx, 5
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
bswap rcx
mov rax,rcx
end:
