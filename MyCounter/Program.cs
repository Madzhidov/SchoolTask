using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;


namespace MyCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            string FileContents = File.ReadAllText(args[0]);
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            using (var client = new ServiceReference1.Service1Client())
            {
                var WordCounts = client.countWord(FileContents);
                List<Tuple<int, string>> WordStats = WordCounts.Select(x => new Tuple<int, string>(x.Value, x.Key)).ToList();
                WordStats.Sort((x, y) => y.Item1.CompareTo(x.Item1));
                foreach (Tuple<int, string> t in WordStats)
                    File.AppendAllText(args[1], t.Item2 + " " + t.Item1 + Environment.NewLine);
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
            Console.ReadLine();
        }
    }
}
