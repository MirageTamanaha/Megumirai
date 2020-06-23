using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Megumirai.Models
{
    public class CsLoginModel
    {
        
        [Required(ErrorMessage = "顧客ID（パスワード）が入力されていません")]
        [RegularExpression("[0-9]{6}", ErrorMessage = "入力された顧客IDまたはパスワードが誤っています")]
        public string CustomerId { get; set; }
        [Required(ErrorMessage = "顧客ID（パスワード）が入力されていません")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}