using System.Globalization;

namespace LMSApi2.Helpers
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message = "Bad request") : base(message)
        {


        }
        public BadRequestException(string message, params object[] args)
           : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
