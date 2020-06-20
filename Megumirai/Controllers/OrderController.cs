using Megumirai.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Web.Mvc;
using System.Linq.Expressions;
using Microsoft.Ajax.Utilities;

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

        public ActionResult OrderSearchResult(string customerId, string orderNo, DateTime? deliveryFrom, DateTime? deliveryTo, DateTime? orderFrom, DateTime? orderTo, string status)
        {
            int CsId = int.Parse(customerId);
            int OrNo = int.Parse(orderNo);

            using (var db = new Database1Entities1())
            {
                var x = from e in db.OrderMixes
                        where e.CustomerId == CsId
                         & e.OrderId == OrNo
                        select new
                        {
                            OrderId = e.OrderId,
                            CustomerId = e.CustomerId,
                            OrderDetailId = e.OrderDetailId,
                            ItemId = e.ItemId,
                            ItemName = e.ItemName,
                            DeliveryDate = e.DeliveryDate,
                            Status = e.Status,
                            Quantity = e.Quantity
                        };
                //検索条件を結果に渡す。                                                                    
                ViewBag.customerId = customerId;
                ViewBag.orderNo = orderNo;
                ViewBag.deliveryFrom = deliveryFrom;
                ViewBag.deliveryTo = deliveryTo;
                ViewBag.orderFrom = orderFrom;
                ViewBag.orderTo = orderTo;
                ViewBag.status = status;

                ViewBag.List = x;
                return View();

            }
        }

        public ActionResult OrderList()
        {
            using (var db = new Database1Entities1())
            {
                var List = db.OrderMixes.ToList();

                return View(List);
            }
        }

        public ActionResult OrderUpdateInput(string OrderMixId)
        {
            using (var db = new Database1Entities1())
            {
                var p = db.OrderMixes.Find(int.Parse(OrderMixId));

                return View(p);
            }
        }

        public ActionResult OrderUpdate(OrderMix model, string status)
        {
            using (var db = new Database1Entities1())
            {
                var p = db.OrderMixes.Find(model.OrderMixId);
                var q = db.Stocks.Find(model.ItemId);
                if (p.Status != status)
                {
                    if (status == "未出荷")
                    {
                        p.Status = status;
                        q.Stock1 = q.Stock1 + p.Quantity;
                    }
                    else if (status == "出荷")
                    {
                        p.Status = status;
                        q.Stock1 = q.Stock1 - p.Quantity;
                    }
                }
                db.SaveChanges();
                return View("OrderUpdateInput", p);
            }



        }
        public ActionResult OrderUpdateX(OrderMix model, string status)
        {
            using (var db = new Database1Entities1())
            {
                var p = db.OrderMixes.Find(model.OrderMixId);
                var q = db.Stocks.Find(model.ItemId);


                //q.Stock1 = q.Stock1 + p.Quantity;

                //db.SaveChanges();
                return Content(String.Format("{0:D4}",q.ItemId));  
            //return View("OrderUpdateInput", p);
            }
        }
    }
}

    




       
        





