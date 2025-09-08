
using Microsoft.EntityFrameworkCore;
using Myapi.Models;

namespace Myapi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
