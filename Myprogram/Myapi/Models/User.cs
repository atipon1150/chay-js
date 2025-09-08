using System.ComponentModel.DataAnnotations.Schema;

namespace Myapi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        public Department? Department { get; set; }  // nullable, ไม่ต้องส่งตอน POST
    }
}
