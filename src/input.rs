use clap::ArgMatches;

pub fn input_number_f32(cmd: &ArgMatches, cmdname: &str) -> f32 {
    let intermediate: f32 = cmd.value_of(cmdname)
                                                .unwrap()
                                                .parse()
                                                .unwrap();
        intermediate
}
pub fn input_number_i32(cmd: &ArgMatches, cmdname: &str) -> i32 {
    let intermediate: i32 = cmd.value_of(cmdname)
                                                .unwrap()
                                                .parse()
                                                .unwrap();
        intermediate
}