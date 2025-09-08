using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Myapi.Models;

namespace Myapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public DepartmentsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Departments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            return await _context.Departments
                                 .Include(d => d.Users) // ดึง Users ด้วย
                                 .ToListAsync();
        }

        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var dept = await _context.Departments
                                     .Include(d => d.Users)
                                     .FirstOrDefaultAsync(d => d.Id == id);

            if (dept == null)
                return NotFound();

            return dept;
        }

        // POST: api/Departments
        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartment(Department dept)
        {
            if (dept == null)
                return BadRequest();

            // เพิ่ม Department
            _context.Departments.Add(dept);
            await _context.SaveChangesAsync(); // dept.Id ถูกสร้าง

            // เพิ่ม Users
            if (dept.Users != null)
            {
                foreach (var user in dept.Users)
                {
                    user.DepartmentId = dept.Id;
                    _context.Users.Add(user);
                }
                await _context.SaveChangesAsync();
            }

            return Ok(dept);
        }

        // PUT: api/Departments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, Department dept)
        {
            if (id != dept.Id)
                return BadRequest();

            var existingDept = await _context.Departments
                                             .Include(d => d.Users)
                                             .FirstOrDefaultAsync(d => d.Id == id);

            if (existingDept == null)
                return NotFound();

            existingDept.Name = dept.Name;

            // อัปเดต Users (ง่ายที่สุด: ลบแล้วเพิ่มใหม่)
            if (dept.Users != null)
            {
                // ลบ Users เก่า
                _context.Users.RemoveRange(existingDept.Users);

                // เพิ่ม Users ใหม่
                foreach (var user in dept.Users)
                {
                    user.DepartmentId = id;
                    _context.Users.Add(user);
                }
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var dept = await _context.Departments
                                     .Include(d => d.Users)
                                     .FirstOrDefaultAsync(d => d.Id == id);
            if (dept == null)
                return NotFound();

            // ลบ Users ก่อน
            if (dept.Users != null && dept.Users.Count > 0)
            {
                _context.Users.RemoveRange(dept.Users);
            }

            // ลบ Department
            _context.Departments.Remove(dept);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
