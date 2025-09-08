using System.Collections.Generic;      // สำหรับใช้ List<T>
using System.ComponentModel.DataAnnotations;  // สำหรับ attributes เช่น [Key], [Required]

namespace Myapi.Models
{
    // คลาส Department แทนตาราง Departments ในฐานข้อมูล
    public class Department
    {
        [Key]  // กำหนดว่า property นี้เป็น primary key ของตาราง
        public int Id { get; set; }  // รหัสแผนก (auto increment โดย EF Core)

        [Required]  // กำหนดว่า Name ต้องมีค่า (not null)
        public string Name { get; set; }  // ชื่อแผนก

        // Navigation property
        // ใช้เพื่อเชื่อมความสัมพันธ์ 1:M กับ User
        // Department สามารถมี User ได้หลายคน
        public List<User> Users { get; set; } = new List<User>(); 
        /*
            - กำหนดค่าเริ่มต้นเป็น List<User> ว่าง
            - ป้องกัน null reference
            - EF Core จะใช้ property นี้ในการ load/associate User ที่อยู่ในแผนกนี้
        */
    }
}
