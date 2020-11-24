using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PartialViewHomeWork.Helpers
{
    public static class Helper
    {
        public static void DeleteFromRoot(string webroot,string folder,string filename)
        {
            string filepath = Path.Combine(webroot,folder,filename);
            if (System.IO.File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
            }
        }
    }
}
