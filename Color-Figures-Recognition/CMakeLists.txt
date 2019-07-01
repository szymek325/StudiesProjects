cmake_minimum_required(VERSION 3.9)
project(CvOtherIdea)

set(CMAKE_CXX_STANDARD 17)

find_package( OpenCV REQUIRED )

add_executable(CvOtherIdea main.cpp constants.h)

target_link_libraries( CvOtherIdea ${OpenCV_LIBS} )