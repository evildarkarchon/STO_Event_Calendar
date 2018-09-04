use clap::ArgMatches;

pub fn input_number_f32(cmd: &ArgMatches, cmdname: &str) -> f32 {
    let intermediate: f32 = cmd.value_of(cmdname).unwrap().parse().ok().unwrap();
    assert!(intermediate > 0 as f32, "You must put in a value greater than 0");
        intermediate
}

pub fn input_number_u32(cmd: &ArgMatches, cmdname: &str) -> u32 {
    let intermediate: u32 = cmd.value_of(cmdname).unwrap().parse().ok().unwrap();
    assert!(intermediate > 0 as u32, "You must put in a value greater than 0");
        intermediate
}

pub fn ask(message: &str) -> String {
    print!("{}", message);
    let out: String = read!("{}\n");
    out
}