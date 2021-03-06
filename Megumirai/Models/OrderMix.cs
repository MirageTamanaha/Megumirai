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
    
    public partial class OrderMix
    {
        public int OrderMixId { get; set; }
        public int OrderId { get; set; }
        public int OrderDetailId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public System.DateTime DeliveryDate { get; set; }
        public string Status { get; set; }
        public int CustomerId { get; set; }
        public int SubPrice { get; set; }
        public int Tax { get; set; }
        public int TotalPrice { get; set; }
        public System.DateTime OrderDate { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Item Item { get; set; }
        public virtual Order Order { get; set; }
    }
}
