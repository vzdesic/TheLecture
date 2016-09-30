using System.Web.Mvc;

namespace Enciklopedija.Areas.Zanimljivosti
{
    public class ZanimljivostiAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Zanimljivosti";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Zanimljivosti_default",
                "Zanimljivosti/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}