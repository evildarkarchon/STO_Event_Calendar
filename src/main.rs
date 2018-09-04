extern crate chrono;
#[allow(unused_imports)]#[macro_use] extern crate clap;
extern crate time;
#[allow(unused_imports)]#[macro_use] extern crate serde_derive;
extern crate serde_json;
#[allow(unused_imports)]#[macro_use] extern crate text_io;


mod times;
mod json;
mod input;

#[allow(unused_imports)]use chrono::prelude::*;
#[allow(unused_imports)]use clap::{Arg, App};
#[allow(unused_imports)]use time::Duration;
#[allow(unused_imports)]use serde_json::{Error, Value};

fn main() {
    #[allow(dead_code)]const DATE_STR: &'static str = "%B %d %Y";
    //const MIN_DATE_STR: &'static str = "%B %d";
    #[allow(dead_code)]const RESET_STR: &'static str = "%B %d %Y at %I:%M %p";
    const DATE_FORMAT: &'static str = "%m/%d/%Y";
    
    let args = clap_app!(STO_Event_Calendar =>
        (version: "1.0")
        (author: "Andrew Nelson <andrew@andrewnelson.org>")
        (about: "Calculator for Star Trek Online in-game events.\n
        Based on a python script made by /u/AuguryDefiant on reddit")
        (@arg daily_tokens: -d --("daily-tokens") -required +takes_value "The amount of tokens you can receive from daily quests.")
        (@arg total_tokens: -t --("total-tokens") -required +takes_value "The amount of tokens you need to complete the event.")
        (@arg tokens_claimed: -c --("tokens-claimed") -required +takes_value "The amount of tokens you have claimed.")
        (@arg end_date: -e --("end-date") -required +takes_value "Date that the event ends, in MM/DD/YYYY format (yes, I'm an American).")
        (@arg reset: -rh --("reset-hours") -required +takes_value "The amount of hours (can use decimals for partial hours) until the daily quests reset.")
        (@arg write_json: -j --("write-json") -required +takes_value "Write the raw data to a file in JSON format.")
        (@arg print_json: -p --("print-json") "Print the raw data to the console.")
    ).get_matches();

    let _tokens_claimed: u32 = 1;
    let _total_tokens: u32 = 1;
    let _daily_tokens: u32 = 1;
    let reset: f32 = 20.0;
    let end_date: Date<Local> = Local::now().date() + Duration::days(20);

    if args.is_present("daily_tokens") {
        let _daily_tokens = input::input_number_u32(&args, "daily_tokens");
    }
    else {
        let _daily_tokens: u32 = input::ask("Input the number of tokens can you receive on a daily basis: ").parse().ok().unwrap();
    }

    if args.is_present("total_tokens") {
        let _total_tokens = input::input_number_u32(&args, "total_tokens");
    }
    else {
        let _total_tokens: u32 = input::ask("Input the number of tokens needed to complete the project: ").parse().ok().unwrap();
    }

    if args.is_present("tokens_claimed") {
        let _tokens_claimed: u32 = input::input_number_u32(&args, "tokens_claimed");
    }
    else {
        let _tokens_claimed: u32 = input::ask("Input the number of tokens you have claimed so far: ").parse().ok().unwrap();
    }

    if args.is_present("reset") {
        let _reset: f32 = input::input_number_f32(&args, "reset");
    }
    else {
        let _reset: f32 = input::ask("Input the number of hours until the dailies reset: ").parse().ok().unwrap();
    }

    if args.is_present("end_date") {
        let _end_date = NaiveDateTime::parse_from_str(args.value_of("end_date").unwrap(), DATE_FORMAT);
        let offset = TimeZone::offset_from_local_datetime(&Local, &_end_date.unwrap());
        let end_date = TimeZone::datetime_from_str(&offset.unwrap(), &args.value_of("end_date").unwrap(), &DATE_FORMAT);
        assert!(end_date.is_ok(), "Couldn't parse the end date from user input.");
        let _end_date = end_date.unwrap().date();
    }
    else {
        print!("Input the date that the event ends.");
        let _end_date: String = read!("{}\n");
        let _end_date = NaiveDateTime::parse_from_str(&_end_date, DATE_FORMAT);
        let offset = TimeZone::offset_from_local_datetime(&Local, &_end_date.unwrap());
        let _end_date = TimeZone::datetime_from_str(&offset.unwrap(), &_end_date.unwrap().to_string().as_str(), &DATE_FORMAT);
        assert!(_end_date.is_ok(), "Couldn't parse the end date from user input.");
        let _end_date = _end_date.unwrap().date();
    }

    let times = times::build_times(&end_date, &reset, &_daily_tokens, &_total_tokens, &_tokens_claimed);
    let reset_time = times.reset_time();
    let remaining = times.remaining();
    let final_day = times.final_day();
    let _jsondata = json::build_jsondata(&times, &remaining, &reset_time, &final_day);
    
    if args.is_present("write_json") || args.is_present("print_json") {
        #[allow(unused_must_use)]
        json::json_out(&args, &_jsondata);
    }

    assert!(times.max_date.num_days() as i32 > 0, "times.days_needed returned a negative number or is 0. times.days_needed value is: {}", times.max_date.num_days() as i32);
    assert!(remaining.num_days() > 0, "remaining returned a negative number or is 0. remaining value is: {}", remaining.num_days());
    assert!(*times.reset_hours as f32 > 0.0, "Reset time must be a positive number.");

    println!("Today's Date is {}", times.current_dt.date().format(DATE_STR));
    println!("Days remaining in event is: {}", remaining.num_days());
    println!("Daily quests reset at approximately: {}", reset_time.format(RESET_STR));

    if final_day == times.current_dt.date() {
        println!("You must start the event today to acquire the required number of tokens.");
    } else {
        println!("Final day to start the event is: {}", final_day.format(DATE_STR));
    }
}
