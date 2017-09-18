using System.Collections.Generic;

namespace CorePlugs20.Models
{
    public class ConfigModel
    {
        public string EnvironmentName { get; set; }
        public AuthenServerModel AuthenServer { get; set; }
        public BlogUiModel BlogUi { get; set; }
        public ApiBlogModel ApiBlog { get; set; }

        public MongoConfigModel MongoConfig { get; set; }
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
    }
    
    public class ApiBlogModel 
    {
        public List<string> ApiBlogUrls { get; set; }
        public bool Debug { get; set; }

        public bool AutoCreateDb { get; set; }
        public string ConnectionString { get; set; }
        public bool Authen { get; set; }
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
    
}