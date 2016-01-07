using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plugin3
{
    public class ReversePlugin
    {
        public static string GetPluginName()
        {
            return "Reverse";
        }

        public static void Reverse(RichTextBox value)
        {
            value.Text =  new string(value.Text.ToCharArray().Reverse().ToArray());

        }
    }
}
