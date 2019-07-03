using System;

namespace STO_Event_Calendar
{
    class Print
    {
        public static void AnnounceEnd(TimeSpan EndDiff)
        {
            if (EndDiff.Days >= 1)
            {
                if (EndDiff.Days > 1) { Console.WriteLine("There are {0} days until the event ends.", EndDiff.Days); }
                else if (EndDiff.Days == 1) { Console.WriteLine("There is 1 day until the event ends."); }
            }
            else if (EndDiff.Days < 1)
            {
                if (EndDiff.Hours >= 1)
                {
                    if (EndDiff.Hours > 1) { Console.WriteLine("There are {0} hours until the event ends", EndDiff.Hours); }
                    else if (EndDiff.Hours == 1) { Console.WriteLine("There is 1 hour until the event ends."); }
                }
                else if (EndDiff.Hours < 1)
                {
                    if (EndDiff.Minutes > 1) { Console.WriteLine("There are {0} minutes until the event ends.", EndDiff.Minutes); }
                    else if (EndDiff.Minutes == 1) { Console.WriteLine("There is only 1 minute until the event ends."); }
                    else if (EndDiff.Minutes < 1 && EndDiff.Seconds < 1) { Console.WriteLine("The event is over, sorry."); }
                }
            }
        }

        public static void Announce(STO DateCalc, DateTime FinalDay)
        {
            Console.WriteLine("Todays Date: {0}", DateCalc.Now.ToShortDateString());
            Console.WriteLine("Daily Quests will be available on {0} at {1}", DateCalc.Reset.ToShortDateString(), DateCalc.Reset.ToShortTimeString());
            Console.WriteLine("Estimated date of completion: {0}", DateCalc.DateNeeded().ToShortDateString());
            Console.WriteLine("Days needed to complete the event: {0}", DateCalc.DaysNeeded.Days);

            if (FinalDay < DateCalc.Now) { Console.WriteLine("There is no way to complete this event, sorry."); }
            else if (FinalDay.Day == DateCalc.Now.Day) { Console.WriteLine("You have to do dailies every day to be able to get enough tokens."); }
            Console.WriteLine("The last day to start the event is: {0}", FinalDay.ToShortDateString());
            Console.WriteLine("The event ends on {0}", DateCalc.End.ToShortDateString());
        }
    }
}
