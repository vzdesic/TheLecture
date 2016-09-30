using System.Web.Mvc;

namespace Enciklopedija.Areas.Dogadaji
{
    public class DogadajiAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Dogadaji";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
               "Dogadaji_PocetniDatum",
               "DogadajiNaDan/{datum}",
               new { area = "Dogadaji", controller = "Dogadajs", action = "ZapoceliNaDatum", datum = UrlParameter.Optional }
            );

            context.MapRoute(
                "Dogadaji_default",
                "Dogadaji/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}