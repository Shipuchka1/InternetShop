using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternetShop.Models
{
    public class OrderEntry
    {
        public int Id { get; set; }
        public int GoodId { get; set; }
        public Order Order { get; set; }
        public virtual Good Good { get; set; }
        public double EndPrice { get; set; }
        public int Quantity { get; set; }
    }
}