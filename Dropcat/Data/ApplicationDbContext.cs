using Dropcat.Models;
using Microsoft.EntityFrameworkCore;

namespace Dropcat.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<UserInfo> UserInfo { get; set; }
    }

}
