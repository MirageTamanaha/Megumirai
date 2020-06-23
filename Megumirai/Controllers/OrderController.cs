using Megumirai.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Web.Mvc;
using System.Linq.Expressions;
using Microsoft.Ajax.Utilities;
using System.Security.Cryptography.X509Certificates;

namespace Megumirai.Controllers
{
    public static class LinqExtentions
    {
        public static IQueryable<TSource> WhereIf<TSource>
                                    (this IQueryable<TSource> Source, bool Condition, Expression<Func<TSource, bool>> Predicate)
        {
            if (Condition)
                return Source.Where(Predicate);
            else
                return Source;
        }
    }

    public class OrderController : Controller
    {
        // GET: Order

        public ActionResult OrderSearch()
        {
            return View();
        }
        public ActionResult OrderSearchResult(OrderMix model, DateTime? deliveryFrom, DateTime? deliveryTo, DateTime? orderFrom, DateTime? orderTo, string status)
        {

            using (var db = new Database1Entities1())
            {

                var x = db.OrderMixes
                   .WhereIf(model.OrderId != 0, e => e.OrderId >= model.OrderId)
                   .WhereIf(model.CustomerId != 0, e => e.CustomerId >= model.CustomerId)
                   .WhereIf(deliveryTo != null, e => e.DeliveryDate <= deliveryTo)
                   .WhereIf(deliveryTo != null, e => e.DeliveryDate <= deliveryTo)
                   .WhereIf(orderFrom != null, e => e.OrderDate >= orderFrom)
                   .WhereIf(orderFrom != null, e => e.OrderDate >= orderFrom)
                   .WhereIf(status == "出荷済", e => e.Status == "出荷済")
                   .WhereIf(status == "未出荷", e => e.Status == "未出荷")
                   .WhereIf(status == "キャンセル", e => e.Status == "キャンセル")
                   .WhereIf(status == "全て", e => e.Status == "出荷済" | e.Status == "未出荷" | e.Status == "キャンセル")
                    .ToList();

                //検索条件を結果に渡す。                                                                    
                
                ViewBag.customerId = model.CustomerId;
                ViewBag.orderNo = model.OrderId;
                ViewBag.deliveryFrom = deliveryFrom;
                ViewBag.deliveryTo = deliveryTo;
                ViewBag.orderFrom = orderFrom;
                ViewBag.orderTo = orderTo;
                ViewBag.status = status;

               
                Session["Search"] = x;


                return View(x);

            }
        }
        public ActionResult OrderUpdateInput(OrderMix om)
        {
            using (var db = new Database1Entities1())
            {
                var p = db.OrderMixes.Find(om.OrderMixId);
                Session["Update"] = p;
                return View(Session["Update"]);
            }
        }
        public ActionResult OrderUpdateX(OrderMix model, string status)
        {
            using (var db = new Database1Entities1())
            {
                var p = db.OrderMixes.Find(model.OrderMixId);
                var q = db.Items.Find(p.ItemId);
                if (p.Status != status)
                {
                    if (status == "未出荷")
                    {
                        q.Stock = q.Stock + p.Quantity;
                        db.SaveChanges();
                        p.Status = status;
                        db.SaveChanges();
                    }
                    else if (status == "出荷済")
                    {
                        if (q.Stock >= p.Quantity)
                        {
                            q.Stock = q.Stock - p.Quantity;
                            db.SaveChanges();
                            p.Status = status;
                            db.SaveChanges();
                        }
                       else if(q.Stock < p.Quantity)
                        {
                         ViewBag.message = "在庫数量が不足しています。";
                            return View("OrderUpdateInput",Session["Update"]);
                         }
                    }
                }
                return View("OrderUpdate",p);
               }
        }
        public ActionResult SearchBack() 
        {
            var u =  Session["Search"];
           
            return View("OrderSearchResult",u);
        }
     }
}




       
        





