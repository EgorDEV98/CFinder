namespace CFinder.WebAPI.Middleware;

/// <summary>
/// Внедрение перехватчика ошибок
/// </summary>
internal static class CustomExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this
        IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
    }
}