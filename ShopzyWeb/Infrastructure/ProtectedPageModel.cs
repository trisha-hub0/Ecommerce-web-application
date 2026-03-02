using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ShopzyWeb.Infrastructure
{
    public class ProtectedPageModel : PageModel
    {
        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                context.Result = new RedirectToPageResult("/Account/Login");
                return;
            }

            base.OnPageHandlerExecuting(context);
        }
    }
}
