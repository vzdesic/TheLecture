using System.Web.Mvc;

namespace Enciklopedija.Areas.Glazba
{
    public class GlazbaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Glazba";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Glazba_TopPjesme",
                "TopPjesme/{topN}",
                new { area = "Glazba", controller = "Pjesmas", action = "TopPjesme", topN = UrlParameter.Optional }
            );

            context.MapRoute(
                "Glazba_TopIzvodaci",
                "TopIzvodaci/{topN}",
                new { area = "Glazba", controller = "Izvodacs", action = "TopIzvodaci", topN = UrlParameter.Optional }
            );

            context.MapRoute(
                "Glazba_default",
                "Glazba/{controller}/{action}/{id}",
                new { controller="Pjesmas", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}