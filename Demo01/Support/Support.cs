using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo01.Support
{
    public class Support
    {
        public static string GetBase64ForImage(string urlImage)
        {
            byte[] imgBytes = System.IO.File.ReadAllBytes(urlImage);
            string base64 = Convert.ToBase64String(imgBytes);
            return base64;
        }
    }
}
