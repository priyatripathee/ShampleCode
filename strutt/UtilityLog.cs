using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace strutt
{
    public static class UtilityLog
    {
        public static string LogFile { get; set; }
        public static void WriteToLog(string Log)
        {
            if (File.Exists(LogFile))
            {
                using (StreamWriter w = File.AppendText(LogFile))
                {
                    w.WriteLine(DateTime.Now.ToString("hh:mm:ss:ffff") + " - " + Log);
                }
            }
            else
                File.AppendAllText(LogFile, DateTime.Now.ToString("hh:mm:ss:ffff") + " - " + Log + Environment.NewLine);
        }
    }
}