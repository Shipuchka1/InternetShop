using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InternetShop.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public double Sum { get; set; }
        public int? VoucherId { get; set; }
        public bool IsPaid { get; set; }
        public virtual List<OrderEntry> OrderEntries { get; set; }
        public Voucher Voucher { get; set; }
        public Order()
        {
            OrderEntries = new List<OrderEntry>();
        }
       
    }

   
}