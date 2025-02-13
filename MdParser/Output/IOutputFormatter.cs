using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MdParser.Parser;

namespace MdParser.Output
{
    interface IOutputFormatter
    {
        // Exports nodes to a file
        Retval ToFile(Node root, string outputfile);
    }
}
