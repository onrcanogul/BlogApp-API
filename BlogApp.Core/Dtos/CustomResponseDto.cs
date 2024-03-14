using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Core.Dtos
{
    public class CustomResponseDto<T>
    {
        public T Data { get; set; }
        public ErrorDto? Errors { get; set; }
        public int StatusCode { get; set; }

        public static CustomResponseDto<T> Success(int statusCode, T data)
        {
            return new CustomResponseDto<T>() { Data = data, StatusCode = statusCode };
        }
        public static CustomResponseDto<T> Success(int statusCode)
        {
            return new CustomResponseDto<T>() {StatusCode = statusCode };
        }


        public static CustomResponseDto<T> Fail(int statusCode, ErrorDto Errors)
        {
            return new CustomResponseDto<T>() { StatusCode = statusCode, Errors = Errors };
        }

        public static CustomResponseDto<T> Fail(int statusCode, string error ,bool isShow)
        {
            return new CustomResponseDto<T>() { StatusCode = statusCode, Errors = new ErrorDto(error, isShow) };
        }

    }
}
