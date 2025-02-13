using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdParser.Utils
{
    public class MdParserException : Exception
    {
        public MdParserException(Retval code, string message, string inner = null) : 
            base(
                string.Format(
                    "Error_0x{0:X2}: {1}", 
                    (int)code, message), 
                    inner != null ? new Exception(inner) : null
                ) { }

        public override string ToString() {

            string str = string.Empty;
            str += this.Message;
            if (this.InnerException != null)
                str += InnerException.ToString();

            return str;
        }
    }
}
