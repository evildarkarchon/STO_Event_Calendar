#pragma once
#ifndef IS_EMPTY_H
#define IS_EMPTY_H
#include <fstream>
#include "pch.h"
namespace stack_overload {
    bool is_file_empty(std::ifstream& pFile);
}
#endif