using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Megumirai.Models;

namespace Megumirai.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        // GET: Customer
        [AllowAnonymous]
        public ActionResult CsAddInput()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult CsAddCheck(Customer customer)
        {
            var u = new Customer
            {
                CompanyName = customer.CompanyName,
                Adress = customer.Adress,
                TelNo = customer.TelNo,
                CustomerName = customer.CustomerName,
                CustomerKana = customer.CustomerKana,
                Dept = customer.Dept,
                Email = customer.Email,
                Password = customer.Password
            };
            Session["customer"] = u;
            ViewBag.Customer = Session["customer"];
            return View();
        }
        [AllowAnonymous]
        public ActionResult CsAdd(Customer customer)
        {
            using (var db = new Database1Entities1())
            {
                var u = (Customer)Session["customer"];
                db.Customers.Add(u);
                db.SaveChanges();
                int lastCustomerId = db.Customers.Max(model => model.CustomerId);
                ViewBag.id = lastCustomerId;
                return View();
            }
        }

        public ActionResult CsList()
        {
            using (var db = new Database1Entities1())
            {
                var CustomerList = db.Customers.ToList();
                return View(CustomerList);
            }
        }

        public ActionResult CsUpdateInput(Customer customer)
        {
            using (var db = new Database1Entities1())
            {
                var u = db.Customers.Find(customer.CustomerId);
                ViewBag.Model = u;
                return View();
            }
        }

        public ActionResult CsUpdateCheck(int customerid, string companyname, string adress, string telno, string customername, string customerkana, string dept, string email, string password)
        {
            var Cmodel2 = new Customer
            {
                CustomerId = customerid,
                CompanyName = companyname,
                Adress = adress,
                TelNo = telno,
                CustomerName = customername,
                CustomerKana = customerkana,
                Dept = dept,
                Email = email,
                Password = password,
            };
            ViewBag.Cmodel2 = Cmodel2;
            Session["updateCustomer"] = Cmodel2;
            return View();
        }

        public ActionResult CsUpdate()
        {
            var u = (Customer)Session["updatecustomer"];

            using (var db = new Database1Entities1())
            {
                var ul = db.Customers.Find(u.CustomerId);
                ul.CompanyName = u.CompanyName;
                ul.Adress = u.Adress;
                ul.TelNo = u.TelNo;
                ul.CustomerName = u.CustomerName;
                ul.CustomerKana = u.CustomerKana;
                ul.Dept = u.Dept;
                ul.Email = u.Email;
                ul.Password = u.Password;
                db.SaveChanges();
            }
            Session.Remove("updateemployee");
            return Redirect("CsList");
        }

        public ActionResult CsDeleteCheck(Customer customer)
        {
            using (var db = new Database1Entities1())
            {
                var u = db.Customers.Find(customer.CustomerId);
                var Cmodel3 = new Customer
                {
                    CustomerId = u.CustomerId,
                    CompanyName = u.CompanyName,
                    Adress = u.Adress,
                    TelNo = u.TelNo,
                    CustomerName = u.CustomerName,
                    CustomerKana = u.CustomerKana,
                    Dept = u.Dept,
                    Email = u.Email,
                    Password = u.Password,
                };
                Session["deletecs"] = Cmodel3;
                ViewBag.Cmodel3 = Cmodel3;
                return View();
            }
        }
        public ActionResult CsDelete(Customer customer)
        {
            using (var db = new Database1Entities1())
            {
                var u = (Customer)Session["deletecs"];
                var I = db.Customers.Find(u.CustomerId);
                db.Customers.Remove(I);
                db.SaveChanges();
                Session.Remove("deletecs");
                return RedirectToAction("CsList");
            }
        }
    }
}