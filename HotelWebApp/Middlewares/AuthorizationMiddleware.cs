using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace HotelWebApp.Middlewares
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizationMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor)
        {
            _next = next;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path.ToString().ToLower();
            var userRole = _httpContextAccessor.HttpContext?.Session.GetString("UserRole");

            if (path.StartsWith("/admin") && userRole != "Admin")
            {
                await ShowForbiddenPage(context);
                return;
            }
            else if (path.StartsWith("/manager") && userRole != "Admin" && userRole != "Manager")
            {
                await ShowForbiddenPage(context);
                return;
            }

            await _next(context);
        }

        private async Task ShowForbiddenPage(HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Response.ContentType = "text/html; charset=utf-8";
            await context.Response.WriteAsync(@"
            <html>
                <head><title>403 - Forbidden</title></head>
                <body>
                    <h1>403 - Bạn không có quyền truy cập trang này</h1>
                    <p>Vui lòng liên hệ Admin nếu bạn nghĩ đây là lỗi.</p>
                    <a href='/'>Quay lại trang chủ</a>
                </body>
            </html>");
        }
    }
}
