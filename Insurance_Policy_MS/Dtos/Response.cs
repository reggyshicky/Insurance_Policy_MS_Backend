using System.Net;

namespace Insurance_Policy_MS.Dtos
{
    public class Response<T>
    {
        public HttpStatusCode Status { get; set; }
        public string? Message { get; set; } = string.Empty;
        public T? Data { get; set; }
    }

    //coding C# 

    // i am coding C#, HOPE IT IS GOOD NTHKLTNE
}
