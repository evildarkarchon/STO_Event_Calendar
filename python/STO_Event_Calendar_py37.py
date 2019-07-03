#!/usr/bin/env python3

# This is a version of the STO_Event_Calendar script that is only compatible with >= Python 3.7  # noqa E501
from __future__ import annotations

import argparse
import json
import dateutil

from datetime import datetime, timedelta
from math import ceil
from dataclasses import dataclass

parser = argparse.ArgumentParser(
    description="Calculator for Star Trek Online in-game events.")
parser.add_argument('--daily-tokens', '-d', type=int, dest='daily',
                    help='The amount of tokens you can get per day.')
parser.add_argument('--total-tokens', '-t', type=int, dest='total',
                    help='The total amount of tokens you need to complete the project.')  # noqa E501
parser.add_argument('--tokens-claimed', '-c', type=int, dest='claimed',
                    help="The amount of tokens you've already claimed.")
parser.add_argument('--end-date', '-e', dest='end',
                    help="The date that the event ends.")
parser.add_argument('--daily-reset', '-r', dest='reset', type=float, default=20.0,  # noqa E501
                    help="Time that the daily reset has left (in case you've already turned it in today), in hours.")  # noqa E501
parser.add_argument('--json', '-j', dest='json', type=str,
                    default='STO_Event_Calendar.json', help='Dump data to specified json file.')  # noqa E501
parser.add_argument('--print-json', '-p', action='store_true',
                    dest="print_json", help='Print the json data to the terminal.')  # noqa E501
parser.add_argument('--debug', '-d', dest='debug', action='store_true', help='Turn on assert statements')  # noqa E501

args: argparse.Namespace = parser.parse_args()


@dataclass
class STO_Event_Calendar:
    daily: int
    total: int
    claimed: int
    event_end: str
    reset: float
    today: datetime = datetime.today()
    reset_time: datetime
    end_date: datetime
    remaining: datetime
    days_needed: timedelta
    final_day: timedelta

    # just doing this one because I can AND its (theoretically) simple.
    def asjson(self) -> str:
        return json.dumps(self.asdict())

    def write_json(self):
        jsonout: str = self.asjson()
        assert type(jsonout) is str
        out = open(args.json, 'w')
        out.write(jsonout)
        out.close()

    def print_json(self):
        print(self.asjson())


sto = STO_Event_Calendar()


sto.daily = args.daily if args.daily else int(input("Max tokens per day: "))
if args.debug:
    assert sto.daily >= 1, "Number of daily tokens must be greater than or equal to 1"  # noqa E501

sto.total = args.total if args.total else int(input("Total tokens needed: "))
if args.debug:
    assert sto.total > 1, "Number of total tokens needed must be greater than 1"  # noqa E501

sto.claimed = args.claimed if args.claimed else int(
    input("Tokens already claimed: "))
if args.debug:
    assert sto.claimed >= 0, "Number of tokens claimed must be greater than or equal to 0"  # noqa E501

sto.reset = args.reset if args.reset else float(
    input("Hours until dailies reset: "))
if args.debug:
    assert args.reset > 0.0, "Number of hours until reset must be greater than 0"  # noqa E501

sto.reset_time = sto.today + timedelta(hours=sto.reset)

sto.end_date = args.end if args.end else input(
    "Enter the date that the event ends: ")
sto.end = dateutil.parser.parse(sto.end_date)

# sto.end = datetime.strptime(sto.end_date, "%m/%d/%Y")

days: int = (sto.total - sto.claimed) / sto.daily
sto.days_needed = timedelta(ceil(days))
if args.debug:
    assert type(days) is int
    assert type(sto.days_needed) is int
    assert sto.days_needed > 0, "Days needed must be greater than zero"
sto.final_day = sto.end - sto.days_needed

if args.print_json:
    sto.print_json()

if args.json and type(args.json) is str:
    sto.write_json()


def write_days():
    if sto.final_day.days >= 1:
        if sto.final_day.days > 1:
            print("There are {} days until the event ends.".format(
                sto.final_day.days))
        elif sto.final_day.days == 1:
            print("There is only 1 day left until the event ends.")
    elif sto.final_day.days == 0 and sto.final_day.seconds >= 3600:
        if sto.final_day.seconds > 3600:
            h, r = divmod(sto.final_day.seconds, 3600)
            print("There is {} hours left until the event ends.".format(h))
        elif sto.final_day.seconds == 3600:
            print("There is only 1 hour until the event ends.")
    elif sto.final_day.days == 0 and sto.final_day.seconds < 3600:
        m, r = divmod(sto.final_day.seconds, 60)
        if sto.final_day.seconds > 60:
            print("There are {} minutes left until the event ends.".format(m))
            if sto.final_day.seconds == 60:
                print("There is only 1 minute left until the event ends.")
    elif sto.final_day.days == 0 and sto.final_day.seconds >= 1 and sto.final_day.seconds < 60:  # noqa E501
        print("The event is will be over in a matter of seconds.")
    else:
        print("The event is over.")
