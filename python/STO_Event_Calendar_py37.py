#!/usr/bin/env python3

# This is a version of the STO_Event_Calendar script that is only compatible with >= Python 3.7
from __future__ import annotations

from typing import Any
import argparse
import json

from datetime import datetime, timedelta
from math import ceil
from dataclasses import dataclass, make_dataclass


Namespace = argparse.Namespace
parser = argparse.ArgumentParser(description="Calculator for Star Trek Online in-game events.")
parser.add_argument('--daily-tokens', '-d', type=int, dest='daily', help='The amount of tokens you can get per day.')
parser.add_argument('--total-tokens', '-t', type=int, dest='total', help='The total amount of tokens you need to complete the project.')
parser.add_argument('--tokens-claimed', '-c', type=int, dest='claimed', help="The amount of tokens you've already claimed.")
parser.add_argument('--end-date', '-e', dest='end', help="The date that the event ends.")
parser.add_argument('--daily-reset', '-r', dest='reset', type=float, default=20.0, help="Time that the daily reset has left (in case you've already turned it in today), in hours.")
parser.add_argument('--json', '-j', dest='json', type=str, default='STO_Event_Calendar.json', help='Dump data to specified json file.')
parser.add_argument('--print-json', '-p', action='store_true', dest="print_json", help = 'Print the json data to the terminal.')
args = parser.parse_args()

@dataclass
class STO_Event_Calendar:
    daily: int
    total: int
    claimed: int
    event_end: str
    reset: float
    today: datetime = datetime.today()
    tod: datetime
    end_date: datetime
    remaining: datetime
    days_needed: timedelta
    final_day: datetime

    # just doing this one because I can AND its (theoretically) simple.
    def asjson(self) -> str:
        return json.dumps(self.asdict())

sto = STO_Event_Calendar()

sto.daily = args.daily if args.daily else int(input("Max tokens per day: "))

sto.days_needed = args.total if args.total else int(input("Total tokens needed: "))

sto.claimed = args.claimed if args.claimed else int(input("Tokens already claimed: "))

sto.end_date = args.end if args.end else input("End date (MM/DD/YYY): ")

sto.reset = args.reset if args.reset else float(input("Hours until dailies reset: "))

sto.tod = sto.today + timedelta(hours=sto.reset)

end: datetime = datetime.strptime(sto.end_date, "%m/%d/%Y")
days: int = (sto.total - sto.claimed) / sto.daily
days_needed: timedelta = timedelta(ceil(days))
assert type(days) is int
assert type(days_needed) is int
assert days_needed > 0, "Days needed must be greater than zero"

def write_json():
    jsonout: str = sto.asjson()
    assert type(jsonout) is str
    out = open(args.json, 'w')
    out.write(jsonout)
    out.close()

if args.print_json:
    jsonout: str = sto.asjson()
    assert type(jsonout) is str
    print(jsonout)

if args.json:
    assert type(args.json) is str
    write_json()

def write_days():
    final_day: timedelta = end - days_needed
    if final_day.days >= 1:
        if final_day.days > 1:
            print("There are {} days until the event ends".format(final_day.days))
