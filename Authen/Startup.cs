using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authen.Models;
using BlogModels.MongoKv;
using CorePlugs20.ApiFilter;
using CorePlugs20.OdinMongo;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Authen
{
    public class Startup
    {
        MongoHelper mongo = new MongoHelper(Program.Config.MongoConfig.ConnectionString,Program.Config.MongoConfig.DataBase);
        string cvalue = Program.Config.MongoConfig[MongoCollectionEnum.BLOGCONFIGCOLLECTION].CollectionNameValue;
        string authenVvalue = Program.Config.MongoConfig[MongoCollectionEnum.AUTHENCONFIGCOLLECTION].CollectionNameValue;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            
            services.AddDbContext<AuthenEntities>(options=>options.UseMySql(Program.Config.AuthenServer.ConnectionString));
            
            services.AddMvc(options => options.Filters.Add(new ApiFilterAttribute(Program.AuthenInfo){ Order = 2 }));

            services.AddMvc(options => options.Filters.Add(new ApiAuthenFilterAttribute(
                                Program.Config,
                                MongoCollectionEnum.AUTHENCONFIGCOLLECTION,
                                Program.AuthenInfo){ Order = 1 }));
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,AuthenEntities authenEntities)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            
            DataBaseInit(authenEntities);

            MongoInit(authenEntities);

            app.UseMvc();
        }

        private void MongoInit(AuthenEntities authenEntities)
        {
            mongo.ClearCollection(cvalue);
            mongo.AddModel(cvalue,Program.Config);

            mongo.ClearCollection(authenVvalue);
            mongo.AddModels(authenVvalue,authenEntities.InvokerAuthens.ToList());

        }

        private void DataBaseInit(AuthenEntities authenEntities)
        {
            //创建数据库
            var flag = authenEntities.Database.EnsureCreated();
            if(flag)
            {
                SampleData.Init(authenEntities);
            }
        }
    }
}
