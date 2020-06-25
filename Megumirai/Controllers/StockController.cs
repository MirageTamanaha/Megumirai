using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Megumirai.Models;

namespace Megumirai.Controllers
{

    [Authorize]
    public class StockController : Controller
    {
        // GET: Stock
        public ActionResult StockList()
        {
            using (var db = new Database1Entities1())
            {
                var StockList = db.Items.ToList();
                return View(StockList);
            }
        }

        public ActionResult StockUpdateInput(Item item)
        {
            using (var db = new Database1Entities1())
            {
                var u = db.Items.Find(item.ItemId);
                ViewBag.Model = u;
                Session["updateEmployee"] = u;
                return View();
            }
        }
        public ActionResult StockUpdate(int up)
        {
            var I = (Item)Session["updateemployee"];
            using (var db = new Database1Entities1())
            {
                var u = db.Items.Find(I.ItemId);
                u.Stock = u.Stock + up;
                db.SaveChanges();
            }
            return RedirectToAction("StockList");
        }
        public ActionResult StockUpdateDown(int down)
        {
            var I = (Item)Session["updateemployee"];
            using (var db = new Database1Entities1())
            {
                var u = db.Items.Find(I.ItemId);
                u.Stock = u.Stock - down;
                db.SaveChanges();
            }
            return RedirectToAction("StockList");
        }

        public ActionResult StockUpdateDate(DateTime rdate)
        {
            var I = (Item)Session["updateemployee"];
            using (var db = new Database1Entities1())
            {
                var u = db.Items.Find(I.ItemId);
                u.ReceiptDate = rdate;
                db.SaveChanges();
            }
            return RedirectToAction("StockList");
        }
    }
}