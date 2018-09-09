#!/usr/bin/env ruby

# frozen_string_literal: true

# rubocop:disable lint/UnneededCopEnableDirectve
# rubocop:disable Lint/MissingCopEnableDirective, Lint/UnneededCopDisableDirective, Metrics/LineLength

require 'date'
require 'optparse'
# rubocop:disable Layout/SpaceAroundOperators
require 'optparse/date'
# rubocop:enable Layout/SpaceAroundOperators
Stats = Struct.new(:needed, :claimed, :end, :daily, :reset)
stats = Stats.new

def gatherinfo(prompt)
  print(prompt)
  STDOUT.flush
  gets.chomp
end

# rubocop:disable Lint/UselessMethodArgument, Lint/UnusedMethodArgument
def print_dates(days, reset_time, rem, final)
  # rubocop:disable Style/StringLiterals
  puts "Estimated date of completion (if you get your tokens every day): #{days.strftime('%B %d')}"
  puts ""
  # rubocop:enable Style/StringLiterals
end
# rubocop:enable Lint/UselessMethodArgument

# rubocop:disable Metrics/LineLength, Style/DateTime
OptionParser.new do |parser|
  parser.on('-d', '--daily [DAILY]', Integer, 'Amount of tokens acquired per day.') { |d| stats.daily = d }
  parser.on('-n', '--needed [NEEDED]', Integer, 'Number of tokens needed to complete event.') { |n| stats.needed = n }
  parser.on('-c', '--claimed [CLAIMED]', Integer, 'Number of tokens already claimed.') { |c| stats.claimed = c }
  parser.on('-e', '--end [END]', DateTime, 'End date of the event.') { |e| stats.end = e }
  parser.on('-r', '--reset [RESET]', Float, 'Amount of hours until dailies reset.') { |r| stats.reset = r }
end
# rubocop:enable Metrics/LineLength

# rubocop:disable Metrics/LineLength, Style/StringLiterals
stats.daily = gatherinfo('Max tokens per day: ').to_i unless stats.daily.is_a?(Integer)
stats.needed = gatherinfo('Total tokens needed: ').to_i unless stats.needed.is_a?(Integer)
stats.claimed = gatherinfo('Tokens already claimed: ').to_i unless stats.claimed.is_a?(Integer)
stats.end = DateTime.strptime(gatherinfo('End Date (MM/DD/YYYY): '), "%m/%d/%Y") unless stats.end.respond_to?(:to_datetime)
stats.reset = gatherinfo('Hours until reset: ').to_f unless stats.reset.is_a?(Float)
# rubocop:enable Metrics/LineLength, Style/StringLiterals

today = DateTime.now
# rubocop:disable Lint/UselessAssignment
remaining = today - stats.end
days_needed = ((stats.needed - stats.claimed) / stats.daily).ceil
date_needed = today.to_date + days_needed.to_r if days_needed.respond_to?(:to_r)
final_day = stats.end - days_needed
puts "Max tokens per day: #{stats.daily}"
puts "Total tokens needed: #{stats.needed}"
puts "Number of tokens already claimed: #{stats.claimed}"
puts "End Date: #{stats.end}"
puts "Hours until reset: #{stats.reset}"
puts "Days remaining: #{remaining}"
puts "Today's Date: #{today}"
puts "Estimated date of completion: #{date_needed.to_time.strftime('%B %d')}"
puts "Number of days needed: #{days_needed}"
puts "Final day to be able to start the event: #{final_day.to_time.strftime('%B %d')}"
