using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSettingsLoader
{
    internal class IniFile
    {
        public Dictionary<string, int> Settings { get; } = new Dictionary<string, int>();

        public IniFile(string path)
        {
            var lines = File.ReadAllLines(path);

            foreach (var line in lines.Where(s => s.Contains('=')))
            {
                var parts = line.Split('=');
                if(int.TryParse(parts[1], out var value))
                {
                    Settings[parts[0]] = value;
                }
            }
        }
    }
}
