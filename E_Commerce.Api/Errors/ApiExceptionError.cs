﻿namespace E_Commerce.Api.Errors
{
    public class ApiExceptionError : ApiResponse
    {
        public string? Details { get; set; }
        public ApiExceptionError(int statusCode, string? message = null, string? details = null) : base(statusCode, message)
        {
            Details = details;
        }
    }
}
