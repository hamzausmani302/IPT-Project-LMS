using System.Globalization;

namespace LMSApi2.Helpers
{
    public class ValidationException : Exception
    {
        public ValidationException(string message = "Validation error") : base(message)
        {


        }
        public ValidationException(string message, params object[] args)
           : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
