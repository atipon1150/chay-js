using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Myapi.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        // Navigation property
        public List<User> Users { get; set; } = new List<User>();
    }
}
