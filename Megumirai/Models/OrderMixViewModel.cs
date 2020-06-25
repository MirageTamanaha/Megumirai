using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Megumirai.Models
{
    public class OrderMixViewModel
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

        public OrderMixViewModel() { }
        public OrderMixViewModel(OrderMix om)
        {
            OrderMixId = om.OrderMixId;
            OrderId = om.OrderId;
            OrderDetailId = om.OrderDetailId;
            ItemId = om.ItemId;
            ItemName = om.ItemName;
            UnitPrice = om.UnitPrice;
            Quantity = om.Quantity;
            Price = om.Price;
            DeliveryDate = om.DeliveryDate;
            Status = om.Status;
            CustomerId = om.CustomerId;
            SubPrice = om.SubPrice;
            Tax = om.Tax;
            TotalPrice = om.TotalPrice;
            OrderDate = om.OrderDate;
        }
    }
}