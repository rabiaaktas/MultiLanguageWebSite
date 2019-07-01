using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using RouteLocalization.Mvc;
using RouteLocalization.Mvc.Setup;
using WebApplication10.Controllers;

namespace WebApplication10
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapMvcAttributeRoutes(Localization.LocalizationDirectRouteProvider);

            ISet<string> acceptedCultures = new HashSet<string>() { "tr", "en" };
            string DefaultCulture = "en";
            var configuration = new RouteLocalization.Mvc.Setup.Configuration       //localization configurations
            {

                DefaultCulture = DefaultCulture,
                AcceptedCultures = acceptedCultures,
                AttributeRouteProcessing =
            AttributeRouteProcessing.AddAsNeutralAndDefaultCultureRoute,
                AddCultureAsRoutePrefix = true
            };

            var localization = new Localization(configuration); //create a localization variable

            localization.TranslateInitialAttributeRoutes();
            localization.ForCulture("tr")       //localized variables---for tr
                .ForController<HomeController>()
                .ForAction(x => x.About())
                .AddTranslation("Hakkında")
                .ForAction(x => x.Contact())
                .AddTranslation("İletişim");
            CultureSensitiveHttpModule.GetCultureFromHttpContextDelegate =
            Localization.DetectCultureFromBrowserUserLanguages(acceptedCultures,
               DefaultCulture);

            
        routes.MapRoute(                //route value for the change language attribute
            name: "Language",
                 url: "{controller}/{action}",
                 defaults: new { controller = "Language", action = "ChangeLanguage" }
                 );
        }

    }
}
