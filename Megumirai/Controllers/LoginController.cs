using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using Megumirai.Models;

namespace Megumirai.Controllers
{

    public class LoginController : Controller
    {
       [HttpGet]
        public ActionResult EmpLoginIndex()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EmpLoginIndex(EmpLoginModel model)
        {
            using (var db = new Database1Entities1())
            {
                var ul = db.Employees.Find(int.Parse(model.EmployeeId));
                if (int.Parse(model.EmployeeId)==ul.EmployeeId && model.Password==ul.Password)
                {
                    // ユーザー認証 成功    
                    FormsAuthentication.SetAuthCookie(model.EmployeeId, model.RememberMe);
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
        [HttpPost]
        public ActionResult EmpLogout()
        {
            FormsAuthentication.SignOut();
            return View("EmpLoginIndex");
        }
    }
    }
        
    




