﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Megumirai.Models;

namespace Megumirai.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        //商品追加画面の呼び出し
        public ActionResult CartInput(int Id)
        {
            using (var db = new Database1Entities1())
            {
                var items = db.Items.Find(Id);
                Session["cart"] = items;
                return View(items);
            }
        }
        //入力された数量・希望納期と一緒に商品をカートに入れる
        public ActionResult CartCheck(Item item, int quantity, string date)
        {
            int price = 0;
            price = item.UnitPrice * quantity;

            using (var db = new Database1Entities1())
            {
                var u = db.Items.Find(item.ItemId);
                var cart = new Cart
                {
                    ItemId = item.ItemId,
                    ItemName = item.ItemName,
                    ItemPhoto = item.ItemPhoto,
                    UnitPrice = item.UnitPrice,
                    Quantity = quantity,
                    DeliveryDate = date,
                    Price = price
                };

                if (quantity <= 0)
                {
                    var i = (Item)Session["cart"];
                    ViewBag.message = "数量は0より大きい値を入力してください。";
                    return View("CartInput", i);
                }
                else if (quantity > u.Stock)
                {
                    var i = (Item)Session["cart"];
                    ViewBag.message = "在庫数より多い数量を指定することはできません。";
                    return View("CartInput", i);
                }
                else if (quantity <= u.Stock)
                {
                    db.Carts.Add(cart);
                    db.SaveChanges();
                    ViewBag.Cart = cart;
                }
            }
            return View();
        }
        //見積確認の表示
        public ActionResult CartCalc(Cart cart)
        {
            int subPrice = 0;
            int tax = 0;
            int totalPrice = 0;

            using (var db = new Database1Entities1())
            {
                var carts = db.Carts.ToList();
                foreach (var i in carts)
                {
                    subPrice += i.Price;
                }
                tax = (int)Math.Truncate(subPrice * 0.08);
                totalPrice = subPrice + tax;
                ViewBag.SubPrice = String.Format("{0:C}-", subPrice);
                ViewBag.Tax = String.Format("{0:C}-", tax);
                ViewBag.TotalPrice = String.Format("{0:C}-", totalPrice);
                return View(carts);
            }
        }
        //見積商品の削除
        public ActionResult CartDelete(Cart cart)
        {
            using (var db = new Database1Entities1())
            {
                var u = db.Carts.Find(cart.CartId);
                db.Carts.Remove(u);
                db.SaveChanges();
                return RedirectToAction("CartCalc");
            }
        }

        //見積商品の数量・希望納期の変更
        public ActionResult CartUpdate(Cart cart)
        {
            using (var db = new Database1Entities1())
            {
                db.SaveChanges();
                return Redirect("CartCalc");
            }
        }
    }
}