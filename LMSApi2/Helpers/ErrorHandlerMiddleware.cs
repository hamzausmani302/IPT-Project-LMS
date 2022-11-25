using System.Net;
using System.Text.Json;

namespace LMSApi2.Helpers
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                Console.WriteLine(error.GetType().Name);
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case AppException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case NotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case APIError e:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                    case BadRequestException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case NotAuthorizedException e:
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        break;
                    case ValidationException e:
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new {  m=error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
