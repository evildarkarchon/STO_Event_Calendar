use chrono::prelude::*;
#[allow(unused_imports)]use serde_json::Error;
use time::Duration;

pub struct Times<'a> {
    pub end: &'a Date<Local>,
    pub reset_hours: &'a f32,
    pub current_dt: DateTime<Local>,
    pub max_date: Duration,
    pub tokens_needed: &'a u32,
    pub tokens_claimed: &'a u32,
    pub daily_tokens: &'a u32
    }

impl<'a> Times<'a> {
    pub fn reset_time(&self) -> DateTime<Local> {
        self.current_dt + Duration::hours(*self.reset_hours as i64)
    }

    pub fn remaining(&self) -> Duration {
        let out = Date::signed_duration_since(*self.end, self.current_dt.date());
        assert!(out.num_days() > 0, "The days remaining calculation");
        out
    }

    pub fn final_day(&self) -> Date<Local> {
        *self.end - self.max_date
    }
}

pub fn build_times<'a>(end: &'a Date<Local>, reset_hours: &'a f32, daily_tokens: &'a u32, tokens_needed: &'a u32, tokens_claimed: &'a u32) -> Times<'a> {
    let days = ((*tokens_needed - *tokens_claimed) - *daily_tokens) as f64;
    let days = days.ceil();
    let days = days as i64;
    let days = Duration::days(days);
    assert!(days.num_days() > 0, "The days needed calculation returned a negative number.");
    let out = Times {
        end,
        reset_hours,
        current_dt: Local::now(),
        tokens_claimed,
        tokens_needed,
        daily_tokens,
        max_date: days
    };
    out
}
