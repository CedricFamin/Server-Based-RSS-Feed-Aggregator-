using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Utils
{
    public class WebResult
    {
        public ErrorCodeList ErrorCode { get; set; }

        public enum ErrorCodeList
        {
            SUCCESS = 0x0,
            USER_NOT_FOUND,
            USER_ALREADY_EXIST,
        }

        public WebResult()
        {
            ErrorCode = ErrorCodeList.SUCCESS;
        }

        public WebResult(ErrorCodeList error)
        {
            ErrorCode = error;
        }
    }

    public class WebResult<T> : WebResult
    {
        public T Value { get; set; }

        public WebResult() : base()
        {
        }

        public WebResult(ErrorCodeList error) : base(error)
        {
        }

        public WebResult(T value) : base()
        {
            Value = value;
        }

        public WebResult(T value, ErrorCodeList error) : base(error)
        {
            Value = value;
        }
    }
}