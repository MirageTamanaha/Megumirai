using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Megumirai.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Home()
        {
            return View();
        }
    }
}