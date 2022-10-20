namespace LMSApi2.Helpers
{


    public enum ERROR_TYPES { 
        WRONG_CREDENTIALS=0,
        AUTH_ERROR=1,
    }

    public class ErrorMessages
    {
        public static  readonly Dictionary<ERROR_TYPES, string> dict = new Dictionary<ERROR_TYPES, string>() {
            { ERROR_TYPES.WRONG_CREDENTIALS , "Incorrect Username Or Password" },
            {ERROR_TYPES.AUTH_ERROR , "Forbidden Access" }
        };
        
    }
}
