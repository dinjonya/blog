namespace CorePlugs20.Models
{
    public class PlugModel
    {
        public ApiInvokeRecordConfig ApiInvokeRecord { get; set; }

    }

    public class ApiInvokeRecordConfig
    {
        public bool Used { get; set; }
        public InvokeMethodConfig InvokeMethod { get; set; }
    }

    public class InvokeMethodConfig
    {
        public string Uri { get; set; }
        public string MethodName { get; set; }
    }
}