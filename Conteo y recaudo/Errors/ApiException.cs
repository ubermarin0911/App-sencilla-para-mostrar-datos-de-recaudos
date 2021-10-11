using System;
using System.Collections.Generic;
using System.Text;

namespace Conteo_y_recaudo.Errors
{
    public class ApiException : ApiResponse
    {
        public ApiException(int statusCode, string message = null, string details = null)
        : base(statusCode, message)
        {
            Details = details;
        }

        public string Details { get; set; }
    }
}
