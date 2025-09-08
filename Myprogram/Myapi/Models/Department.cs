
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Myapi.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
    }
}
