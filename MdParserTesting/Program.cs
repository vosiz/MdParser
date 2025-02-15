using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdParserTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Test1 - parser basics
                var test = new Tests.Test1();


                test.RunTest();
            }
            catch (Exception exc) {

                throw exc;
            }

        }
    }
}
