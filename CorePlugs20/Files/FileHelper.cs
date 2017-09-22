using System.Collections.Generic;
using System.IO;

namespace CorePlugs20.Files
{
    public static class FileHelper
    {
        public static Dictionary<string,string> FileFormatByContent = new Dictionary<string, string>()
        {
            {"image/fax",".fax"},
            {"image/gif",".gif"},
            {"image/x-icon",".ico"},
            {"image/jpeg",".jpg"},
            {"image/pnetvue",".net"},
            {"image/png",".png"},
            {"image/vnd.rn-realpix",".rp"},
            {"image/tiff",".tif"},
            {"image/vnd.wap.wbmp",".wbmp"},
        };
        /// <summary>
        /// 系统分隔符  win '\'  other '/'
        /// </summary>
        /// <returns></returns>
        public static string DirectorySeparatorChar = Path.DirectorySeparatorChar.ToString();
    }
}