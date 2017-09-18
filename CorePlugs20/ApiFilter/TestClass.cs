using System;
using System.IO;
using System.Reflection;
using System.Text;
using CorePlugs20.Models;
using CorePlugs20.PlugConfig;

namespace CorePlugs20.ApiFilter
{
    public class TestClass
    {
        public void show()
        {
            PlugModel model = PlugHelper.ReadPlugConfig();
            System.Console.WriteLine($"model.ApiInvokeRecord:{model.ApiInvokeRecord}");
        }
    }
}