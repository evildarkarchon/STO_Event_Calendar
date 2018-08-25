extern crate clap;

use clap::ArgMatches;

pub fn input_number(cmd: &ArgMatches, cmdname: &str) -> f64 {
    let intermediate: f64 = cmd.value_of(cmdname)
                                                .unwrap()
                                                .parse()
                                                .unwrap();
        intermediate
}