﻿using System.Net;

namespace GeolocationApi.Application.Exceptions
{
    public class InternalErrorException : ApplicationException
    {
        public InternalErrorException(string message = null) : base(message)
        {
            Code = ExceptionCode.InternalError;
            HttpStatus = HttpStatusCode.InternalServerError;
        }
    }
}
