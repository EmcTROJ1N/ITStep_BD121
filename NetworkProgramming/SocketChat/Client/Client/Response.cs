using System;

namespace Client
{
    [Serializable]
    public class Response
    {
        public enum ResponseStatus
        {
            OK,
            Error
        }

        public ResponseStatus Status { get; set; }
        public Request.AnswerType Type { get; set; }
        public object? Body { get; set; }
        public string? From { get; set; }

        public Response(ResponseStatus status, Request.AnswerType type, string? from = null, object? body = null)
        {
            Status = status;
            Type = type;
            Body = body;
            From = from;
        }
    }
}