#pragma once
#ifndef USERINPUT_H
#define USERINPUT_H
#include "pch.h"
#include <string>

using namespace std;

string GetKeyboardInput(string message, bool is_required = false, bool newline = false);
string GetKeyboardInput(string *message, bool is_required = false, bool newline = false);

#endif