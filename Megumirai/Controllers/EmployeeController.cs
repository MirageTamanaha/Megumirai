using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Megumirai.Models;

namespace Megumirai.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult EmpList()
        {
            using (var db = new Database1Entities())
            {
                var EmployeeList = db.Employees.ToList();
                return View(EmployeeList);
            }
        }

        public ActionResult EmpAddInput()
        {
            return View();
        }

        public ActionResult EmpAddCheck(int employeeid, string employeename, string password)
        {
            var Emodel1 = new Employee
            {
                EmployeeId = employeeid,
                EmployeeName = employeename,
                Password = password,
            };
            Session["addemployee"] = Emodel1;
            ViewBag.Emodel1 = Emodel1;
            return View(Emodel1);
        }

        public ActionResult EmpAdd(Employee employee)
        {
            using (var db = new Database1Entities())
            {
                var u = (Employee)Session["addemployee"];
                db.Employees.Add(u);
                db.SaveChanges();
                Session.Remove("addemployee");
            }
            return Redirect("EmpList");
        }

        public ActionResult EmpUpdateInput(Employee employee)
        {
            using (var db = new Database1Entities())
            {
                var u = db.Employees.Find(employee.EmployeeId);
                ViewBag.Model = u;
                return View();
            }
        }

        public ActionResult EmpUpdateCheck(int employeeid, string employeename, string password)
        {
            var Emodel2 = new Employee
            {
                EmployeeId = employeeid,
                EmployeeName = employeename,
                Password = password,
            };
            ViewBag.Emodel2 = Emodel2;
            Session["updateEmployee"] = Emodel2;
            return View();
        }

        public ActionResult EmpUpdate()
        {
            var u = (Employee)Session["updateemployee"];

            using (var db = new Database1Entities())
            {
                var ul = db.Employees.Find(u.EmployeeId);
                ul.EmployeeName = u.EmployeeName;
                ul.Password = u.Password;
                db.SaveChanges();
            }
            Session.Remove("updateemployee");
            return Redirect("EmpList");
        }

        public ActionResult EmpDeleteCheck(Employee employee)
        {
            using (var db = new Database1Entities())
            {
                var u = db.Employees.Find(employee.EmployeeId);
                var Emodel3 = new Employee
                {
                    EmployeeId = u.EmployeeId,
                    EmployeeName = u.EmployeeName,
                    Password = u.Password,
                };
                Session["deleteemp"] = Emodel3;
                ViewBag.Emodel3 = Emodel3;
                return View();
            }
        }
        public ActionResult EmpDelete(Employee employee)
        {
            using (var db = new Database1Entities())
            {
                var u = (Employee)Session["deleteemp"];
                var I = db.Employees.Find(u.EmployeeId);
                db.Employees.Remove(I);
                db.SaveChanges();
                Session.Remove("deleteemp");
                return RedirectToAction("EmpList");
            }
        }
    }
}