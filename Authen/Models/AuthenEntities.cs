using Microsoft.EntityFrameworkCore;

namespace Authen.Models
{
    public class AuthenEntities : DbContext
    {
        public AuthenEntities(DbContextOptions<AuthenEntities> options) : base(options)
        {
            
        }

        public DbSet<InvokeRecord_DbModel> InvokeRecords { get; set; }

        public DbSet<AuthenVisitsRecord_DbModel> AuthenVisitsRecords { get; set; }

        public DbSet<InvokerAuthen_DbModel> InvokerAuthens { get; set; }

    }
}