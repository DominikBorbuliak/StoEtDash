using Microsoft.EntityFrameworkCore;

namespace StoEtDash.Web.Database.Models
{
    public class StoEtDashContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite(@"Data Source=./Database/StoEtDash.db");
    }
}
