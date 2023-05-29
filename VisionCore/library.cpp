#include "library.h"

#if __cplusplus
extern "C" {
#endif

    __declspec(dllexport) int test_lib() {
        return 1;
    }

#if __cplusplus
}
#endif