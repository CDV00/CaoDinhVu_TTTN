using System.Collections.Generic;

namespace Entities.Responses
{
    public class BaseResponse
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; }
        public BaseResponse()
        {

        }
        public BaseResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public BaseResponse(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
    }
    public class Response<T> : BaseResponse
    {
        public T Data { get; set; }

        public Response(bool isSuccess, string message):base(isSuccess, message) { }
        public Response(bool isSuccess, string message, T data) :base(isSuccess, message) 
        {
            Data = data;
        }

    }
}
