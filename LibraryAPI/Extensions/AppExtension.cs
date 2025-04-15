using LibraryAPI.Middlewares;

namespace LibraryAPI.Extensions
{
    public static class AppExtension
    {
        public static void ErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
