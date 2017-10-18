namespace apiblog.Models
{
    public class SampleData
    {
        public static void Init(BlogEntities blogEntities)
        {
            BlogConfig_DbModel blogModel = new BlogConfig_DbModel
            {
                BlogOwner = "dinjonya",
                Salt = "jAxNzA5MjAxMDQ0NTM=",
                OwnerPwd = "a0ea836ecf0544887c74f9eab8069cdc",
                BlogTitle = "OdinSam的博客",
                AboutMe = "",
                PostAutograph = ""
            };
            blogEntities.BlogConfigs.Add(blogModel);

            
            blogEntities.SaveChanges();
        }
    }
}