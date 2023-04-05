using System;

namespace Server
{
    [Serializable]
    public class Request
    {
        public enum AnswerType
        {
            TextMessage,
            OpenServerConnection,
            OpenClientConnection,
            CloseClientConnection
        }

        public AnswerType Type { get; set; }
        public object? Body { get; set; }
        public string From { get; set; }
        public string? To { get; set; }

        public Request(AnswerType type, string from, string? to = null, object? body = null)
        {
            Type = type;
            Body = body;
            From = from;
            To = to;
        }
    }
}