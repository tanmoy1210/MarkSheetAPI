using System;
using MT.Exceptions;
namespace MT.Exceptions
{
    #region snippet_HttpResponseException
    public class HttpResponseException : Exception
    {
        public int Status { get; set; } = 500;

        public object Value { get; set; }
    }
    #endregion
}
