using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MdParser.Utils;

namespace MdParser.Parser
{

    public enum NodeType {

        Text,
        HeadLine,
        ListItem,
    }

    public enum NodeConnectivity
    {
        Root,           // only children
        Terminal,       // no children
        Middle,         // can have parent and children
        MiddleLeveled,  // can have parent (lower level) and children
        Empty,          // no parent, no children, skip
    }

    partial class Parser
    {
        public string LastError { get; private set; }

        public List<Node> Nodes { get; private set; }


        public Retval ParseToNodes(string[] md_rows, bool skip_unknown = false)
        {

            try
            {
                // raw parsing
                Nodes = new List<Node>();
                foreach (var row in md_rows)
                {
                    var node = TryParse(row);

                    if (node == null && !skip_unknown)
                    {

                        throw new MdParserException(Retval.ParseGeneralError, "Row cannot be parsed, unknown type, check inner-exc for row-string", row);
                    }

                    if (node == null && skip_unknown)
                        continue;

                    if (Nodes.Count == 0)
                        Nodes.Add(new RootNode());

                    Nodes.Add(node);
                }

                // connect nodes
                if (Nodes.Count == 0)
                    throw new MdParserException(Retval.ParseNoNodes, "No nodes were parsed");

                var root = Nodes[0];
                var target = root; // pointed node
                foreach (var current in Nodes)
                {

                    // connect nodes by their level and dependencies
                    target = ConnectNode(current, target);
                }

            }
            catch (Exception exc)
            {

                LastError = exc.ToString();
                return Retval.ParseGeneralError;
            }

            return Retval.NoError;
        }

        private Node TryParse(string row)
        {

            Func<Node>[] parsemethods = {
                () => ParseHeadline(row),
                () => ParseListItem(row),
                () => ParseText(row),
            };

            foreach (var m in parsemethods)
            {
                var node = m();
                if (node != null)
                    return node;
            }

            return null;
        }

        // returns mutual parent or null (is dominant node)
        private Node ConnectNode(Node current, Node target)
        {

            switch (current.Connectivity)
            {

                case NodeConnectivity.Root:
                    return current;

                case NodeConnectivity.Empty:
                case NodeConnectivity.Terminal:
                    current.AttachParent(target);
                    return target;

                case NodeConnectivity.Middle:
                    current.AttachParent(target);
                    return current;

                case NodeConnectivity.MiddleLeveled:

                    // same thing
                    if (current.GetType() == target.GetType())
                    {
                        // current lower level
                        if (current.Level < target.Level) {

                            // find suitable parent
                            var parent = target.Parent;
                            return ConnectNode(current, parent);
                        }

                        // current same level
                        else if (current.Level == target.Level)
                        {
                            current.AttachParent(target.Parent);
                            return current;
                        }

                        // current higher level
                        else {                        

                            current.AttachParent(target);
                            return current;
                        }
                    }
                    else { // not same type

                        current.AttachParent(target);
                        return current;
                    }

                default:
                    throw new MdParserException(Retval.ParserUndefinedNodeConnectivity, "Unimplemented NODE connectivity", "Connectivity=" + current.Connectivity.ToString());
            }


        }
    }
}
