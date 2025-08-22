namespace EmployeeManagementSystemAPI.Models
{
    public class JsonData
    {
        public List<Department> Departments { get; set; } = new List<Department>();
        public List<Employee> Employees { get; set; } = new List<Employee>();
    }
}
