using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace HGI.Models.General
{
    public class LogWritter
    {
        const double Megabyte = 1048576;

        public static void WriteErrorLog(Exception ex)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + ex.Source.ToString().Trim() + "; " + ex.Message.ToString().Trim());
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }

        public static void WriteErrorLog(string Message)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + Message);
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }

        public static void DeleteTempFiles(int sizeFile)
        {
            try
            {
                string[] files = Directory.GetFiles(System.AppDomain.CurrentDomain.BaseDirectory, "LogFile.txt");

                if (files.Length > 0)
                {
                    foreach (string file in files)
                    {
                        var name = new FileInfo(file).Name;
                        name = name.ToLower();

                        var fileS = new FileInfo(file);
                        var length = (fileS.Length / Megabyte);

                        if (name == "logfile.txt" && length > sizeFile)
                        {
                            File.Delete(file);
                        }
                    }
                }
            }
            catch
            {
            }
        }
    }
}