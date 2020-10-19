using System;
using TheaterApplication.Utils.Exceptions;

namespace TheaterApplication.Bll.Exceptions
{
    public class InternalHandlingException: HandledException
    {
        private readonly ExceptionInfo exceptionInfo;

        public InternalHandlingException(string code, string message)
        {
            exceptionInfo = new ExceptionInfo(code, message);
        }

        public override object GetExceptionInfo()
        {
            return exceptionInfo;
        }
    }

    public class ExceptionInfo
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }

        public ExceptionInfo(string code, string message)
        {
            Code = code;
            Message = message;
            Timestamp = DateTime.UtcNow;
        }
    }
}
