using Microsoft.EntityFrameworkCore;  // สำหรับ DbContext, DbSet
using Myapi.Models;                    // สำหรับ Department, User

namespace Myapi.Data
{
    // AppDbContext → เป็นคลาสหลักที่ EF Core ใช้ในการเชื่อมต่อและจัดการฐานข้อมูล
    public class AppDbContext : DbContext
    {
        // Constructor รับ options สำหรับ config database เช่น connection string
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        // DbSet<Department> → แทนตาราง Departments ในฐานข้อมูล
        // ใช้สำหรับ query, insert, update, delete
        public DbSet<Department> Departments { get; set; }

        // DbSet<User> → แทนตาราง Users ในฐานข้อมูล
        public DbSet<User> Users { get; set; }
    }
}
