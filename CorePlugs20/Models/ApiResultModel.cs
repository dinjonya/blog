using System;
namespace CorePlugs20.Models
{
    /// <summary>
    /// 统一api传递格式
    /// {
    //     "Value": {},
    //     "Formatters": [],
    //     "ContentTypes": [],
    //     "DeclaredType": "NCDF.Models.ConfigModels.ConfigModel, NCDF.Models, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
    //     "StatusCode": null,
    //     "ResultTime": "2017-03-02 13:25:36,2536"
    //  }
    /// </summary>
    public class ApiResultModel
    {
        public ResultData Value { get; set; }

        public string[] Formatters { get; set; }

        public string[] ContentTypes { get; set; }

        public string DeclaredType { get; set; }

        public string StatusCode { get; set; }

        public string ResultTime {get;set;}
    }
    public class ResultData
    {
        public bool Status { get; set; }
        public Object Data { get; set; }
    }
}