using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin2
{
    public class CapitalizePlugin
    {
        public static string GetPluginName()
        {
            return "Capitalize";
        }

        public static string Capitalize(string value)
        {
            return value.ToUpperInvariant();

        }
    }
}
