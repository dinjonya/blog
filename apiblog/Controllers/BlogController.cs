using System;
using System.Collections.Generic;
using System.Linq;
using apiblog.Models;
using BlogModels.MongoKv;
using BlogModels.UiModel;
using CorePlugs20.Models;
using CorePlugs20.OdinMongo;
using CorePlugs20.TimeHelper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace apiblog.Controllers
{
    [Route("apiblog/api1.0/[controller]")]
    public class BlogController : Controller
    {
        private static MongoHelper mongo = new MongoHelper(Program.Config.MongoConfig.ConnectionString,Program.Config.MongoConfig.DataBase);
        private string cvalue = Program.Config.MongoConfig[MongoCollectionEnum.BLOGDATACOLLECTION].CollectionNameValue;
        private IHostingEnvironment hostingEnv;
        BlogEntities db;
        public BlogController(BlogEntities _db,IHostingEnvironment env)
        {
            db = _db;
            hostingEnv = env;
        }

        /*
        接口简介: 获取blog title 接口
        接口路径: apiblog/api1.0/Blog/GetBlogTitle
        请求方式: Get
        输入参数: 无
        接口返回: Json格式
        {
            statue = true,   //业务逻辑是否成功 失败返回false
            Data = "BlogTitle"     // 成功返回blogtitle 失败返回错误信息
        }
        */
        [EnableCors("AllowSpecificOrigin")]
        [Route("GetBlogTitle")]
        [HttpGet]
        public ResultData GetBlogTitle()
        {
            var filter = Builders<BsonDocument>.Filter.Eq("Key", "Blog");
            var mongoResult = mongo.SelectBsonModel(cvalue,filter);
            if(mongoResult == null)
            {
                var dbModel = db.BlogConfigs.SingleOrDefault();
                if(dbModel == null)
                    return new ResultData{ Status = false,Data="BlogConfig查询不存在，页面无法显示,0xGetBlogTitle01" };
                BlogConfig_Model configModel = new BlogConfig_Model
                {
                    BlogTitle = dbModel.BlogTitle,
                    BlogAboutMe = dbModel.AboutMe
                };
                mongo.RemoveModel(cvalue,filter);
                mongo.AddModel(cvalue,configModel);
                return new ResultData{ Status = true,Data=configModel.BlogTitle };
            }
            else
            {
                BlogConfig_Model configModel = mongo.ConvertMongoObjectToObject<BlogConfig_Model>(mongoResult,new List<string>{"_id"});
                return new ResultData{ Status = true,Data=configModel.BlogTitle };
            }
        }


        /*
        接口简介: 获取当前页面Pv 以及 网站 Uv 的接口
        接口路径: apiblog/api1.0/Blog/GetPvUv/{VisitPage}
        请求方式: Get
        输入参数: VisitPage 访问页面名称
        接口返回: Json格式
        {
            statue = true,   //业务逻辑是否成功 失败返回false
            Data = new { uv = uv,pv = pv }     // 成功返回pv和uv的信息 失败返回错误信息
        }
        */
        [EnableCors("AllowSpecificOrigin")]
        [Route("GetPvUv/{VisitPage}")]
        [HttpGet]
        public ResultData GetPvUv(string VisitPage)
        {
            var pvModel =  db.PV.Where(pView=>pView.VisitPage==VisitPage).Single();
            int pv = pvModel.VisitCount;
            int uv = db.UV.Where(uView=>uView.VisitPath==VisitPage).Count();
            return new ResultData{ Status = true,Data=new { uv = uv,pv = pv } };
        }


        /*
        接口简介: 获取Index页面的Post文章的接口，带分页
        接口路径: apiblog/api1.0/Blog/GetAllPosts
        请求方式: Get
        输入参数: PageIndex 当前页数  起始从 1 开始
        接口返回: Json格式
        {
            statue = true,   //业务逻辑是否成功 失败返回false
            Data=new { PageCount = pageCount,Posts = List<IndexPost_Model> }     // 成功返回Post展示信息 失败返回错误信息
        }
        */
        [Route("GetAllPosts")]
        [HttpGet]
        public ResultData GetAllPosts()
        {
            var dbModels = (from post in db.Posts 
                            orderby post.PostTime descending
                            select new IndexPost_Model
                            {
                                Id = post.Id,
                                PostTitle = post.PostTitle,
                                PostDesc = post.PostDescription,
                                PostTime = post.PostTime,
                            }).ToList();
            Index_Model model = new Index_Model{ PageCount = -1, Posts = dbModels };
            
            return new ResultData{ Status = true,Data = model };
        }

        /*
        接口简介: 获取Index页面的Post文章的接口，带分页
        接口路径: apiblog/api1.0/Blog/GetPosts
        请求方式: Get
        输入参数: PageIndex 当前页数  起始从 1 开始
        接口返回: Json格式
        {
            statue = true,   //业务逻辑是否成功 失败返回false
            Data=new { PageCount = pageCount,Posts = List<IndexPost_Model> }     // 成功返回Post展示信息 失败返回错误信息
        }
        */
        [Route("GetPosts/{PageIndex}")]
        [HttpGet]
        public ResultData GetPosts(int PageIndex)
        {
            int pageSize = Program.Config.PageSize;
            var dbResult =  db.Posts;
            int pageCount = dbResult.Count();
            pageCount = pageCount%pageSize==0? pageCount/pageSize : pageCount/pageSize+1;
            var dbModels = (from post in dbResult 
                            join c in db.PostCategories
                            on post.PostCategoryId equals c.Id
                            select new IndexPost_Model
                            {
                                Id = post.Id,
                                PostTitle = post.PostTitle,
                                PostDesc = post.PostDescription,
                                PostTime = post.PostTime,
                                PostCategoryId = c.Id,
                                PostCategory = c.CategoryName
                            }).Skip((PageIndex-1)*pageSize).Take(pageSize).ToList();
            Index_Model model = new Index_Model{ PageCount = pageCount, Posts = dbModels };
            return new ResultData{ Status = true,Data = model };
        }



        /*
        接口简介: 获取Index页面的Post文章的接口，带分页
        接口路径: apiblog/api1.0/Blog/GetPostsByCategory/{categoryId}
        请求方式: Get
        输入参数: 
            categoryId 类别编号 
            PageIndex 当前页数  起始从 1 开始
        接口返回: Json格式
        {
            statue = true,   //业务逻辑是否成功 失败返回false
            Data=new { PageCount = pageCount,Posts = List<IndexPost_Model> }     // 成功返回Post展示信息 失败返回错误信息
        }
        */
        [Route("GetPostsByCategory/{categoryId}/{PageIndex?}")]
        [HttpGet]
        public ResultData GetPostsByCategory(int categoryId,int? PageIndex)
        {
            var pageIndex = PageIndex==null ? 1 : Convert.ToInt32(PageIndex);
            int pageSize = Program.Config.PageSize;
            var dbResult =  db.Posts.AsEnumerable();
            dbResult = dbResult.Where(p=>p.PostCategoryId == categoryId);
            int pageCount = dbResult.Count();
            pageCount = pageCount%pageSize==0? pageCount/pageSize : pageCount/pageSize+1;
            var category = db.PostCategories.Where(c=>c.Id == categoryId).SingleOrDefault();
            var cid = category!=null ? category.Id:-1;
            var cname = category!=null ? category.CategoryName:"";
            var dbModels = (from post in dbResult 
                            where post.PostCategoryId == cid
                            orderby post.PostTime descending
                            select new CategoryPost_Model
                            {
                                Id = post.Id,
                                PostTitle = post.PostTitle,
                                PostDesc = post.PostDescription,
                                PostTime = post.PostTime,
                                PostCategoryId = cid,
                                PostCategory = cname
                            }).Skip((pageIndex-1)*pageSize).Take(pageSize).ToList();
            CategoryPosts_Model model = new CategoryPosts_Model{ PageCount = pageCount, Posts = dbModels,CategoryId = cid, CategoryName = cname };
            return new ResultData{ Status = true,Data = model };
        }




        /*
        接口简介: 获取Blog所有类别的接口
        接口路径: apiblog/api1.0/Blog/GetCategories
        请求方式: Get
        输入参数: PageIndex 当前页数  起始从 1 开始
        接口返回: Json格式
        {
            statue = true,   //业务逻辑是否成功 失败返回false
            Data=new { PageCount = pageCount,Posts = List<CategoryModel> }     // 成功返回Post展示信息 失败返回错误信息
        }
        */
        [Route("GetCategories/{PageIndex?}")]
        [HttpGet]
        public ResultData GetCategories(int? PageIndex)
        {
            int pageIndex = PageIndex==null?1:Convert.ToInt32(PageIndex);
            int pageSize = Program.Config.PageSize;
            var dbResult =  db.PostCategories;
            int pageCount = dbResult.Count();
            pageCount = pageCount%pageSize==0? pageCount/pageSize : pageCount/pageSize+1;
            var dbModels = (from c in dbResult 
                            join p in db.Posts
                            on c.Id equals p.PostCategoryId
                            group c by new {Id=c.Id,CategoryName=c.CategoryName} into g
                            select new CategoryModel
                            {
                                CategoryId = g.Key.Id,
                                CategoryName = g.Key.CategoryName,
                                PostNum = g.Count()
                            }).OrderBy(c=>c.PostNum).Skip((pageIndex-1)*pageSize).Take(pageSize).ToList();
            Category_Model model = new Category_Model{ PageCount = pageCount, Categories = dbModels };
            return new ResultData{ Status = true,Data = model };
        }


        /*
        接口简介: 获取Tag信息
        接口路径: apiblog/api1.0/Blog/GetCategories
        请求方式: Get
        输入参数: PageIndex 当前页数  起始从 1 开始
        接口返回: Json格式
        {
            statue = true,   //业务逻辑是否成功 失败返回false
            Data=new { PageCount = pageCount,Posts = List<CategoryModel> }     // 成功返回Post展示信息 失败返回错误信息
        }
        */
        [Route("GetTags")]
        [HttpGet]
        public ResultData GetTags()
        {
            
            var dbResult =  db.Tags.Select(t=>new TagModel
                                {
                                    Id = t.Id,
                                    TagName = t.TagName,
                                    PostNum = t.PostNum
                                }).ToList();
            Tag_Model model = new Tag_Model{ Tags = dbResult };
            return new ResultData{ Status = true,Data = model };
        }



        /*
        接口简介: 按照Tag获取对应post信息
        接口路径: apiblog/api1.0/Blog/GetPostByTag/{TagId}
        请求方式: Get
        输入参数: PageIndex 当前页数  起始从 1 开始
        接口返回: Json格式
        {
            statue = true,   //业务逻辑是否成功 失败返回false
            Data=new { PageCount = pageCount,Posts = List<CategoryModel> }     // 成功返回Post展示信息 失败返回错误信息
        }
        */
        [Route("GetPostByTag/{TagId}/{PageIndex?}")]
        [HttpGet]
        public ResultData GetPostByTag(int TagId,int? PageIndex)
        {
            int pageIndex = PageIndex==null?1:Convert.ToInt32(PageIndex);
            string tagId = TagId.ToString();
            int pageSize = Program.Config.PageSize;
            var tags = db.Tags.ToList();
            var tag = tags.Where(t=>t.Id == TagId).SingleOrDefault();
            var tid = tag!=null ? tag.Id:-1;
            var tname = tag!=null ? tag.TagName:"";
            var dbResult =  db.Posts.Where(p=>p.Tags.Contains(tagId));
            int pageCount = dbResult.Count();
            pageCount = pageCount%pageSize==0? pageCount/pageSize : pageCount/pageSize+1;
            var dbModels = (from post in dbResult 
                            join c in db.PostCategories
                            on post.PostCategoryId equals c.Id
                            orderby post.PostTime descending
                            select new TagPost_Model
                            {
                                Id = post.Id,
                                PostTitle = post.PostTitle,
                                PostDesc = post.PostDescription,
                                PostTime = post.PostTime,
                                PostCategoryId = c.Id,
                                PostCategory = c.CategoryName,
                                Tags = tags.Where(t=>post.Tags.Contains(t.Id.ToString())).Select(t=>new TagModel{ Id = t.Id, TagName = t.TagName }).ToList(),
                            }).Skip((pageIndex-1)*pageSize).Take(pageSize).ToList();
            TagPosts_Model model = new TagPosts_Model{ PageCount = pageCount, Posts = dbModels, TagId = tid, TagName = tname };
            return new ResultData{ Status = true,Data = model };
        }

        /*
        接口简介: 获取Rss所需要的文章信息 top20
        接口路径: apiblog/api1.0/Blog/GetPostByRss
        请求方式: Get
        输入参数: Json格式
            无
        接口返回: Json格式
        {
            statue = true,   //业务逻辑是否成功 失败返回false
            Data=new { PageCount = 1,Posts = List<CategoryModel> }     // 成功返回Post展示信息 失败返回错误信息
        }
        */
        
        [Route("GetPostByRss")]
        [HttpGet]
        public ResultData GetPostByRss()
        {
            int pageSize = Program.Config.PageSize;
            var dbModels = (from post in db.Posts 
                            join c in db.PostCategories
                            on post.PostCategoryId equals c.Id
                            orderby post.PostTime descending
                            select new IndexPost_Model
                            {
                                Id = post.Id,
                                PostTitle = post.PostTitle,
                                PostDesc = post.PostDescription,
                                PostContent = post.PostContent,
                                PostTime = post.PostTime,
                                PostCategoryId = c.Id,
                                PostCategory = c.CategoryName
                            }).Take(pageSize).ToList();
            Index_Model model = new Index_Model{ PageCount = 1, Posts = dbModels };
            return new ResultData{ Status = true,Data = model };
        }


        /*
        接口简介: 获取About me 数据
        接口路径: apiblog/api1.0/Blog/GetAboutMe
        请求方式: Get
        输入参数: Json格式
            无
        接口返回: Json格式
        {
            statue = true,   //业务逻辑是否成功 失败返回false
            Data=new { About = AboutMe_Model }     // 成功返回AboutMe_Model展示信息 失败返回错误信息
        }
        */
        
        [Route("GetAboutMe")]
        [HttpGet]
        public ResultData GetAboutMe()
        {
            var result = db.BlogConfigs.SingleOrDefault();
            if(result == null)
                return new ResultData{ Status = false,Data="BlogConfig查询不存在，找不到About Me信息,0xGetAboutMe01" };
            string aboutme = result.AboutMe;
            return new ResultData{ Status = true,Data = new AboutMe_Model{ Me = aboutme } };

        }

        /*
        接口简介: 按照文章Id获取文章信息，以及获取上一篇文章与下一篇的title和Id
        接口路径: apiblog/api1.0/Blog/GetPostArticle
        请求方式: Get
        输入参数: postId 当前文章Id
        接口返回: Json格式
        {
            statue = true,   //业务逻辑是否成功 失败返回false
            Data=new { Article = Article_Model }     // 成功返回AboutMe_Model展示信息 失败返回错误信息
        }
        */
        
        [Route("GetPostArticle/{postId}")]
        [HttpGet]
        public ResultData GetPostArticle(int postId)
        {
            var dbResult =  db.Posts.Where(p=>p.Id==postId);
            if(dbResult.Count()!=1)
                return new ResultData{ Status = false,Data = "文章查找不正确" };
            long pt = Convert.ToInt64(dbResult.Single().PostTime);
            Article_Model dbModels = (from post in dbResult
                            join c in db.PostCategories
                            on post.PostCategoryId equals c.Id
                            where post.Id == postId
                            orderby post.PostTime descending
                            select new Article_Model
                            {
                                Id = post.Id,
                                PostTitle = post.PostTitle,
                                PostPageKeywords = post.PostPageKeywords,
                                PostDescription = post.PostPageDescription,
                                PostContent = post.PostContent,
                                PostTime = UnixTimeHelper.FromUnixTime(pt),
                                PostCategoryId = c.Id,
                                PostCategoryName = c.CategoryName
                            }).Single();
            dbModels.Tags=new List<TagModel>();
            foreach (var tag in dbResult.Single().Tags.Split(',').ToList())
            {
                int tt = Convert.ToInt32(tag);
                dbModels.Tags.Add(new TagModel { Id = tt, TagName = db.Tags.Where(t=>t.Id==tt).Single().TagName });
            }

            var provious = db.Posts.Where(p=>p.Id<dbResult.Single().Id).FirstOrDefault();
            if(provious!=null)
                dbModels.PreviousPost = new NeighborPost { Id = provious.Id, PostTitle = provious.PostTitle };
            else
                dbModels.PreviousPost = null;

            var next = db.Posts.Where(p=>p.Id>dbResult.Single().Id).FirstOrDefault();
            if(next!=null)
                dbModels.NextPost = new NeighborPost { Id = next.Id, PostTitle = next.PostTitle };
            else
                dbModels.NextPost = null;
            return new ResultData{ Status = true,Data = dbModels };
        }
    }
}