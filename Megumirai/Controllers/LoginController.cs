using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Megumirai.Models;

namespace Megumirai.Controllers
{
    public class LoginController : Controller
    {

        private Database1Entities db = new Database1Entities();

        [AllowAnonymous]
        public ActionResult EmpLoginIndex()
        {
                  return View();
        }


        [HttpPost]
        public ActionResult EmpLogin(string id,string password,Employee model)
        {
            using (var db = new Database1Entities())
            {
                var ul = db.Employees.Find(int.Parse(id));
                if (int.Parse(id) == ul.EmployeeId && password == ul.Password)
                {
                    // ユーザー認証 成功    
                    FormsAuthentication.SetAuthCookie(id, model.RememberMe);
                    return this.Redirect("EmpMainMenu");
                }
                else
                {
                    // ユーザー認証 失敗
                    this.ModelState.AddModelError(string.Empty, "指定されたユーザー名またはパスワードが正しくありません。");
                    return this.View(model);
                }
            }

        }
    }
}
        
