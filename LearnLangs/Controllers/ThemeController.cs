using Microsoft.AspNetCore.Mvc;

namespace LearnLangs.Controllers
{
    public class ThemeController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Toggle(string? returnUrl = null)
        {
            var current = Request.Cookies["ui-theme"] ?? "light";
            var next = string.Equals(current, "dark", StringComparison.OrdinalIgnoreCase) ? "light" : "dark";

            Response.Cookies.Append("ui-theme", next, new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddYears(1),
                HttpOnly = false,               // cần JS đọc nếu có
                IsEssential = true,
                Secure = true,                  // chỉ gửi trên HTTPS
                SameSite = SameSiteMode.Lax,
                Path = "/"
            });

            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                return LocalRedirect(returnUrl);

            return Redirect("~/");
        }
    }
}
