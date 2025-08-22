using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystemAPI.Models
{
    public class Department
    {
        public int Id { get; set; } 

        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
