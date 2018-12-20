using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace Strongpoint.Models
{
    public class LogError
    {
        private static readonly object objLock = new object();
        private static LogError logError;
        private LogError() { }
        public static LogError LogErrorInstance
        {
            get
            {
                lock (objLock)
                {
                    if (logError == null)
                    {
                        logError = new LogError();
                    }
                }
                return logError;
            }
        }
        public void Log(Exception ex,IHostingEnvironment env)
        {
            try
            {
                string filePath = env.WebRootPath + "\\log\\error.txt";
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("-----------------------------------------------------------------------------");
                    writer.WriteLine("Date : " + DateTime.Now.ToString());
                    writer.WriteLine();
                    while (ex != null)
                    {
                        writer.WriteLine(ex.GetType().FullName);
                        writer.WriteLine("Message : " + ex.Message);
                        writer.WriteLine("StackTrace : " + ex.StackTrace);
                        ex = ex.InnerException;
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
