using MdParser.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MdParser.Utils;
using System.IO;

namespace MdParser.Output.TextBased
{
    class SimpleTextOutput : IOutputFormatter
    {

        private Dictionary<int, KeyValuePair<char, int>> Headers;

        public SimpleTextOutput() {

            Headers = new Dictionary<int, KeyValuePair<char, int>>();
            Headers.Add(1, new KeyValuePair<char, int>('/', 50));
            Headers.Add(2, new KeyValuePair<char, int>('=', 30));
            Headers.Add(3, new KeyValuePair<char, int>('-', 15));
        }

        public Retval ToFile(Node root, string outputfile)
        {
            string output = string.Empty;

            foreach (var node in root.TraverseDepthFirst()) {

                switch (node.Type)
                {
                    case NodeType.HeadLine:
                        output += AddHeadline(node);
                        break;

                    case NodeType.ListItem:
                        output += AddListItem(node);
                        break;

                    case NodeType.Text:
                        output += AddText(node);
                        break;

                    default:
                        throw new MdParserException(Retval.OutputUnknownType, "Node type from MD is uknown or missinf function to process", "Nodetype = " + node.Type.ToString());

                }
            }

            File.WriteAllText(outputfile, output);

            return Retval.NoError;
        }

        private string AddHeadline(Node node) {

            try
            {
                var header = Headers[node.Level];
                char c = header.Key;
                int count = header.Value;

                string output = string.Format("{0}{0} {1} {0}", c, node.Text);
                for (int i = 0; i < count; i++) {

                    output += c;
                }

                return AddWithNl(output);
            }
            catch (ArgumentOutOfRangeException exc)
            {
                throw new MdParserException(Retval.OutputUndefinedLevel, "Header level undefined", "Level " + node.Level);
            }
            catch (Exception exc) {

                throw new MdParserException(Retval.SaveGeneralError, "Unable to save file", exc.Message);
            }

        }

        private string AddListItem(Node node) {

            return AddWithNl("- " + node.Text);
        }

        private string AddText(Node node) {

            return AddWithNl(node.Text);
        }

        private string AddWithNl(string text) {

            return text + Environment.NewLine;
        }
    }
}
