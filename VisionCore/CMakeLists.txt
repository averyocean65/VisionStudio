cmake_minimum_required(VERSION 3.25)
project(VisionCore VERSION 0.1.0)

set(CMAKE_CXX_STANDARD 20)

# Find Java and set JNI include directories
find_package(Java REQUIRED)
include_directories(${Java_INCLUDE_DIRS})

# Find JNI library and set JNI linker flags
find_library(JNI_LIBRARY jvm PATHS ${Java_LIBRARIES})

add_library(VisionCore SHARED library.cpp)

if(WIN32)
    target_link_libraries(VisionCore ${JNI_LIBRARY})

    add_custom_command(TARGET VisionCore POST_BUILD
            COMMAND ${CMAKE_COMMAND} -E copy $<TARGET_FILE:VisionCore> ../../libs/libVisionCore.dll
            )
endif()
