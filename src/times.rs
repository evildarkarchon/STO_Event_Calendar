use chrono::prelude::*;
#[allow(unused_imports)]use serde_json::Error;
use time::Duration;

pub struct Times<'a> {
    pub end: &'a NaiveDate,
    pub reset_hours: &'a f32,
    pub current_dt: DateTime<Local>,
    pub days_needed: Duration,
    pub tokens_needed: &'a u32,
    pub tokens_claimed: &'a u32,
    pub daily_tokens: &'a u32
    }

impl<'a> Times<'a> {
    pub fn reset(&self) -> DateTime<Local> {
        self.current_dt + Duration::hours(*self.reset_hours as i64)
    }

    pub fn remaining(&self) -> Duration {
        NaiveDate::signed_duration_since(*self.end, self.current_dt.date().naive_local())
    }

    pub fn final_day(&self) -> NaiveDate {
        *self.end - self.days_needed
    }
}

pub fn build_times<'a>(end: &'a NaiveDate, reset_hours: &'a f32, daily_tokens: &'a u32, tokens_needed: &'a u32, tokens_claimed: &'a u32) -> Times<'a> {
    let days = ((*tokens_needed - *tokens_claimed) - *daily_tokens) as f64;
    let days = days.ceil();
    let days = days as i64;
    let days = Duration::hours(days);
    let out = Times {
        end,
        reset_hours,
        current_dt: Local::now(),
        tokens_claimed,
        tokens_needed,
        daily_tokens,
        days_needed: days
    };
    out
}
