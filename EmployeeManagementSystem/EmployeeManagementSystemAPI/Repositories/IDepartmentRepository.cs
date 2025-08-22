﻿using EmployeeManagementSystemAPI.Models;

namespace EmployeeManagementSystemAPI.Repositories
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllAsync();
        Task<Department?> GetByIdAsync(int id);
        Task<Department> AddAsync(Department department);
        Task<Department?> UpdateAsync(Department department);
        Task<bool> DeleteAsync(int id);
    }
}
