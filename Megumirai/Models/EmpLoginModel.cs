using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Megumirai.Models
{
    public class EmpLoginModel
    {
        [RegularExpression("[0-9]{4}", ErrorMessage = "社員番号は数字で入力してください")]
        [Required(ErrorMessage = "IDは必須です。")]
        public string EmployeeId { get; set; }
        [Required(ErrorMessage = "パスワードは必須です。")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}