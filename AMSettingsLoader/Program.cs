using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSettingsLoader
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if(args.Length == 0)
            {
                Console.WriteLine("Specify an ini file");
                return;
            }

            var amc = new AlphaManagerClient();
            var ini = new IniFile(args[0]);

            foreach(var setting in ini.Settings)
            {
                try
                {
                    amc.SetValue(setting.Key, setting.Value);
                }
                catch
                {

                }
            }
        }
    }
}
