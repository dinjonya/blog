using System.IO;
using System.Reflection;
using System.Text;
using CorePlugs20.Models;
using Newtonsoft.Json;

namespace CorePlugs20.PlugConfig
{
    public class PlugHelper
    {
        public static PlugModel ReadPlugConfig()
        {
            Assembly ass = Assembly.GetAssembly(typeof(CorePlugs20.PlugConfig.PlugHelper));
            byte[] bs = null;
            using (Stream stream = ass.GetManifestResourceStream("CorePlugs20.Config.json"))
            {
                bs = new byte[stream.Length];
                stream.Read(bs, 0, (int)stream.Length);
                stream.Close();
            }
            UTF8Encoding encod8 = new UTF8Encoding();
            string str = encod8.GetString(bs);
            return JsonConvert.DeserializeObject<PlugModel>(str);
        }
    }
}