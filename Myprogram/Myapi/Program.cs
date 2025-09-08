using Microsoft.EntityFrameworkCore;  // ใช้สำหรับ EF Core และเชื่อมต่อฐานข้อมูล
using Myapi.Data; // namespace ของ DbContext ของเรา (AppDbContext)

var builder = WebApplication.CreateBuilder(args); // สร้าง WebApplication builder สำหรับ config app

// ================================
// Add services (เพิ่ม service ที่แอปจะใช้)
// ================================

// PostgreSQL: เพิ่ม DbContext และเชื่อมต่อฐานข้อมูล PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Controllers: เพิ่ม Controller สำหรับ API endpoints
builder.Services.AddControllers();

// Swagger/OpenAPI: สำหรับสร้างเอกสาร API และ UI ทดสอบ
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS สำหรับ Angular frontend
// เพื่อให้ frontend (localhost:4200) สามารถเรียก API จาก backend ได้
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200") // กำหนด URL frontend
              .AllowAnyHeader()  // อนุญาตทุก header
              .AllowAnyMethod(); // อนุญาตทุก HTTP method (GET, POST, PUT, DELETE)
    });
});

var app = builder.Build(); // สร้าง WebApplication จริงจาก builder

// ================================
// Configure middleware (กำหนด pipeline ของ HTTP request)
// ================================

// Swagger: เปิดใช้งาน UI และ endpoint สำหรับทดสอบ API
app.UseSwagger();
app.UseSwaggerUI();

// หากต้องการ HTTPS redirect ใน dev ให้แน่ใจว่า frontend ใช้ https
// app.UseHttpsRedirection();

// CORS: ให้ backend อนุญาต request จาก frontend ตาม policy ที่กำหนด
app.UseCors(); // ✅ ต้องวางก่อน MapControllers()

// Authorization: หากมีระบบ Authentication/Authorization ใช้ middleware นี้
app.UseAuthorization();

// Map Controllers: ให้ endpoint ของ Controller ทำงานได้
app.MapControllers();

// Run app: เริ่ม server
app.Run();
