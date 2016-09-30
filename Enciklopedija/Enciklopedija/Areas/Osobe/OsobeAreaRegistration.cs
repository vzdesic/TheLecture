using System.Web.Mvc;

namespace Enciklopedija.Areas.Osobe
{
    public class OsobeAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Osobe";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Osobe_DatumRodenja",
                "RodeniNaDan/{datum}",
                new { area = "Osobe", controller = "Osobas", action = "RodeniNaDatum", datum = UrlParameter.Optional }
            );

            context.MapRoute(
                "Osobe_default",
                "Osobe/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}