using EmployeeManagementSystemAPI.Models;

namespace EmployeeManagementSystemAPI.Repositories
{
    public interface IJsonDataRepository
    {
        Task<JsonData> GetDataAsync();
        Task SaveDataAsync(JsonData jsonData);
    }
}
