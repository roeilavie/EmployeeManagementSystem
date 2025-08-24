using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystemAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Hire date is required")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(Employee), nameof(ValidateHireDate))]
        public DateTime HireDate { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Salary must be greater than zero")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "DepartmentId is required")]
        public int DepartmentId { get; set; }

        // Custom validation method
        public static ValidationResult? ValidateHireDate(DateTime hireDate, ValidationContext context)
        {
            if (hireDate > DateTime.UtcNow.Date)
                return new ValidationResult("Hire date cannot be in the future");

            return ValidationResult.Success;
        }
    }
}
