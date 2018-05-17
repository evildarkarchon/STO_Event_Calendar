extern crate clap;
//extern crate chrono;

use clap::ArgMatches;
//use chrono::prelude::*;

/*pub fn input_date(cmd: &ArgMatches, cmdname: &str) -> NaiveDate {
    const DATE_FORMAT: &'static str = "%m/%d/%Y";
        
    let intermediate: &str = cmd.value_of(cmdname).unwrap();
    return NaiveDate::parse_from_str(intermediate, DATE_FORMAT)
                                                            .ok()
                                                            .unwrap();
}*/

pub fn parse_number(cmd: &ArgMatches, cmdname: &str) -> f64 {
    let intermediate: String = cmd.value_of(cmdname)
                                                .unwrap()
                                                .parse()
                                                .unwrap();
        intermediate
}