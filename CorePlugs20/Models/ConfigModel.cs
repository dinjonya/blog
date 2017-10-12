using System.Collections.Generic;

namespace CorePlugs20.Models
{
    public class ConfigModel
    {
        public string EnvironmentName { get; set; }
        public int PageSize { get; set; }
        public AuthenServerModel AuthenServer { get; set; }
        public BlogUiModel BlogUi { get; set; }
        public ApiBlogModel ApiBlog { get; set; }

        public MongoConfigModel MongoConfig { get; set; }
        public RssConfig Rss { get; set; }
    }

    public class AuthenServerModel
    {
        public bool AuthenEnable { get; set; }
        public List<string> AuthenServerUrls { get; set; }
        public NoAuthenIpConfig NoAuthenIp { get; set; }
        public string ConnectionString { get; set; }
    }
    public class NoAuthenIpConfig
    {
        public bool Enable { get; set; }
        public List<string> Ips { get; set; }
    }
    public class BlogUiModel 
    {
        public List<string> BlogUiUrls { get; set; }
        public bool Authen { get; set; }
        public string UploadPath { get; set; }
        public List<Apis_Config> Apis { get; set; }

        public Apis_Config this[string apiName]
        {
            get
            {
                foreach (var item in Apis)
                {
                    if(item.ApiName == apiName)
                        return item;
                }
                return null;
            }
        }
    }
    public class Apis_Config
    {
        public string ApiName { get; set; }
        public string Uri { get; set; }
        public string UriPath { get; set; }
    }
    
    public class ApiBlogModel 
    {
        public List<string> ApiBlogUrls { get; set; }
        public bool Debug { get; set; }

        public bool AutoCreateDb { get; set; }
        public string ConnectionString { get; set; }
        public bool Authen { get; set; }

        public List<string> AllowCorsUris { get; set; }
        
    }

    public class MongoConfigModel
    {
        public string DataBase { get; set; }
        public string ConnectionString { get; set; }
        public List<Collections_Config> Collections { get; set; }

        public Collections_Config this[string collectionName]
        {
            get
            {
                foreach (var collection in Collections)
                {
                    if (collection.CollectionName == collectionName)
                    {
                        return collection;
                    }
                }
                return null;
            }
        }
    }
    public partial class Collections_Config
    {
        /// <summary>
        /// Log集合名称
        /// </summary>
        public string CollectionName { get; set; }
        public string CollectionNameValue { get; set; }
    }
    
    public partial class RssConfig
    {
        public string RssTitle { get; set; }
        public string AlternateLink { get; set; }
        public string SelfLink { get; set; }
        public AtomTagConfig AtomTag { get; set; }
        public string SubTitle  { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
    }
    public partial class AtomTagConfig
    {
        public string Id { get; set; }
        public string Domain { get; set; }
    }
}