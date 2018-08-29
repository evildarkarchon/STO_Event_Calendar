extern crate chrono;
#[allow(unused_imports)]#[macro_use] extern crate clap;
extern crate time;
#[allow(unused_imports)]#[macro_use] extern crate serde_derive;
extern crate serde_json;
#[allow(unused_imports)]#[macro_use] extern crate text_io;

mod times;
mod json;

#[allow(unused_imports)]use chrono::prelude::*;
#[allow(unused_imports)]use clap::{Arg, App};
#[allow(unused_imports)]use time::Duration;

fn main() {
    const DATE_STR: &'static str = "%B %d %Y";
    //const MIN_DATE_STR: &'static str = "%B %d";
    const RESET_STR: &'static str = "%B %d %Y at %I:%M %p";
    const DATE_FORMAT: &'static str = "%m/%d/%Y";
    /*
    let args = App::new("STO Event Calendar")
                        .version("0.1.0")
                        .author("Andrew Nelson <evildarkarchon@gmail.com>")
                        .about("Calculates if you have a chance to complete a Star Trek Online event and how long it will take.")
                        .arg(Arg::with_name("daily")
                            .short("d")
                            .long("daily-tokens")
                            .value_name("DAILY")
                            .takes_value(true)
                            .required(true)
                            .help("Number of tokens awarded per day."))
                        .arg(Arg::with_name("total")
                            .short("t")
                            .long("total-tokens")
                            .value_name("TOTAL")
                            .takes_value(true)
                            .required(true)
                            .help("Total number of tokens require to complete the event."))
                        .arg(Arg::with_name("claimed")
                            .short("c")
                            .long("tokens-claimed")
                            .value_name("TOKENS")
                            .takes_value(true)
                            .required(true)
                            .help("Number of tokens claimed so far."))
                        .arg(Arg::with_name("end")
                            .short("e")
                            .long("end-date")
                            .value_name("END")
                            .takes_value(true)
                            .required(true)
                            .help("Date that the event ends (in MM/DD/YYYY format)."))
                        .arg(Arg::with_name("reset")
                            .short("r")
                            .long("reset")
                            .takes_value(true)
                            .required(true)
                            .value_name("RESET")
                            .help("Number of hours until the daily quest(s) reset."))
                        .get_matches();
    */
    let args = clap_app!(STO_Event_Calendar =>
        (version: "1.0")
        (author: "Andrew Nelson <andrew@andrewnelson.org>")
        (about: "Calculator for Star Trek Online in-game events.\n
        Based on a python script made by /u/AuguryDefiant on reddit")
        (@arg DAILY_TOKENS: -d --("daily-tokens") +takes_value "The amount of tokens you can receive from daily quests.")
        (@arg TOTAL_TOKENS: -t --("total-tokens") +takes_value "The amount of tokens you need to complete the event.")
        (@arg TOKENS_CLAIMED: -c --("tokens-claimed") +takes_value "The amount of tokens you have claimed.")
        (@arg END: -e --("end-date") +takes_value "Date that the event ends, in MM/DD/YYYY format (yes, I'm an American).")
        (@arg RESET: -r --reset +takes_value "The amount of hours (can use decimals for partial hours) until the daily quests reset.")
    );
    /*
    let claimed = parse_number(&args, "claimed");
    let total = parse_number(&args, "total");
    let daily = parse_number(&args, "daily");
    */

    /*let times = Times {
        end: NaiveDate::parse_from_str(args.value_of("end").unwrap(), DATE_FORMAT)
                                                                                .ok()
                                                                                .unwrap(),
        reset_hours: parse_number(&args, "reset"),
        current_dt: Utc::now().naive_local(),
        days_needed: Duration::days((total - claimed) as i64 / daily as i64)
    };*/
    /*
    let reset = times.reset();

    let remaining = times.remaining();

    let final_day = times.final_day();

    assert!(times.days_needed.num_days() as i64 > 0, "times.days_needed returned a negative number or is 0. times.days_needed value is: {}", times.days_needed.num_days() as i64);
    assert!(remaining.num_days() > 0, "remaining returned a negative number or is 0. remaining value is: {}", remaining.num_days());
    assert!(times.reset_hours as i64 > 0, "Reset time must be a positive number.");

    println!("Today's Date is {}", times.current_dt.date().format(DATE_STR));
    println!("Days remaining in event is: {}", remaining.num_days());
    println!("Daily quests reset at approximately: {}", reset.format(RESET_STR));

    if final_day == times.current_dt.date() {
        println!("You must start the event today to acquire the required number of tokens.");
    } else {
        println!("Final day to start the event is: {}", final_day.format(DATE_STR));
    }
    */
}
