using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MdParser.Utils
{
    class FileOps
    {

        public static void EnsureDirectoryExists(string path)
        {
            string dirpath = Path.GetDirectoryName(path);

            if (!string.IsNullOrEmpty(dirpath) && !Directory.Exists(dirpath))
            {
                Directory.CreateDirectory(dirpath);
            }
        }
    }
}
