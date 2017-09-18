using Authen.Models;
using CorePlugs20.ApiFilter;
using CorePlugs20.Models;
using CorePlugs20.OdinString;
using CorePlugs20.WebApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Authen.Controllers
{
    [Route("Authen/api1.0/[controller]")]
    public class InvokeRecordController : Controller
    {
        AuthenEntities db;
        public InvokeRecordController(AuthenEntities _db)
        {
            db = _db;
        }
        /*
        接口路径: Authen/api1.0/InvokeRecord/dbsave
        请求方式: Post
        输入参数: Json格式
        InvokeRecordModel对象
        接口返回: void
        接口简介: 记录所有项目api被调用的情况 无需过滤器拦截
        */
        [Route("dbsave")]
        [NoFilter]
        public void Post()
        {
            string result = ApiHelper.GetStringFromRequestBody(Request.Body);
            InvokeRecord_DbModel model = JsonConvert.DeserializeObject<InvokeRecord_DbModel>(result);
            db.InvokeRecords.Add(model);
            db.Entry<InvokeRecord_DbModel>(model).State = EntityState.Added;
            db.SaveChanges();
        }
    }
}