using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
namespace HotelWebApp.Middlewares
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path.ToString().ToLower();
            var userRole = context.Session.GetString("UserRole");

            // Nếu chưa đăng nhập, chặn truy cập mọi trang trừ trang đăng nhập
            if (string.IsNullOrEmpty(userRole) && !path.StartsWith("/account"))
            {
                await ShowForbiddenPage(context);
                return;
            }
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
            context.Response.ContentType = "text/html";
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
