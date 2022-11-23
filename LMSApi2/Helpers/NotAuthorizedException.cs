using System.Globalization;

namespace LMSApi2.Helpers
{
    public class NotAuthorizedException : Exception
    {
        public NotAuthorizedException(string message = "Not Authorized") : base(message)
        {


        }
        public NotAuthorizedException(string message, params object[] args)
           : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
