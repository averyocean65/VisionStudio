#include "vga/text/terminal.h"

void kernel_early_main() {
    init_terminal();
}

void kernel_main() {
    printf("Welcome to my Operating System!\n");
}

void kernel_end() {
    __asm__ ("hlt");
}