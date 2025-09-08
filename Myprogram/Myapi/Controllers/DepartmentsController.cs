using Microsoft.AspNetCore.Mvc;  // สำหรับ ControllerBase, HttpGet, HttpPost ฯลฯ
using Microsoft.EntityFrameworkCore;  // สำหรับ Include(), ToListAsync()
using Myapi.Data;  // สำหรับ AppDbContext
using Myapi.Models;  // สำหรับ Department, User

namespace Myapi.Controllers
{
    // Route ของ API → api/Departments
    [Route("api/[controller]")]
    [ApiController]  // เพิ่ม validation และ binding อัตโนมัติ
    public class DepartmentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Constructor รับ AppDbContext ผ่าน dependency injection
        public DepartmentsController(AppDbContext context) => _context = context;

        // ===============================
        // GET: api/Departments
        // ===============================
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // ดึงข้อมูล Departments พร้อม Users ที่เกี่ยวข้อง (1:N)
            var departments = await _context.Departments
                                            .Include(d => d.Users)  // join Users table
                                            .ToListAsync();

            return Ok(departments); // ส่ง 200 + JSON
        }

        // ===============================
        // POST: api/Departments
        // ===============================
        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartment([FromBody] Department? dept)
        {
            // ถ้า client ไม่ส่ง dept มา → สร้าง default
            if (dept == null)
            {
                dept = new Department { Name = "Default Department" };
            }

            // ถ้า Name ว่าง → ตั้งชื่อ default
            if (string.IsNullOrWhiteSpace(dept.Name))
            {
                dept.Name = "Default Department";
            }

            // สร้าง Department ใหม่สำหรับ DB
            var newDept = new Department
            {
                Name = dept.Name
            };

            _context.Departments.Add(newDept);
            await _context.SaveChangesAsync();  // บันทึกลง DB → EF Core จะสร้าง Id ให้

            return Ok(newDept); // ส่ง 200 พร้อม Id
        }

        // ===============================
        // PUT: api/Departments/{id}
        // ===============================
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Department dept)
        {
            var existing = await _context.Departments.FindAsync(id);
            if (existing == null) return NotFound(); // ถ้าไม่มี → 404

            existing.Name = dept.Name;  // อัปเดตชื่อ
            await _context.SaveChangesAsync();  // บันทึกลง DB

            return Ok(existing);  // ส่ง 200 + object ที่อัปเดตแล้ว
        }

        // ===============================
        // DELETE: api/Departments/{id}
        // ===============================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _context.Departments.FindAsync(id);
            if (existing == null) return NotFound(); // ถ้าไม่มี → 404

            _context.Departments.Remove(existing);  // ลบ Department
            await _context.SaveChangesAsync();  // บันทึกลง DB

            return NoContent();  // ส่ง 204 → ไม่มี body
        }
    }
}
