#pylint: disable=invalid-name, missing-docstring, line-too-long
from __future__ import division, print_function
import sys

# try:
#     from __future__ import assertions  # putting this here in case i need it.
# except ImportError:
#     pass

import argparse

from datetime import datetime, timedelta
from math import ceil
if sys.version_info[0] >= 3:
    raw_input = input


def gatherinfo(string):
    '''This function prompts the user for input with the prompt being specified by the "string" argument.
       This function does not do any data verification, so any wrong data input will probably trigger a TypeError or ValueError later on.'''
    try:
        return raw_input(string)
    except NameError:
        return input(string)


parser = argparse.ArgumentParser(
    description="Calculator for Star Trek Online in-game events.")
parser.add_argument('--daily-tokens', '-d', type=int, dest='daily',
                    help='The amount of tokens you can get per day.')
parser.add_argument('--total-tokens', '-t', type=int, dest='total',
                    help='The total amount of tokens you need to complete the project.')
parser.add_argument('--tokens-claimed', '-c', type=int, dest='claimed',
                    help="The amount of tokens you've already claimed.")
parser.add_argument('--end-date', '-e', dest='end',
                    help="The date that the event ends.")
parser.add_argument('--daily-reset', '-r', dest='reset', type=float, default=20.0,
                    help="Time that the daily reset has left (in case you've already turned it in today), in hours.")
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

if args.reset:
    reset = args.reset
else:
    reset = float(gatherinfo("Hours until dailies reset: "))


# Calculating time left
td = datetime.today()
tod = td + timedelta(hours=reset)
end = datetime.strptime(end_date, "%m/%d/%Y")  # .date()
remaining = end - td
try:
    days_needed = timedelta(int(ceil((needed - tokens) / daily)))
    if days_needed.days < 0:
        raise ValueError("\nError: Days Needed has yielded a negative number.")
except ValueError as e:
    print(e)
    exit(1)
date_needed = td.date() + days_needed
final_day = end - days_needed


# The Output
# The Final Day represents the point of
# no return.  From that day forward, you must
# get tokens every day in order to complete
# the event and recieve the reward.

print('\nToday\'s date:', td.strftime("%B %d %Y"))
print('Days remaining in event:', remaining.days)


def print_dates():
    print("Estimated date of completion (if you get your tokens every day):",
          date_needed.strftime("%B %d"))
    print("Daily quest will be available at aproximately:",
          tod.strftime("%B %d %Y at %I:%M %p"))
    print('Days needed to complete event:', days_needed.days)


if final_day < td:
    print('The wormhole\'s closed, captain.')
    print('There\'s no way to get the reward for this event.')
elif final_day == td:
    print('You must claim the daily tokens every day from here on out.')
    print_dates()
else:
    print('Final day to start:', final_day.strftime("%B %d"))
    print_dates()
