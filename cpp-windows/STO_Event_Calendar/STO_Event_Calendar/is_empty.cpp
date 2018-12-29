#include <fstream>

namespace stack_overload {
    bool is_file_empty(std::ifstream& pFile) {
        return pFile.peek() == std::ifstream::traits_type::eof();
    }
}