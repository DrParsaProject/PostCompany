using System.Web.Mvc;

namespace PostCompany
{
    public class CustomViewEngine : RazorViewEngine
    {
        public CustomViewEngine()
        {
            var viewLocations = new[] {
            "~/Views/Site/{0}.cshtml",
            "~/Views/Site/Counter/{0}.cshtml",
            "~/Views/Site/Customer/{0}.cshtml",
            "~/Views/Site/Login/{0}.cshtml",
            "~/Views/Site/Manager/{0}.cshtml",
            "~/Views/Site/Transport/{0}.cshtml",
            "~/Views/Site/Weight/{0}.cshtml",
            "~/Views/Shared/{0}.cshtml",
            "~/Views/Shared/{0}.cshtml"
        };

            this.PartialViewLocationFormats = viewLocations;
            this.ViewLocationFormats = viewLocations;
        }
    }
}