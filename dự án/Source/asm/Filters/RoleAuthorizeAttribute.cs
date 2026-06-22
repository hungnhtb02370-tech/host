// Filters/RoleAuthorizeAttribute.cs
// Dùng như [RoleAuthorize("Admin")] hoặc [RoleAuthorize("Admin","NhanVien")]
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace asm.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class RoleAuthorizeAttribute : ActionFilterAttribute
    {
        private readonly string[] _roles;

        public RoleAuthorizeAttribute(params string[] roles)
        {
            _roles = roles;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session;
            var role = session.GetString("Role");

            if (string.IsNullOrEmpty(role))
            {
                // Chưa đăng nhập → trả 401
                context.Result = new JsonResult(new
                {
                    success = false,
                    message = "Vui lòng đăng nhập!",
                    redirect = "/Account/Login"
                })
                { StatusCode = 401 };
                return;
            }

            if (_roles.Length > 0 && !_roles.Contains(role))
            {
                // Không đủ quyền → trả 403
                context.Result = new JsonResult(new
                {
                    success = false,
                    message = $"Bạn không có quyền truy cập. Yêu cầu: [{string.Join(", ", _roles)}]",
                    yourRole = role
                })
                { StatusCode = 403 };
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}