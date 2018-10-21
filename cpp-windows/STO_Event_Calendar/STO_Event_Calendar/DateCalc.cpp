#include <iostream>
#include <string>
#include <chrono>
#include <iomanip>
#include <fstream>
#include <experimental/filesystem>
#include <map>
#include "pch.h"

#include "nlohmann/json.hpp"
#include "argagg/argagg.hpp"
#include "date/date.h"

using namespace std;
using namespace std::chrono;
using namespace nlohmann;

typedef duration<int,std::ratio<60*60*24>> days_type;

extern struct Date;

string GetKeyboardInput(string message, bool is_required = false, bool newline = false);

class DateCalc {
    argagg::parser_results args;
    json jsondata;
    json *jsondata_p = &jsondata;
    system_clock::time_point now = system_clock::now();
    system_clock::time_point *now_p = &now;
    Date *dates;
	map<string, unsigned int> AllTokens;
	map<string, unsigned int> *AllTokens_p;
    public:
    void print_jsondata(), write_jsondata();
    system_clock::time_point final_day(), date_needed();
	system_clock::time_point end, days;
	system_clock::duration end_diff, days_needed;
	DateCalc(argagg::parser_results *arg, Date *date);
};

void DateCalc::print_jsondata()
{
	if (args["print_json"]) {
		cout << jsondata.dump(4) << endl;
	}
}

void DateCalc::write_jsondata()
{
	#if _MSC_VER <= 1912
		namespace fs = std::experimental::filesystem;
	#else
		namespace fs = std::filesystem;
	#endif
	fs::path outpath;
	ofstream outfile;
	if (args["json"] && args["json"].as<string>.empty()) {
		outpath = "sto_event_calendar.json";
	}
	else if (args["json"] && !args["json"].as<string>.empty()) {
		outpath = args["json"].as<string>;
	}
	outfile.open(outpath.string());
	if ( outfile.is_open() ) {
		outfile << jsondata.dump(4) << endl;
		outfile.close();
	}
}

system_clock::time_point DateCalc::final_day()
{
	return system_clock::time_point();
}

system_clock::time_point DateCalc::date_needed()
{
	return system_clock::time_point();
}

DateCalc::DateCalc(argagg::parser_results *arg, Date *date)
{
	argagg::parser_results *args = arg;
	Date *dates = date;
	end_diff = end - now;
	AllTokens_p = &AllTokens;
}