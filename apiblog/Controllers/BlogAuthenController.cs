using System;
using System.Linq;
using apiblog.Models;
using CorePlugs20.Models;
using CorePlugs20.OdinSecurity;
using CorePlugs20.TimeHelper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apiblog.Controllers
{
    [Route("apiblog/api1.0/[controller]")]
    public class BlogAuthenController : Controller
    {
        BlogEntities db;
        public BlogAuthenController(BlogEntities _db)
        {
            db = _db;
        }
        /*
        接口路径: apiblog/api1.0/blogauthen/puv/{pagePath}/{rnd}
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
        [EnableCors("AllowSpecificOrigin")]
        [Route("puv/{pagePath}/{rnd}")]
        [HttpGet]
        public ResultData SetWebPvUv(string pagePath,string rnd)
        {
            CookieInfo cookieInfo = null;
            if(HttpContext.Request.Headers.Keys.Contains("cv"))
            {
                //查找 PV对应记录     添加或更新 pv信息
                AddOrUpdatePv(pagePath);
                var cv = HttpContext.Request.Headers["cv"];
                var ct = HttpContext.Request.Headers["ct"];
                cookieInfo = new CookieInfo{ CookieValue = cv, CreateTime = ct };
            }
            else
            {
                //+pv
                AddOrUpdatePv(pagePath);
                //生成cookie
                string dt = UnixTimeHelper.FromDateTime(DateTime.Now).ToString();
                string cv = ("OdinSam"+dt).StringToMd5ToLower();
                cookieInfo = new CookieInfo{ CookieValue = cv, CreateTime = dt };
                //+uv
                db.UV.Add(new UserView_DbModel { CookieValue = cv, VisitTime = dt,VisitPath = pagePath });
                db.SaveChanges();
            }
            return new ResultData { Status = true, Data = new { cookie=cookieInfo } };
        }

        /// <summary>
        /// 添加或更新PV
        /// </summary>
        /// <param name="pagePath">页面路径</param>
        private void AddOrUpdatePv(string pagePath)
        {
             var model = db.PV.Where(pv=>pv.VisitPage == pagePath).SingleOrDefault();
            //页面pv不存在， 添加新记录  否则更新
            if(model == null )
            {
                model = new PageView_DbModel{ VisitPage = pagePath, VisitCount = 1 };
                db.PV.Add(model);
                db.Entry<PageView_DbModel>(model).State = EntityState.Added;
            }
            else
            {
                model.VisitCount += 1;
                db.Entry<PageView_DbModel>(model).State = EntityState.Modified;
            }
            db.SaveChanges();
        }
    }
}