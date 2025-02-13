using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MdParser.Utils;

namespace MdParser.Parser
{
    partial class Parser
    {
        private Node ParseText(string row) {

            if (string.IsNullOrEmpty(row) || string.IsNullOrWhiteSpace(row)) {

                // create empty node
                var node = new Node("");
                node.Connectivity = NodeConnectivity.Empty;
                return node;
            }
                
            return new Node(row);
        }

        private Node ParseHeadline(string row) {

            if (!row.StartsWith("#"))
                return null;

            int level = 0;
            int index = 0;
            foreach (var c in row) {

                index++;

                if (c == '#')
                    level++;
                else
                    break;
            }

            var text = row.Substring(index);

            return new HeadlineNode(text, level);
        }

        private Node ParseListItem(string row) {

            if (!row.StartsWith("-"))
                return null;

            if (row.Length < 2)
                throw new MdParserException(Retval.ParseListItemShort, "List item too short", row);

            var text = row.Substring(2);

            return new ListItemNode(text);
        }
    }
}
