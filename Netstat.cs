using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace DotNetstat
{
    public static class Netstat
    {
        public static List<NetInfo> GetNetstat()
        {
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo
            {
                //No Window
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                //Launch info
                FileName = "netstat",
                Arguments = "-aon"
            };
            p.Start();

            string stdOut = p.StandardOutput.ReadToEnd();
            stdOut = Regex.Replace(stdOut, @"[ \t]+", " ");
            p.WaitForExit();

            string[] netlines = stdOut.Split('\n');

            List<NetInfo> retList = new List<NetInfo>(netlines.Length - 4);

            for (int i = 4; i < netlines.Length; i++)
            {
                if (!string.IsNullOrEmpty(netlines[i]))
                    retList.Add(new NetInfo(netlines[i]));
            }
            return retList;
        }
    }
}
