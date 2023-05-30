#ifndef H_VGA_TERMINAL
#define H_VGA_TERMINAL

#include <stddef.h>
#include <stdint.h>

#include "../vga.h"
#include "../../io.h"

size_t terminal_column;
size_t terminal_row;

uint8_t color;

uint16_t* terminal_buffer;

static inline void put_entryat(uint16_t entry, size_t x, size_t y) {
    const size_t index = vga_text_index(x, y);
    terminal_buffer[index] = entry;
}

/* Clears the termimal of all text */
void clear_terminal() {
    for(int y = 0; y < VGA_HEIGHT; y++) {
        for(int x = 0; x < VGA_WIDTH; x++) {
            const size_t index = vga_text_index(x, y);
            put_entryat(vga_entry(' ', color), x, y);
        }
    }
}

/* Initializes the terminal position, color and buffer */
void init_terminal() {
    enable_cursor(13, 15);

    terminal_column = 0;
    terminal_row = 0;

    color = vga_entry_color(VGA_COLOR_WHITE, VGA_COLOR_BLACK);

    terminal_buffer = (uint16_t*)VGA_ADDR;
}

/* Sets the terminal foreground and background color */
void set_terminal_color(enum vga_color fg, enum vga_color bg) {
    color = vga_entry_color(fg, bg);
}

/* Returns 1 if a whitespace character is found, if not then it returns 0 */
int whitespace_handler(char c) {
    if(c == '\n') {
        terminal_column = 0;
        if(++terminal_row == VGA_HEIGHT) {
            terminal_row = 0;
        }

        update_cursor();

        /* Whitespace Character found */
        return 1;
    }

    /* No Whitespace Character found */
    return 0;
}

/* Print a character at the current cursor position with the current terminal color */
void printcf(char c) {
    int skip = whitespace_handler(c);
    if(skip == 1)
        return;

    uint16_t entry = vga_entry(c, color);
    put_entryat(entry, terminal_column, terminal_row);

    if(++terminal_column == VGA_WIDTH) {
        terminal_column = 0;
        if(++terminal_row == VGA_HEIGHT) {
            terminal_row = 0;
        }
    }

    update_cursor();
}

/* Print a character with a custom foreground and background color */
void printcf_color(char c, enum vga_color fg, enum vga_color bg) {
    color = vga_entry_color(fg, bg);
    printcf(c);
}

/* Print a string with the current terminal color */
void printf(const char* data) {
    for(size_t i = 0; data[i] != '\0'; i++) {
        printcf(data[i]);
    }
}

/* Print a string with a custom foreground and background color */
void printf_color(const char* data, enum vga_color fg, enum vga_color bg) {
    for(size_t i = 0; data[i] != '\0'; i++) {
        printcf_color(data[i], fg, bg);
    }
}

/* Cursor Logic */
void enable_cursor(uint8_t cursor_start, uint8_t cursor_end) {
    outb(0x3D4, 0x0A);
    outb(0x3D5, (inb(0x3D5) & 0xC0) | cursor_start);

    outb(0x3D4, 0x0B);
    outb(0x3D5, (inb(0x3D5) & 0xE0) | cursor_end);
}

void disable_cursor() {
    outb(0x3D4, 0x0A);
    outb(0x3D5, 0x20);
}

void update_cursor() {
    uint16_t cursorLocation = terminal_row * VGA_WIDTH + terminal_column;
    outb(0x3D4, 0x0F);
    outb(0x3D5, (uint8_t)(cursorLocation & 0xFF));
    outb(0x3D4, 0x0E);
    outb(0x3D5, (uint8_t)((cursorLocation >> 8) & 0xFF));
}

void set_cursor(size_t x, size_t y) {
    terminal_column = x;
    terminal_row = y;
    update_cursor();
}

uint16_t get_cursor() {
    uint16_t cursorLocation = 0;
    outb(0x3D4, 0x0F);
    cursorLocation |= inb(0x3D5);
    outb(0x3D4, 0x0E);
    cursorLocation |= ((uint16_t)inb(0x3D5)) << 8;
    return cursorLocation;
}

#endif