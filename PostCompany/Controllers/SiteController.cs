using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PostCompany.Views.Site
{
	/// <summary>
	/// این کلاس صفحات مختلف را به توجه به 
	/// کاربری که ورود کرده به سیستم نمایش می دهد
	/// </summary>
    public class SiteController : Controller
    {
        //
        // GET: /Site/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Manager()
        {
            return View();
        }
        public ActionResult Customer()
        {
            return View();
        }

    }
}
