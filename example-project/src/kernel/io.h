#ifndef H_IO
#define H_IO

#include <stdint.h>

static inline void outb(uint16_t port, uint8_t value) {
    __asm__ __volatile__ ("outb %0, %1" : : "a"(value), "d"(port));
}

static uint8_t inb(uint16_t port) {
    uint8_t value;
    __asm__ __volatile__ ("inb %1, %0" : "=a"(value) : "d"(port));
    return value;
}

#endif