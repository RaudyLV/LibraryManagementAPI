﻿namespace Application.Wrrappers
{
    public class Response<T> 
    {
        public Response() { }

        public Response(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }

        public Response(string message, List<string> errors)
        {
            Succeeded = false;
            Message = message;
            Errors = errors;
            Data = default;
        }

        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }

    }
}
