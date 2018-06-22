#!/usr/bin/env ruby
# frozen_string_literal: true

require date
require optparse
# rubocop:disable Layout/SpaceAroundOperators
require optparse/date
# rubocop:enable Layout/SpaceAroundOperators
Stats = Struct.new(:needed, :claimed, :end, :daily, :reset)
stats = Stats.new

def gatherinfo(prompt)
  print(prompt)
  STDOUT.flush
  gets.chomp
end

# rubocop:disable Metrics/LineLength
OptionParser.new do |parser|
  parser.on('-d', '--daily [DAILY]', Integer, 'Amount of tokens acquired per day.') { |d| stats.daily = d }
  parser.on('-n', '--needed [NEEDED]', Integer, 'Number of tokens needed to complete event.') { |n| stats.needed = n }
  parser.on('-c', '--claimed [CLAIMED]', Integer, 'Number of tokens already claimed.') { |c| stats.claimed = c }
  parser.on('-e', '--end [END]', Date, 'End date of the event.') { |e| stats.end = e }
  parser.on('-r', '--reset [RESET]', Float, 'Amount of hours until dailies reset.') { |r| stats.reset = r }
end
# rubocop:enable Metrics/LineLength

today = date.now

stats.daily = gatherinfo('Max tokens per day: ').to_i unless stats.daily
stats.needed = gatherinfo('Total tokens needed: ').to_i unless stats.needed
stats.claimed = gatherinfo('Tokens already claimed: ').to_i unless stats.claimed
stats.end = Date.parse(gatherinfo('End Date: ')) unless stats.end
stats.reset = gatherinfo('Hours until reset: ').to_f unless stats.reset
