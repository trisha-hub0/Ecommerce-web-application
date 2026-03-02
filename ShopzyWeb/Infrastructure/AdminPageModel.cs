using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ShopzyWeb.Infrastructure
{
    public class AdminPageModel : PageModel
    {
        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            var isAdmin = context.HttpContext.Session.GetString("IsAdmin");

            if (isAdmin != "true")
            {
                context.Result = new RedirectToPageResult("/Index");
            }

            base.OnPageHandlerExecuting(context);
        }
    }
}
