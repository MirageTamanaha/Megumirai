﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using Megumirai.Models;

namespace Megumirai.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {
        private Database1Entities1 db = new Database1Entities1();

        [AllowAnonymous]
        public ActionResult EmpLoginIndex()
        {
            if (Request.IsAuthenticated)
            {
                return View("EmpMainMenu");
            }
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmpLoginIndex(EmpLoginModel model)
        {
            using (var db = new Database1Entities1())
            {
                var ul = db.Employees.Find(int.Parse(model.EmployeeId));
                if (int.Parse(model.EmployeeId) == ul.EmployeeId && model.Password == ul.Password)
                {
                    // ユーザー認証 成功 
                    FormsAuthentication.SetAuthCookie(userName: model.EmployeeId, createPersistentCookie: false);
                    return this.Redirect("/Login/EmpMainMenu");
                }
                else
                {
                    // ユーザー認証 失敗
                    this.ModelState.AddModelError(string.Empty, "指定されたユーザー名またはパスワードが正しくありません。");
                    return this.View(model);
                }
            }
        }
        public ActionResult EmpMainMenu()
        {
            return View();
        }
        public ActionResult EmpLogout()
        {
            FormsAuthentication.SignOut();
            return View("EmpLoginIndex");
        }
        [AllowAnonymous]
        public ActionResult CsLoginIndex()
        {
            if (Request.IsAuthenticated)
            {
                return View("CsMainMenu");
            }
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CsLoginIndex(CsLoginModel model, int customerid)
        {
            using (var db = new Database1Entities1())
            {
                var ul = db.Customers.Find(int.Parse(model.CustomerId));
                if (int.Parse(model.CustomerId) == ul.CustomerId && model.Password == ul.Password)
                {
                    var CsSession = new Customer()
                    {
                        CustomerId = customerid
                    };
                    Session["cslogin"] = CsSession;
                    FormsAuthentication.SetAuthCookie(userName:model.CustomerId, createPersistentCookie: false);

                    // ユーザー認証 成功
                    return this.Redirect("/Login/CsMainMenu");
                }
                else
                {
                    // ユーザー認証 失敗
                    ModelState.AddModelError(string.Empty, "入力された顧客IDまたはパスワードが誤っています");
                    return View(model);
                }
            }
        }
        public ActionResult CsMainMenu()
        {
            return View();
        } 
        public ActionResult CsLogout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return this.Redirect("/");
        }
    }
}
    
        
    




