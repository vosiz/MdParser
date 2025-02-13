using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdParser.Parser
{
    public class RootNode : Node
    {

        public RootNode() : base() {

            Connectivity = NodeConnectivity.Root;
        }

    }
}
