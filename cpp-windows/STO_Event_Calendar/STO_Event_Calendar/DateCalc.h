#pragma once
#ifndef DATECALC_H
#define DATECALC_H
#include <iostream>
#include <string>
#include <chrono>
#include <iomanip>
#include <fstream>
#include <map>
#include <memory>

#if _MSC_VER < 2000
	#include <experimental/filesystem>
#else
	#include <filesystem>
#endif

#include "pch.h"

#include "nlohmann/json.hpp"
#include "argagg/argagg.hpp"
#include "date/date.h"

using namespace std;

class DateCalc {
    shared_ptr<argagg::parser_results> args;
    json jsondata;
    system_clock::time_point now = system_clock::now();
    Date *dates;
	map<string, unsigned int> AllTokens;
    public:
    void print_jsondata(), write_jsondata();
    system_clock::time_point final_day(), date_needed();
	system_clock::time_point end, days;
	system_clock::duration end_diff, days_needed;
	DateCalc(argagg::parser_results *arg, Date *date);
    unique_ptr<map<string, unsigned int>> AllTokens_p;
    system_clock::time_point *now_p = &now;
    unique_ptr<json> jsondata_p;
};
#endif

typedef duration<int,std::ratio<60*60*24>> days_type;
