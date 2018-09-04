extern crate serde_json;
#[allow(unused_imports)]use serde_json::*;
#[allow(unused_imports)]use times::*;
#[allow(unused_imports)]use chrono::prelude::*;
#[allow(unused_imports)]use time::Duration;
use clap::ArgMatches;
use std::fs;
use std::env;


#[derive(Serialize, Deserialize)]
pub struct JsonData {
    pub end: String,
    pub reset_hours: f32,
    pub current_dt: String,
    pub days_needed: u32,
    pub tokens_needed: u32,
    pub tokens_claimed: u32,
    pub daily_tokens: u32,
    pub remaining: u32,
    pub reset_time: String,
    pub final_day: String
}

pub fn build_jsondata(jd: &Times, remaining: &Duration, reset_time: &DateTime<Local>, final_day: &Date<Local>) -> JsonData {
    JsonData {
        end: jd.end.format("%B %d %Y %I:%M %p").to_string() as String,
        reset_hours: *jd.reset_hours,
        current_dt: jd.current_dt.format("%B %d %Y %I:%M %p").to_string() as String,
        days_needed: jd.max_date.num_days() as u32,
        tokens_needed: *jd.tokens_needed,
        tokens_claimed: *jd.tokens_claimed,
        daily_tokens: *jd.daily_tokens,
        remaining: remaining.num_days() as u32,
        reset_time: reset_time.format("%B %d %Y at %I:%M %p").to_string() as String,
        final_day: final_day.format("%B/%d/%Y").to_string() as String
    }
}
#[allow(unused_must_use, dead_code)]
pub fn json_out(args: &ArgMatches, jd: &JsonData) -> Result<()> {
    let json = serde_json::to_string_pretty(&jd).unwrap();
    let json_str = json.as_str();
    if args.is_present("write_json") {
        let cwd = env::current_dir().unwrap();
        let cwd: String = cwd.to_str().unwrap().into();
        let _default = format!("{}/STO_Event_Calendar.json", cwd);
        let _default = _default.as_str();
        let out = args.value_of("write_json").unwrap_or(_default);
        
        fs::write(out, &json).expect("Can not write to file");
    }

    if args.is_present("print_json") {
        println!("{}", json_str);
    }
    Ok(())
}