// STO_Event_Calendar.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <string>
#include <sstream>
#include <ostream>
#include <chrono>
#include <cstdlib>
#include <cmath>
#include "nlohmann/json.hpp"
#include "argagg/argagg.hpp"
#include "date/date.h"

using namespace std;
using namespace std::chrono;
using namespace nlohmann;

string GetKeyboardInput(string message, bool is_required = false, bool newline = false);
struct Date {
		string EndDate;
		float Reset;
		unsigned int Total;
		unsigned int Daily;
		unsigned int Claimed;
	};

int main(int argc, char* argv[])
{
	 Date Dates;
	
	
	argagg::parser argparser{
		{
			{ "help", { "-h", "--help"}, "show this message", 0},
			{ "daily", { "-d", "--daily-tokens" }, "Amount of tokens you acquire per day.", 1 },
			{ "total", { "-t", "--total-tokens" }, "Amount of tokens needed to complete the event.", 1 },
			{ "claimed", { "-c", "--tokens-claimed" }, "Amount of tokens you've already claimed", 1 },
			{ "reset", { "-r", "--reset" }, "Amount of time, in hours, until the daily quests reset.", 1 },
			{ "json", { "-j", "--json" }, "Save raw data as a json file", 1 },
			{ "quiet", { "-q", "--quiet" }, "Don't actually print anything to the console", 0 },
			{ "print_json", { "-p", "--print-json" }, "Print the raw data, as JSON, to the console.", 0 }
		}
	};
	argagg::parser_results args;
	try {
		args = argparser.parse(argc, argv);
	}
	catch (const exception& e) {
		cerr << e.what() << endl;
		return EXIT_FAILURE;
	}

	if (args["help"]) {
		cout << "STO_Event_Calendar" << endl;
		cout << argparser;
		return EXIT_SUCCESS;
	}
}
