using System;

namespace Viber.Bot
{
    /// <summary>
    /// Viber request API exception.
    /// </summary>
    public class ViberRequestApiException : Exception
    {
        public ViberRequestApiException(
            ErrorCode code,
            string message,
            string method,
            string request,
            string response) : base(message) {
            Code = Enum.IsDefined(typeof(ErrorCode), code) ? code : ErrorCode.GeneralError;
            Request = request;
            Method = method;
            Response = response;
        }

        public ErrorCode Code { get; }

        public string Method { get; }
        public string Request { get; }
        public string Response { get; }
    }
}