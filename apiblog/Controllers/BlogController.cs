using apiblog.Models;
using CorePlugs20.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace apiblog.Controllers
{
    [Route("apiblog/api1.0/[controller]")]
    public class BlogController : Controller
    {
        private IHostingEnvironment hostingEnv;
        BlogEntities db;
        public BlogController(BlogEntities _db,IHostingEnvironment env)
        {
            db = _db;
            hostingEnv = env;
        }

        /*
        接口简介: 后台管理员登录接口
        接口路径: apiblog/api1.0/Blog/GetBlogTitle
        请求方式: Get
        输入参数: 无
        接口返回: Json格式
        {
            statue = true,   //业务逻辑是否成功 失败返回false
            Data = "new { cookie=cookieInfo,otherObject }"     // 失败返回错误信息
        }
        */
        
        [EnableCors("AllowSpecificOrigin")]
        [Route("GetBlogTitle")]
        [HttpGet]
        public ResultData GetBlogTitle()
        {
            
        }
    }
}