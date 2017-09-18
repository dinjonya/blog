using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace CorePlugs20.OdinString
{
    public static class StringExtends
    {
        public static string StringToBase64String(this string str)
        {
            byte[] bt = System.Text.Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(bt);
        }

        public static string Base64StringToString(this string str)
        {
            byte[] bt = Convert.FromBase64String(str);
            return System.Text.Encoding.UTF8.GetString(bt);
        }

        public static string ToJsonFormatString(this string str)
        {
           //格式化json字符串
           JsonSerializer serializer = new JsonSerializer();
           TextReader tr = new StringReader(str);
           JsonTextReader jtr = new JsonTextReader(tr);
           object obj = serializer.Deserialize(jtr);
           if (obj != null)
           {
               StringWriter textWriter = new StringWriter();
               JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
               {
                   Formatting = Formatting.Indented,
                   Indentation = 4,
                   IndentChar = ' '
               };
               serializer.Serialize(jsonWriter, obj);
               return textWriter.ToString();
           }
           else
           {
               return str;
           }         
       }
    }
}