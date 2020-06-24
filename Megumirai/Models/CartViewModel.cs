using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Megumirai.Models;

namespace Megumirai.Models
{
    public class CartViewModel
    {
        public int CartId { get; set; }
        public int CustomerId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemPhoto { get; set; }
        public int UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string DeliveryDate { get; set; }
        public Boolean IsChecked { get; set; }

        public CartViewModel() { }
        public CartViewModel(Cart cart)
        {
            CartId = cart.CartId;
            CustomerId = cart.CustomerId;
            ItemId = cart.ItemId;
            ItemName = cart.ItemName;
            ItemPhoto = cart.ItemPhoto;
            UnitPrice = cart.UnitPrice;
            Quantity = cart.Quantity;
            Price = cart.Price;
            DeliveryDate = cart.DeliveryDate;
        }
    }
}