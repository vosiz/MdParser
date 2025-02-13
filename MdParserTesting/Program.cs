using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MdParser;

namespace MdParserTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            string TEST_RSC_1 = "../../TestResources/CHANGELOG.md";

            try
            {
                // create instance
                var parser = new MdParser.Container();

                // load MD file
                parser.Load(TEST_RSC_1);

                // process it
                parser.Parse(false);

                // save it to desired format
                parser.Save(OutputType.TextFile, "./Output/changelog.txt");
            }
            catch (Exception exc) {

                throw exc;
            }

        }
    }
}
