using System.Net;

namespace SocialMedia.API.Response
{
    public class ApiResponse<T>
    {
        public ApiResponse() 
        {
            Message = string.Empty;
        }

        public T? Data { get; set; } = default;
        public string Message { get; set; }
        public HttpStatusCode Status { get; set; }

        public static ApiResponse<T> Success(T data, string message = "")
        {
            return new ApiResponse<T>
            {
                Data = data,
                Message = message,
                Status = HttpStatusCode.OK
            };
        }

        public static ApiResponse<T> Failure(T data, string message = "")
        {
            return new ApiResponse<T>
            {
                Data = data,
                Message = message,
                Status = HttpStatusCode.BadRequest
            };
        }
    }
}
