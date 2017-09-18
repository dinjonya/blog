using System.Security.Cryptography;
using System.Text;

namespace CorePlugs20.OdinSecurity
{
    public static class StringSecurity
    {
        public static string StringToMd5ToLower(this string str,int length=32)
        {
            MD5 md5 = MD5.Create();
            byte[] bt = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bt.Length; i++)
            {
                sb.AppendFormat("{0:x2}", bt[i]);
            }
            return sb.ToString().ToLower().Substring(0, length);
        }

        

        
    }
}