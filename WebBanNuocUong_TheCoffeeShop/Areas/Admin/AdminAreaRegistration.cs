using System.Web.Mvc;

namespace WebBanNuocUong_TheCoffeeShop.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "WebBanNuocUong_TheCoffeeShop.Areas.Admin.Controllers" }
            );
        }
    }
}