#pylint: disable=invalid-name, missing-docstring
from __future__ import print_function

import argparse

from datetime import date, timedelta, datetime

def gatherinfo(string):
    try:
        return int(raw_input(string))
    except NameError:
        return int(input(string))

parser = argparse.ArgumentParser(description="Calculator for Star Trek Online in-game events.")
parser.add_argument('--daily-tokens', '-d', type=int, dest='daily')
parser.add_argument('--total-tokens', '-tt', type=int, dest='total')
parser.add_argument('--tokens-claimed', '-tc', type=int, dest='claimed')
parser.add_argument('--end-date', '-e', dest='end')
args = parser.parse_args()

# Gathering info about the current event

if args.daily:
    daily = args.daily
else:
    daily = gatherinfo("Max tokens per day: ")

if args.total:
    needed = args.total
else:
    needed = gatherinfo("Total tokens needed: ")

if args.claimed:
    tokens = args.claimed
else:
    tokens = gatherinfo("Tokens already claimed: ")

if args.end:
    end_date = args.end
else:
    end_date = gatherinfo("End date (MM/DD/YYY): ")

'''try:
    daily = int(raw_input("Max tokens per day: "))
except NameError:
    try:
        daily = int(input("Max Tokens per day: "))
    except KeyboardInterrupt:
        exit(1)

try:
    needed = int(raw_input("Total tokens needed: "))
except NameError:
    try:
        needed = int(input("Total tokens needed: "))
    except KeyboardInterrupt:
        exit(1)

try:
    tokens = int(raw_input("Tokens already claimed: "))
except NameError:
    try:
        tokens = int(input("Tokens already claimed: "))
    except KeyboardInterrupt:
        exit(1)

try:
    end_date = raw_input("End date (MM/DD/YYYY): ")
except NameError:
    try:
        end_date = input("End Date (MM/DD/YYYY): ")
    except KeyboardInterrupt:
        exit(1)'''


# Calculating time left
td = date.today()
# month, day, year = end_date.split("/")
# end = date(int(year), int(month), int(day))
end = datetime.strptime(end_date, "%m/%d/%Y").date()
remaining = end - td
try:
    days_needed = int((needed - tokens) / daily)
    if days_needed < 0:
        raise ValueError
except ValueError:
    print("ValueError: Days Needed is a negative number.")
    exit(1)
date_needed = td + timedelta(days_needed)
final_day = end - timedelta(days_needed)


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
    print('Days needed to complete event:', days_needed)
    print('You must claim the daily tokens every day from here on out.')
else:
    print('Days needed to complete event:', days_needed)
    print('Final day to start:', final_day.strftime("%B %d"))
print("Estimated date of completion (if you get your tokens every day):", date_needed.strftime("%B %d"))
