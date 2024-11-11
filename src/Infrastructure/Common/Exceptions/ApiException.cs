﻿using System.Net;

namespace Infrastructure.Common.Exceptions;

public class ApiException(string message, HttpStatusCode statusCode) : Exception(message)
{
    public HttpStatusCode StatusCode { get; } = statusCode;
}