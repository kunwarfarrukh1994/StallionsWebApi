using Microsoft.EntityFrameworkCore;
using WEBAPI.DAL.models;

namespace WEBAPI.DAL
{
    public class UserDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public UserDBContext(DbContextOptions<UserDBContext> options): base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
