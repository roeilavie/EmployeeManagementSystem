using EmployeeManagementSystemAPI.Models;
using EmployeeManagementSystemAPI.Utils;
using System.Text.Json;

namespace EmployeeManagementSystemAPI.Repositories
{
    public class JsonDataRepository : IJsonDataRepository
    {
        private readonly string _filePath;
        private readonly SemaphoreSlim _lock = new(1, 1); // to prevent race condition

        public JsonDataRepository(IWebHostEnvironment env)
        {
            _filePath = Path.Combine(env.ContentRootPath, Helper.APP_DATA, Helper.EMPLOYEE_DATA_FILE);

            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, JsonSerializer.Serialize(new JsonData(), new JsonSerializerOptions { WriteIndented = true }));
            }
        }

        public async Task<JsonData> GetDataAsync()
        {
            await _lock.WaitAsync();
            try
            {
                if (!File.Exists(_filePath))
                    return new JsonData();

                var json = await File.ReadAllTextAsync(_filePath);
                return JsonSerializer.Deserialize<JsonData>(json) ?? new JsonData();
            }
            catch (IOException ex)
            {
                throw new Exception("Error reading data from storage.", ex);
            }
            finally
            {
                _lock.Release();
            }
        }

        public async Task SaveDataAsync(JsonData data)
        {
            await _lock.WaitAsync();
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);

                var json = JsonSerializer.Serialize(data, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                await File.WriteAllTextAsync(_filePath, json);
            }
            catch (IOException ex)
            {
                throw new Exception("Error writing data to storage.", ex);
            }
            finally
            {
                _lock.Release();
            }
        }
    }
}
