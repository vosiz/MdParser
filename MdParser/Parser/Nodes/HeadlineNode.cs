using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdParser.Parser
{
    class HeadlineNode : Node
    {
        public HeadlineNode(string text, int level = 1) {

            Text = text;
            Level = level;
            Type = NodeType.HeadLine;
            Connectivity = NodeConnectivity.MiddleLeveled;
        }
    }
}
