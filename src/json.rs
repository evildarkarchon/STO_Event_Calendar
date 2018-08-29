#[allow(unused_imports)]use serde_json::Error;
#[allow(unused_imports)]use times::*;


#[derive(Serialize)]
pub struct JsonData {
    pub end: String,
    pub reset_hours: f32,
    pub current_dt: String,
    pub days_needed: u32,
    pub tokens_needed: u32,
    pub tokens_claimed: u32,
    pub daily_tokens: u32
}

pub fn build_jsondata(jd: &Times) -> JsonData {
    JsonData {
        end: jd.end.format("%B %d %Y %I:%M %p").to_string() as String,
        reset_hours: *jd.reset_hours,
        current_dt: jd.current_dt.format("%B %d %Y %I:%M %p").to_string() as String,
        days_needed: jd.days_needed.num_days() as u32,
        tokens_needed: *jd.tokens_needed,
        tokens_claimed: *jd.tokens_claimed,
        daily_tokens: *jd.daily_tokens
    }
}
