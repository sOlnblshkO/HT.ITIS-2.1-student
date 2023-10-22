using Hw7.Enums;

namespace Hw7.Response;

public class Response<T>
{
    public ResultStatus Status { get; set; }
    
    public T Data { get; set; }
}