using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdParser.Utils
{
    static class StringExt
    {
        public static string[] SplitByNl(this string str) {

            str = str.Replace("\n", Environment.NewLine);
            return str.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
        }
    }
}
