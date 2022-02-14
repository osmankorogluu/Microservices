using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Course.Shared.Dtos
{
    public class Response<T>
    {
        public T Data { get; private set; }
        [BsonIgnore]
        public int StatusCode { get; private set; }
        [BsonIgnore]
        public bool IsSuccessful { get; private set; }
        public List<string> Errors { get; private set; }


        public static Response<T> Success(T data, int statusCode)
        {
            return new Response<T> { Data = data, StatusCode = statusCode, IsSuccessful = true };
        }

        public static Response<T> Success(int statusCode)
        {
            return new Response<T> { Data = default(T), StatusCode = statusCode, IsSuccessful = true };
        }

        public static Response<T> Fail(List<string> erros, int statusCode)
        {
            return new Response<T>
            {
                Errors = erros,
                StatusCode = statusCode,
                IsSuccessful = false,
            };
        }

        public static Response<T> Fail(string error, int statusCode)
        {
            return new Response<T> { Errors = new List<string>() { error }, StatusCode = statusCode, IsSuccessful = false };
        }
    }
}
