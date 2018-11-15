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

#include "DateCalc.h"
#include "userinput.h"
#include "Date.h"

using namespace std;
using namespace std::chrono;
using namespace nlohmann;

void DateCalc::print_jsondata()
{
	if (args["print_json"]) {
		cout << jsondata.dump(4) << endl;
	}
}

void DateCalc::write_jsondata(auto outpath)
{
	#if _MSC_VER < 2000
		namespace fs = std::experimental::filesystem;
	#else
		namespace fs = std::filesystem;
	#endif

	ofstream outfile;

	if (args["json"] && args["json"].as<string>.empty()) {
		outpath = fs::path("sto_event_calendar.json");
	}
	else if (args["json"] && !args["json"].as<string>.empty()) {
		outpath = fs::path(args["json"].as<string>);
	}

	outfile.open(outpath.string(), ofstream::out | ofstream::trunc);
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

DateCalc::DateCalc(shared_ptr<argagg::parser_results> arg, Date *date)
{
	args = make_shared<argagg::parser_results>(arg);
	jsondata_p = make_unique<json>(jsondata)
	dates = date;
	end_diff = end - now;
	AllTokens_p = make_unique<map<string, unsigned int>>(AllTokens);
}
