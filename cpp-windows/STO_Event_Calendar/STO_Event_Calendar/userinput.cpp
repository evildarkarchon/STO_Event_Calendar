#include "pch.h"
#include <iostream>
#include <string>

using namespace std;

string GetKeyboardInput(string message, bool is_required = false, bool newline = false) {
	string s;

	if (is_required) {
		while ( s.empty() || s.size() <= 0 ) {
			cout << message;
			if (newline) { cout << endl; }
			getline(cin, s);
			if (!s.empty() && !s.size() <= 0) { return s; }
		}
	}
	else {
		cout << message;
		if (newline) { cout << endl; }
		getline(cin, s);
		return s;
	}
	return "";
}
string GetKeyboardInput(string *message, bool is_required = false, bool newline = false) {
	string s;

	if (is_required) {
		while ( s.empty() || s.size() <= 0 ) {
			cout << *message;
			if (newline) { cout << endl; }
			getline(cin, s);
			if (!s.empty() && !s.size() <= 0) { return s; }
		}
	}
	else {
		cout << *message;
		if (newline) { cout << endl; }
		getline(cin, s);
		return s;
	}
	return "";
}