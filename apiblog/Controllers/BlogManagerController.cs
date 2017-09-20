using System;
using System.Linq;
using apiblog.Models;
using CorePlugs20.Models;
using CorePlugs20.OdinSecurity;
using CorePlugs20.TimeHelper;
using CorePlugs20.WebApi;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace apiblog.Controllers
{
    [Route("apiblog/api1.0/[controller]")]
    public class BlogManagerController : Controller
    {
        BlogEntities db;
        public BlogManagerController(BlogEntities _db)
        {
            db = _db;
        }

        /*
        接口路径: apiblog/api1.0/BlogManager/Login
        请求方式: Post
        输入参数: Json格式
        {
            "un":"loingName",
            "up":"loginPwd"
        }
        接口返回: Json格式
        {
            statue = true,   //业务逻辑是否成功 失败返回false
            Data = "new { cookie=cookieInfo,otherObject }"     // 失败返回错误信息
        }
        接口简介: 后台管理员登录接口
        */
        
        [EnableCors("AllowSpecificOrigin")]
        [Route("Login")]
        [HttpPost]
        public ResultData Login()
        {
            string strParam = this.RouteData.Values["paramStr"].ToString();
            var js = JObject.Parse(strParam);
            string un = js["un"].ToString();
            string up = js["up"].ToString();
            var blogOwner = db.BlogConfigs.Where(cfg=>cfg.BlogOwner == un).SingleOrDefault();
            if(blogOwner == null)
                return new ResultData { Status = false, Data = "用户名或密码有误 - 0x000001" };
            if(blogOwner.OwnerPwd != (up+blogOwner.Salt).StringToMd5ToLower())
                return new ResultData { Status = false, Data = "用户名或密码有误 - 0x000002" };
            //生成cookie
            string dt = UnixTimeHelper.FromDateTime(DateTime.Now).ToString();
            string cv = ("OdinSam"+dt).StringToMd5ToLower();
            var cookieInfo = new CookieInfo{ CookieValue = cv, CreateTime = dt };
            return new ResultData { Status = true, Data = new { cookie = cookieInfo } };
        }


        /*
        接口路径: apiblog/api1.0/BlogManager/Authen
        请求方式: Post
        输入参数: 无
        Headers中包含 cookie信息
        {
            ct:unixTimer,   //cookie创建时间 unix时间戳
            cv:cookieValue  //cookie的值
        }
        接口返回: Json格式
        {   
            Status = true,  //判断失败返回false
            Data = "ok"     //失败返回错误信息
        }
        接口简介: 后台验证当前用户权限接口
        */
        
        [EnableCors("AllowSpecificOrigin")]
        [Route("Authen")]
        [HttpPost]
        public ResultData AuthenCurrentUser()
        {
            return Authen();
        }

        
        /*
        接口路径: apiblog/api1.0/BlogManager/ChangeTitle
        请求方式: Post
        输入参数: Json格式
        {
            title:"change title"
        }
        接口返回: Json格式
        {
            Status = true,  //判断失败返回false
            Data = "ok"     //失败返回错误信息
        }
        接口简介: 
        */
        
        [EnableCors("AllowSpecificOrigin")]
        [Route("ChangeTitle")]
        [HttpPost]
        public ResultData BlogChangeTitle()
        {
            //验证身份
            var result = Authen();
            if(!result.Status)
                return result;
            else
            {   
                string strParam = this.RouteData.Values["paramStr"].ToString();
                System.Console.WriteLine(strParam);
                var title = JObject.Parse(strParam).GetValue("title").ToString();
                var model = db.BlogConfigs.SingleOrDefault();
                model.BlogTitle = title;
                db.Entry<BlogConfig_DbModel>(model).State = EntityState.Modified;
                db.SaveChanges();
                return new ResultData { Status = true, Data = new { Message = "ok" } };
            }
        }

        private ResultData Authen()
        {
            string cv = HttpContext.Request.Headers["cv"];
            string ct = HttpContext.Request.Headers["ct"];
            if(string.IsNullOrWhiteSpace(cv) || string.IsNullOrWhiteSpace(ct))
                return new ResultData { Status = false, Data = "身份认证不正确 - 0x000003" };
            if(("OdinSam"+ct).StringToMd5ToLower() != cv)
                return new ResultData { Status = false, Data = "身份认证不正确 - 0x000004" };
            else
                return new ResultData { Status = true, Data = new { Message = "ok" } };
        }
    }
}