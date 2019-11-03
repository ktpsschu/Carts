using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Carts.Controllers
{
    public class RouteTestController : Controller
    {
        // GET: RouteTest
        public ActionResult Index()
        {
            return View();
            //return Content("這是index");
        }
        public ActionResult Index2(int id=1)
        {
            //return View();
            return Content("這是index2 id="+ id);
        }
        public ActionResult Index3(int? id)
        {
            //return View();
            return Content("這是index3 id=" + id);
        }
    }
}