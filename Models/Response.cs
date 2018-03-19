using System;

// Custom class for a error message
// status code represents the code which we want to send as
// and the message represent the message which can be used
// by front end for displaying
namespace jobManagement.Models
{
    public class Response
    {
        public Response()
        {
            // provide a default message here
        }

        public Response(string message, long statusCode)
        {
            this.msg = message;
            this.code = statusCode;
        }
        public long code { get; set; }
        public string msg { get; set; }
    }
}
