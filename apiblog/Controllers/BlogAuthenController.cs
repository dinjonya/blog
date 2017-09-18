using CorePlugs20.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiblog.Controllers
{
    [Route("apiblog/api1.0/[controller]")]
    public class BlogAuthenController : Controller
    {
        /*
        接口路径: api1.0/apiblog/blogauthen/puv/{pagePath}/{rnd}
        请求方式: get
        输入参数: 
        1.pagePath:页面路径
        2.rnd:防冲突参数
        接口返回: Json格式
        {
            Status = true,  // true 业务逻辑成功 false失败
            Data = CookieInfo对象   
        }
        接口简介: 
        */
        
        [Route("puv/{pagePath}/{rnd}")]
        [HttpGet]
        public CookieInfo SetWebPvUv(string pagePath,string rnd)
        {
            if(HttpContext.Request.Cookies.Keys.Contains("odinSamBlog"))
            {
                //只+pv

            }
            else
            {
                //+pv

                //生成cookie

                //+uv

            }
            return null;
        }
    }
}