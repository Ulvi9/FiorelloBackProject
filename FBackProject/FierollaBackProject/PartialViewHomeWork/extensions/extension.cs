using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace PartialViewHomeWork.extensions
{
    public static class extension
    {
        public static bool IsImage(this IFormFile file)
        {
            return file.ContentType.Contains("image/");
        }

        public static bool Maxlength(this IFormFile file,int kb)
        {
            return file.Length / 1024 > kb;
        }
        public  async static Task<string> Saveimg(this IFormFile file,string root,string folder)
        {
            string path = root;
            string filename = Guid.NewGuid().ToString() + file.FileName;
            string resultpath = Path.Combine(path, folder, filename);

            using (FileStream filestream = new FileStream(resultpath, FileMode.Create))
            {
                await file.CopyToAsync(filestream);
            }
            return filename;
        }
        
    }
}
