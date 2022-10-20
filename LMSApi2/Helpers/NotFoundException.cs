using System.Globalization;

namespace LMSApi2.Helpers
{
    public class NotFoundException: Exception
    {
        
        public NotFoundException(string message="Not found") : base(message ) {
           
            
        }
        public NotFoundException(string message, params object[] args)
           : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
