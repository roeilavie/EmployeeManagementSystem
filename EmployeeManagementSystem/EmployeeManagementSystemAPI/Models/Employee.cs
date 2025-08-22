using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystemAPI.Models
{
    public class Employee
    {
        public int Id { get; set; } 

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public DateTime HireDate { get; set; }

        [Required]
        public float Salary { get; set; }

        [Required]
        public int DepartmentId { get; set; }
    }
}
