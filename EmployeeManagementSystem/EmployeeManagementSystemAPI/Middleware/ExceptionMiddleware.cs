using EmployeeManagementSystemAPI.Utils;
using System.Text.Json;

namespace EmployeeManagementSystemAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _logFolder;
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1); // to prevent race condition

        public ExceptionMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            string appDataFolder = Path.Combine(env.ContentRootPath, Helper.APP_DATA);
            _logFolder = Path.Combine(appDataFolder, Helper.LOGS_FOLDER);
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var errorResponse = new
            {
                message = ex.Message,
                path = context.Request.Path,
                timestamp = DateTime.UtcNow
            };

            await WriteLogAsync(errorResponse);

            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }

        private async Task WriteLogAsync(object errorResponse)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string logsDir = Path.Combine(baseDir, _logFolder);

            if (!Directory.Exists(logsDir))
                Directory.CreateDirectory(logsDir);

            string logFile = Path.Combine(logsDir, $"log-{DateTime.UtcNow:yyyy-MM-dd}.json");

            string logEntry = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
            {
                WriteIndented = true
            }) + Environment.NewLine;

            // Ensure only one write at a time
            await _semaphore.WaitAsync();
            try
            {
                await File.AppendAllTextAsync(logFile, logEntry);
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
