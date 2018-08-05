#!/usr/bin/env python3

# This is a version of the STO_Event_Calendar script that is only compatible with >= Python 3.7

from __future__ import assertions

import argparse
import json

from datetime import datetime, timedelta
from math import ceil

parser = argparse.ArgumentParser(description="Calculator for Star Trek Online in-game events.")
parser.add_argument('--daily-tokens', '-d', type=int, dest='daily', help='The amount of tokens you can get per day.')
parser.add_argument('--total-tokens', '-t', type=int, dest='total', help='The total amount of tokens you need to complete the project.')
parser.add_argument('--tokens-claimed', '-c', type=int, dest='claimed', help="The amount of tokens you've already claimed.")
parser.add_argument('--end-date', '-e', dest='end', help="The date that the event ends.")
parser.add_argument('--daily-reset', '-r', dest='reset', type=float, default=20.0, help="Time that the daily reset has left (in case you've already turned it in today), in hours.")
parser.add_argument('--json', '-j', dest='json', type=str, default='STO_Event_Calendar.json', help='Dump data to specified json file.')
args: Namespace = parser.parse_args()

@dataclass
class STO_Event_Calendar:
    daily: int = args.daily
    total: int = args.total
    claimed: int = args.claimed
    event_end: str = args.end
    reset: float = args.reset
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
