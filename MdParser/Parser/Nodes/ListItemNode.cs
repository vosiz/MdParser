using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdParser.Parser
{
    class ListItemNode : Node
    {
        public bool IsNumeric() => Numeric;

        protected bool Numeric = false;


        public ListItemNode(string text, bool numeric = false) {

            Text = text;
            Numeric = numeric;
            Type = NodeType.ListItem;
        }
    }

    class ListItemNumericNode : ListItemNode
    {
        public int Number { get; private set; }

        public ListItemNumericNode(string text, int n) : base(text, true) {

            Text = text;
            Numeric = true;
            Number = n;
        }
    }
}
