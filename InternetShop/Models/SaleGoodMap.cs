using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternetShop.Models
{
    public class SaleGoodMap
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public int GoodId { get; set; }
        public virtual Sale Sale { get; set; }
        public virtual Good Good { get; set; }
    }
}