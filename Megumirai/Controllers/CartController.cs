using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Megumirai.Models;

namespace Megumirai.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
 public ActionResult CartInput()
        {
            using (var db = new Database1Entities1())
            {
                var items = db.Items.Find(1);
                return View(items);
            }
        }

        public ActionResult CartCheck(Item item, string quantity, string date)
        {
            Cart cart = new Cart
            {
                Itemid = String.Format("{0:D4}", item.ItemId),
                Itemname = item.ItemName,
                Itemphoto = item.ItemPhoto,
                Unitprice = String.Format("{0:C}-", item.UnitPrice),
                Quantity = int.Parse(quantity),
                Deliverydate = date
            };
            Session["cart"] = cart;
            GetSessionValue();
            return View();
        }

        public ActionResult CartCalc(Cart cart)

        {
            return View();
        }

        public class Cart
        {
            public string Itemid { get; set; }
            public string Itemname { get; set; }
            public string Itemphoto { get; set; }
            public string Unitprice { get; set; }
            public int Quantity { get; set; }
            public string Deliverydate { get; set; }
        }

        private void GetSessionValue()
        {
            //セッション状態の値を取得します。
            object cart = Session["cart"];
            ViewBag.Cart = cart;
        }

    }
}