//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはテンプレートから生成されました。
//
//     このファイルを手動で変更すると、アプリケーションで予期しない動作が発生する可能性があります。
//     このファイルに対する手動の変更は、コードが再生成されると上書きされます。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Megumirai.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            this.Orders = new HashSet<Order>();
            this.OrderMixes = new HashSet<OrderMix>();
        }
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "会社名を入力してください")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "住所を入力してください")]
        public string Adress { get; set; }
        [Required(ErrorMessage = "電話番号を入力してください")]

        [RegularExpression("[0-9]{2,4}-[0-9]{2,4}-[0-9]{4}", ErrorMessage = "電話番号は、半角数値と半角ハイフンでxxx-yyyy-zzzzの形式で入力してください")]
        public string TelNo { get; set; }

        public string Dept { get; set; }
        [Required(ErrorMessage = "氏名(漢字)を入力してください")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "氏名(かな)を入力してください")]
        [RegularExpression("[ぁ-ん!ー]+$", ErrorMessage = "氏名(かな)は、全角ひらかなで入力してください")]

        public string CustomerKana { get; set; }
        [RegularExpression(@"^[!-~]*$", ErrorMessage = "メールアドレスは、半角英数字と記号で入力してください")]
        [EmailAddress(ErrorMessage = "メールアドレスは、xxxx@yyyyの形式で入力してください")]

        public string Email { get; set; }
        [RegularExpression(@"^[0-9a-zA-Z]+$", ErrorMessage = "パスワードは、半角英数字で入力してください")]
        [Required(ErrorMessage = "パスワードを入力してください")]
        public string Password { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderMix> OrderMixes { get; set; }
    }
}