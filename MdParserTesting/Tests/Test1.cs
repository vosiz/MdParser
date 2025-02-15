using MdParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdParserTesting.Tests
{
    class Test1 : ITest
    {
        public void RunTest(params object[] pars)
        {
            // create instance
            var parser = new MdParser.Container();

            // load MD file
            parser.Load(Shared.TEST_RSC_1);

            // process it
            parser.Parse(false);

            // save it to desired format
            parser.Save(OutputType.TextFile, "./Output/changelog.txt");
        }
    }
}
