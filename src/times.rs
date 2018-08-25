extern crate chrono;
extern crate time;

use chrono::prelude::*;
use time::Duration;

pub struct Times {
    pub end: NaiveDate,
    pub reset_hours: f64,
    pub current_dt: chrono::NaiveDateTime,
    pub days_needed: chrono::Duration
    }

impl Times {
    pub fn reset(&self) -> chrono::NaiveDateTime {
        self.current_dt + Duration::hours(self.reset_hours as i64)
    }

    pub fn remaining(&self) -> chrono::Duration {
        NaiveDate::signed_duration_since(self.end, self.current_dt.date())
    }

    pub fn final_day(&self) -> chrono::NaiveDate {
        self.end - self.days_needed
    }
}