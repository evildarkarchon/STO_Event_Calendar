using System.IO;
using CommandLine;

namespace STO_Event_Calendar
{
    public class Options
    {
        [Option('d', "daily-tokens", HelpText = "Amount of tokens you get per reset.")]
        public uint DailyTokens { get; set; }

        [Option('t', "total-tokens", HelpText = "Amount of tokens needed to complete the project.")]
        public uint TotalTokens { get; set; }

        [Option('c', "tokens-claimed", HelpText = "Amount of tokens you've already claimed")]
        public uint TokensClaimed { get; set; }

        [Option('e', "end-date", HelpText = "Date that the event ends.")]
        public string EndDate { get; set; }

        [Option('r', "reset", HelpText  = "Amount of time in hours until the quests reset")]
        public float Reset { get; set; }

        [Option('j', "json", HelpText = "Save the raw data as a JSON file.")]
        public bool Json { get; set; }

        [Option('f', "json-file", HelpText = "Path where the JSON file will be saved (defaults to STO_Event_Calendar.json in the current directory)")]
        public string JsonPath { get; set; } = $"{Directory.GetCurrentDirectory()}\\STO_Event_Calendar.json";

        [Option('q', "quiet", HelpText = "Suppress all the pretty text output (should be used with --json and/or --print-json)")]
        public bool Quiet { get; set; }

        [Option('p', "print-json", HelpText  = "Print the raw data as json to the console.")]
        public bool PrintJSON { get; set; }
    }
}

