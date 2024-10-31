using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LibraryWebApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                // Continúa el procesamiento de la solicitud hasta el controlador
                await _next(httpContext);
            }
            catch (ValidationException ex)
            {
                // Captura las excepciones de validación y devuelve un 400 Bad Request
                await HandleExceptionAsync(httpContext, ex, 400);
            }
            catch (DbUpdateException ex)
            {
                // Captura las excepciones de validación y devuelve un 400 Bad Request
                await HandleExceptionAsync(httpContext, ex, 400);
            }
            catch(KeyNotFoundException ex)
            {
                await HandleExceptionAsync(httpContext, ex, 404);
            }
            catch (InvalidOperationException ex)
            {
                await HandleExceptionAsync(httpContext, ex, 409);
            }
            catch (Exception ex)
            {
                // Captura cualquier otro tipo de excepción y devuelve un 500 Internal Server Error
                await HandleExceptionAsync(httpContext, ex, 500);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, int statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var response = new
            {
                message = exception.Message
            };

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
