using System;

namespace TheaterApplication.Utils.Exceptions
{
    public abstract class HandledException: Exception
    {
        public abstract object GetExceptionInfo();
    }
}
