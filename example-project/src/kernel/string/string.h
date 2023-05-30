#ifndef H_STRING
#define H_STRING

#include <stddef.h>

/* Useful for debugging */
#define GET_VAR_NAME(buffer, var) buffer = #var

size_t strlen(const char* str) {
    size_t count = 0;

    while(str[count] != '\0')
        count++;
    
    return count;
}

#endif