using System.Globalization;

namespace LMSApi2.Helpers
{
    public class APIError : Exception
    {
        public APIError(string message) : base(message)
        {


        }
        public APIError(string message, params object[] args)
           : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
