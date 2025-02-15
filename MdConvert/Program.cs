using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdConvert
{
    class Program
    {
        static void Main(string[] args)
        {
            var ar = new Argumentos.Args(args, new string[] { "-", "--", "/" });

            ar.AddDescription("MD convertion tool to other formats");

            // commands
            ar.AddCommand(new Argumentos.Command("help", "Print help"));
            ar.AddCommand(new Argumentos.Command("input", "Input MD file path", true));
            ar.AddCommand(new Argumentos.Command("output", "Output file path", true));
            ar.AddCommand(new Argumentos.Command("type", "Output file type", true));

            ar.Process();

            if (ar.HasArgument("help")) {

                PrintHelp();
                return;
            }

            Converter.Converter.Convert(
                ar.GetArgument<string>("input"),
                ar.GetArgument<string>("output"),
                ar.GetArgument<string>("type")
            );
        }

        static void PrintHelp() { 
        
        }
    }
}
