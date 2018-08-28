using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ICMS.Lite.Service
{
    public class Logger
    {
        public static void WriteLog(string msg)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\icmslog.txt");
            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine("==================================================================");
            writer.WriteLine(msg);
            writer.WriteLine("==================================================================");
            writer.Close();
        }
    }
}
