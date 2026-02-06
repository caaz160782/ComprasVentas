using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ComprasVentas.Common;

public class ErrorResponse
{
    public int StatusCode { get; set; }
    public string traceId { get; set; } = string.Empty;
    public string Message { get; set; }=string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string Path { get; set; } = string.Empty;   
    public List<string> Errors {get; set;}=new List<string>();
}
