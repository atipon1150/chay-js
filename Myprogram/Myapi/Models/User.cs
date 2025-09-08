using System.ComponentModel.DataAnnotations;       // สำหรับ [Key], [Required]
using System.ComponentModel.DataAnnotations.Schema; // สำหรับ [ForeignKey]

namespace Myapi.Models
{
    // คลาส User แทนตาราง Users ในฐานข้อมูล
    public class User
    {
        [Key]  // ระบุว่า Id เป็น primary key ของตาราง
        public int Id { get; set; }  // รหัสผู้ใช้ (auto increment โดย EF Core)

        [Required]  // FullName ต้องมีค่า ไม่สามารถเป็น null
        public string FullName { get; set; }  // ชื่อผู้ใช้

        // Foreign key → เชื่อมไปยัง Department
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }  // รหัสแผนกที่ผู้ใช้นี้สังกัด

        // Navigation property → ชี้ไปยัง Department ของผู้ใช้
        // Nullable เพราะเวลา POST user ใหม่ เราไม่ต้องส่ง Department object ทั้งหมด
        public Department? Department { get; set; } = null;
        /*
          - EF Core จะใช้ property นี้ในการ join กับ Department table
          - ตั้งค่าเริ่มต้นเป็น null เพื่อป้องกัน error
          - เวลาเพิ่ม user เราจะใช้ DepartmentId แทนการใส่ object Department
        */
    }
}
