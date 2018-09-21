﻿using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace STO_Event_Calendar
{
    struct JSONInfo
    {
        public DateTime Now { get; set; }
        public DateTime Reset { get; set; }
        public uint DaysNeeded { get; set; }
        public uint EndDiff { get; set; }
        public DateTime End { get; set; }
        public Dictionary<string, uint> AllTokens { get; set; }
        public DateTime FinalDay { get; set; }
        public DateTime DateNeeded { get; set; }
    }

    class ConvertJSON
    {
        public static Nullable<JSONInfo> CalcJSONInfo(ref Calc DateCalc) {
            Nullable<JSONInfo> Out = new JSONInfo() {
                Now = DateCalc.Now,
                Reset = DateCalc.Reset,
                DaysNeeded = (uint)DateCalc.DaysNeeded.Days,
                EndDiff = (uint)DateCalc.EndDiff.Days,
                End = DateCalc.End,
                AllTokens = DateCalc.AllTokens,
                FinalDay = DateCalc.FinalDay(),
                DateNeeded = DateCalc.DateNeeded()
            };
            return Out;
        }
        public static void OutJSON(ref Nullable<JSONInfo> Base, string Path) { File.WriteAllText(Path, JsonConvert.SerializeObject(Base, Formatting.Indented)); }

        public static void PrintJSON(ref Nullable<JSONInfo> Base)
        {
            string output = JsonConvert.SerializeObject(Base, Formatting.Indented);
            Console.WriteLine("Here's the raw data (in JSON form):");
            Console.WriteLine(output);
        }
    }
}