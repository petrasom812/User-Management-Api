using Microsoft.EntityFrameworkCore;
using UserSystem.Api.Models;

namespace UserSystem.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            
        }
        public DbSet<UserModel> Users {get; set;}
    }
}