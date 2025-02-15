using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MdConvert.Converter
{

    public class MdConvertException : Exception {

        public MdConvertException(string msg) : base("MdConvert error: " + msg) { }
    }

    public static class Converter
    {

        public static void Convert(string input, string output, string type) {

            // input check
            if (!File.Exists(input))
                throw new MdConvertException($"Input file on path \"{input}\" was not found");

            // type check and fetch
            var otype = ConvertType.TypeFromString(type);

            // conversion
            var md = new MdParser.Container();
            md.Load(input);
            md.Parse(false);

            // output file
            md.Save(otype, output);
        }


    }
}
