namespace LMSApi2.Authorization
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowAnonymousAttribute : Attribute
    {
        //class created to prevent amnbigious reference
    }
}
