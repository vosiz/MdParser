using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdParser.Parser
{
    public class Node
    {
        public Node Parent;
        public List<Node> Children;
        public string Text;
        public NodeType Type = NodeType.Text;
        public NodeConnectivity Connectivity = NodeConnectivity.Terminal;
        public int Level;

        public Node() {

            Children = new List<Node>();
        }
        public Node(string value) : this() {

            Text = value;
        }

        public void AttachParent(Node node) {

            Parent = node;
            node.AddChild(this);
        }

        public void AddChildren(Node[] nodes) {

            foreach (var node in nodes) {

                AddChild(node);
            }
        }

        public void AddChild(Node node) {

            Children.Add(node);
        }

        public IEnumerable<Node> TraverseDepthFirst()
        {
            yield return this;

            foreach (var child in Children)
            {
                foreach (var descendant in child.TraverseDepthFirst())
                {
                    yield return descendant;
                }
            }
        }

    }
}
