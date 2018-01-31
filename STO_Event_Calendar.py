#pylint: disable=invalid-name, missing-docstring
from __future__ import print_function

import argparse

from datetime import date, timedelta, datetime

def gatherinfo(string):
    try:
        return raw_input(string)
    except NameError:
        return input(string)

parser = argparse.ArgumentParser(description="Calculator for Star Trek Online in-game events.")
parser.add_argument('--daily-tokens', '-d', type=int, dest='daily', help='The amount of tokens you can get per day.')
parser.add_argument('--total-tokens', '-t', type=int, dest='total', help='The total amount of tokens you need to complete the project.')
parser.add_argument('--tokens-claimed', '-c', type=int, dest='claimed', help="The amount of tokens you've already claimed.")
parser.add_argument('--end-date', '-e', dest='end', help="The date that the event ends.")
args = parser.parse_args()

# Gathering info about the current event

if args.daily:
    daily = args.daily
else:
    daily = int(gatherinfo("Max tokens per day: "))

if args.total:
    needed = args.total
else:
    needed = int(gatherinfo("Total tokens needed: "))

if args.claimed:
    tokens = args.claimed
else:
    tokens = int(gatherinfo("Tokens already claimed: "))

if args.end:
    end_date = args.end
else:
    end_date = gatherinfo("End date (MM/DD/YYY): ")


# Calculating time left
td = date.today()
end = datetime.strptime(end_date, "%m/%d/%Y").date()
remaining = end - td
try:
    days_needed = timedelta(int((needed - tokens) / daily))
    if days_needed.days < 0:
        raise ValueError
except ValueError:
    print("ValueError: Days Needed is a negative number.")
    exit(1)
date_needed = td + days_needed
final_day = end - days_needed


# The Output
# The Final Day represents the point of
# no return.  From that day forward, you must
# get tokens every day in order to complete
# the event and recieve the reward.

print('\nToday:', td.strftime("%B %d"))
print('Days remaining in event:', remaining.days)

if final_day < td:
    print('The wormhole\'s closed, captain.')
    print('There\'s no way to get the reward for this event.')
elif final_day == td:
    print('Days needed to complete event:', days_needed.days)
    print('You must claim the daily tokens every day from here on out.')
else:
    print('Days needed to complete event:', days_needed.days)
    print('Final day to start:', final_day.strftime("%B %d"))
print("Estimated date of completion (if you get your tokens every day):", date_needed.strftime("%B %d"))
