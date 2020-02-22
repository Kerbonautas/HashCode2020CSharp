using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Hashcode2020CSharp
{
    public static class ExceptionHandler
    {
        public static void HandleException(Exception e)
        {
            DateTime date = DateTime.Now;
            using (StreamWriter sw = File.AppendText(@".\GoogleFiles\ExceptionLog.txt"))
            {
                sw.Write("[" + date.ToString() + "] - ");
                sw.WriteLine(e.GetType().ToString());
                sw.WriteLine("- Description: " + e.Message);
                sw.WriteLine("- Error: " + e.ToString());
                sw.WriteLine("\n--------------------------------\n");
            }

        }
    }
}
