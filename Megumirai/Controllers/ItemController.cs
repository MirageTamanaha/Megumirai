using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Megumirai.Models;


namespace Megumirai.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        // GET: Item
        public ActionResult ItemCatalog()
        {
            using (var db = new Database1Entities())
            {
                var ul = db.Items.ToList();
                return View("ItemCatalog", ul);
            }
        }

        public ActionResult ItemList()
        {
            using (var db = new Database1Entities())
            {
                var ItemList = db.Items.ToList();
                return View(ItemList);
            }
        }

        public ActionResult ItemAddInput()
        {
            return View();
        }

        public ActionResult ItemAddCheck(int itemid, string itemname, string itemurl, int unitprice, string size, string assortment, string category, int stock)
        {
            var Imodel1 = new Item
            {
                ItemId = itemid,
                ItemName = itemname,
                ItemPhoto = itemurl,
                UnitPrice = unitprice,
                Size = size,
                Assortment = assortment,
                Category = category,
            };
            Session["additem"] = Imodel1;
            ViewBag.Imodel1 = Imodel1;
            return View();
        }

        public ActionResult ItemAdd(Item item)
        {
            using (var db = new Database1Entities())
            {
                var u = (Item)Session["additem"];
                db.Items.Add(u);
                db.SaveChanges();
                Session.Remove("additem");
            }
            return Redirect("ItemList");
        }

        public ActionResult ItemUpdateInput(Item item)
        {
            using (var db = new Database1Entities())
            {
                var u = db.Items.Find(item.ItemId);
                ViewBag.Model = u;
                return View();
            }
        }

        public ActionResult ItemUpdateCheck(int itemid, string itemname, string itemurl, int unitprice, string size, string assortment, string category)
        {
            var Imodel2 = new Item
            {
                ItemId = itemid,
                ItemName = itemname,
                ItemPhoto = itemurl,
                UnitPrice = unitprice,
                Size = size,
                Assortment = assortment,
                Category = category
            };
            ViewBag.Imodel2 = Imodel2;
            Session["updateitem"] = Imodel2;
            return View();
        }

        public ActionResult ItemUpdate()
        {
            var u = (Item)Session["updateitem"];

            using (var db = new Database1Entities())
            {
                var ul = db.Items.Find(u.ItemId);
                ul.ItemName = u.ItemName;
                ul.ItemPhoto = u.ItemPhoto;
                ul.UnitPrice = u.UnitPrice;
                ul.Size = u.Size;
                ul.Assortment = u.Assortment;
                ul.Category = u.Category;
                db.SaveChanges();
            }
            Session.Remove("updateitem");
            return Redirect("ItemList");
        }

        public ActionResult ItemDeleteCheck(Item item)
        {
            using (var db = new Database1Entities())
            {
                var u = db.Items.Find(item.ItemId);
                var Imodel3 = new Item
                {
                    ItemId = u.ItemId,
                    ItemName = u.ItemName,
                    ItemPhoto = u.ItemPhoto,
                    UnitPrice = u.UnitPrice,
                    Size = u.Size,
                    Assortment = u.Assortment,
                    Category = u.Category
                };
                Session["deleteitem"] = Imodel3;
                ViewBag.Imodel3 = Imodel3;
                return View();
            }
        }
        public ActionResult ItemDelete(Item item)
        {
            using (var db = new Database1Entities())
            {
                var u = (Item)Session["deleteitem"];
                var I = db.Items.Find(u.ItemId);
                db.Items.Remove(I);
                db.SaveChanges();
                Session.Remove("deleteitem");
                return RedirectToAction("ItemList");
            }
        }
    }
}