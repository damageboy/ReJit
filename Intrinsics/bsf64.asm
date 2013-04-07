BITS 64
[MAP all]
trampoline_param_size EQU 4
trampoline_size EQU 5
jit:
; Push the stuff we need to preserve by ABI
push        rbx
; Store the return address in RBX
mov         rbx,qword  [rsp+08]
push        rsi
push        rdi
mov         rdx,rcx

; Block copy all the LEA insn
lea rsi, [rel start]
lea rdi, [rbx-trampoline_size]
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
mov         rcx,rdx
ret
start:
bsf rax,rcx
end:
