using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
namespace apiblog.Models
{
    public class BlogEntities : DbContext
    {
        public BlogEntities(DbContextOptions<BlogEntities> options) : base(options)
        {
            
        }
        public DbSet<Post_DbModel> Posts { get; set; }

        public DbSet<PostReply_DbModel> PostReplys { get; set; }

        public DbSet<Tag_DbModel> Tags { get; set; }

        public DbSet<BlogConfig_DbModel> BlogConfigs { get; set; }
        
    }
}