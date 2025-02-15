using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdConvert.Converter
{

    public static class ConvertType
    {

        public static MdParser.OutputType TypeFromString(string type) {

            switch (type) {

                case "text":
                case "txt":
                    return MdParser.OutputType.TextFile;

                default:
                    throw new MdConvertException($"Undefinded ToConvert type {type}");
            }
        }
    }
}
