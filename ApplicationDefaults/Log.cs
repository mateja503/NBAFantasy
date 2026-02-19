

using System.Text.Json;

namespace ApplicationDefaults
{
    public record Log
    {
        public string? message { get; init; } = string.Empty;
        public object? request { get; init; } = null;
        public object? response { get; init; } = null;  
        public Log(string message, object? request = null, object? response = null) 
        {
            this.message = message;
            this.request = request;
            this.response = response;
        }
        public string ToLogString() 
        {
            return $"Message: {message}, Request: {JsonSerializer.Serialize(request)}, Response: {JsonSerializer.Serialize(response)}";
        }
    }
}
