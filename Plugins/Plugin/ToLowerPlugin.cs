using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin
{
    public class ToLowerPlugin
    {
        public static string GetPluginName()
        {
            return "ToLower";
        }

        public static string ToLower(string value)
        {
            return value.ToLowerInvariant();

        }
    }
}
