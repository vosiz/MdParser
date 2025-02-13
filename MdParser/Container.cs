using System;
using System.IO;
using MdParser.Utils;
using MdParser.Output;
using System.Collections.Generic;

namespace MdParser
{
    public enum Retval { 
    
        // 00 - No problems
        NoError = 0x00,

        // 1# - General errors
        GeneralError = 0x10,
        LoadGeneralError = 0x11,
        ParseGeneralError = 0x12,
        SaveGeneralError = 0x13,

        // 3# - File problems
        FileNotFound = 0x30,
        FileNotMd = 0x31,

        // 4# - Parse problems
        ParseNotMd = 0x40,
        ParseListItemShort = 0x41,
        ParseNoNodes = 0x42,
        ParserUndefinedNodeConnectivity = 0x43,

        // (A-B)# - Output problems
        OutputUnknown = 0xA0,
        OutputUnknownType = 0xA1,
        OutputUndefinedLevel = 0xB0,
    }

    public enum OutputType { 
    
        TextFile = 0x01,
    }

    public class Container
    {
        private string LastError;
        private string[] Content;

        private Parser.Parser Parser;

        private Dictionary<OutputType, IOutputFormatter> SupportedOutputs;

        // Constructor
        public Container() {

            SupportedOutputs = new Dictionary<OutputType, IOutputFormatter>();
            SupportedOutputs.Add(OutputType.TextFile, new Output.TextBased.SimpleTextOutput());
        }

        // Load file
        public Retval Load(string path) {

            try
            {
                if (!File.Exists(path))
                    throw new MdParserException(Retval.FileNotFound, "Path not found", path);

                string content = File.ReadAllText(path);
                Content = content.SplitByNl();
            }
            catch (Exception exc) {

                LastError = exc.ToString();
                throw exc;
            }

            Parser = new Parser.Parser();

            return Retval.NoError;
        }

        // Parse to node structure
        public Retval Parse(bool skip_unknowns) {

            try
            {
                Parser.ParseToNodes(Content, skip_unknowns);
            }
            catch (Exception exc) {

                LastError = exc.ToString();
                return Retval.ParseGeneralError;
            }

            return Retval.NoError;
        }

        // Output to file
        public Retval Save(OutputType otype, string path) {

            try
            {
                var saver = SupportedOutputs[otype];
                FileOps.EnsureDirectoryExists(path);
                saver.ToFile(Parser.Nodes[0], path);
            }
            catch (IndexOutOfRangeException exc)
            {
                throw new MdParserException(Retval.OutputUnknown, "There is no output formatter registered to this type = " + otype.ToString());
            }
            catch (Exception exc)
            {
                throw new MdParserException(Retval.SaveGeneralError, exc.Message);
            }

            return Retval.NoError;
        }

        // Check last operation on errors
        public string CheckRetval(Retval operation_result) {

            if (operation_result != Retval.NoError) {

                return LastError;
            }

            return null;
        }
    }
}
