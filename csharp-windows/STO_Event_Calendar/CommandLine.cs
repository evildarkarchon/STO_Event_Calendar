using CommandLine;
using System.IO;

namespace STO_Event_Calendar
{
    class Options
    {
        [Option('d', "daily-tokens", HelpText = "Amount of tokens you get per reset.")]
        public uint DailyTokens { get; set; }

        [Option('t', "total-tokens", HelpText = "Amount of tokens needed to get the rewards.")]
        public uint TotalTokens { get; set; }

        [Option('c', "tokens-claimed", HelpText = "Amount of tokens you've already claimed.")]
        public uint TokensClaimed { get; set; }

        [Option('e', "end-date", HelpText = "Date that the event ends.")]
        public string EndDate { get; set; }

        [Option('r', "reset", HelpText = "Amount of hours until the daily quests reset.")]
        public float Reset { get; set; }

        [Option('j', "json", HelpText = "Save data as a json file.")] // This feature is to help me debug any possible data problems.
        public bool Json { get; set; }

        [Option('o', "output", Required = false, HelpText = "Where to save the JSON file (defaults to STO_Event_Calendar.json in the current directory).")]
        public string JsonPath { get; set; } = $"{Directory.GetCurrentDirectory()}\\STO_Event_Calendar.json";

        [Option('q', "quiet", HelpText = "Don't actually print anything on the console (Best combined with --json)")]
        public bool Quiet { get; set; }

        [Option('p', "print-json", HelpText = "Print the json to the console instead of writing it to a file.")]
        public bool PrintJSON { get; set; }
    }
}