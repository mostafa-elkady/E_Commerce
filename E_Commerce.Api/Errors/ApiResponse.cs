﻿namespace E_Commerce.Api.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public ApiResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }
        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A Bad Request, you have made",
                401 => " you are not Authorized,",
                404 => "Resource was not Found",
                500 => "Errors are the path to the dark side. Errors lead to anger. Anger leads to hate.Hate leads to career thange",
                _ => null
            };
        }
    }
}
