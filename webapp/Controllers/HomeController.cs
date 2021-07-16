using KKN_UI.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KKN_UI.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //using (KKN_Demo_Service.KKN_Demo_Service conn = new KKN_Demo_Service.KKN_Demo_Service())
            //{
            //    var vr = conn.GetDemoByDetailId(1);
            //}

            return View();
        }
    }
}