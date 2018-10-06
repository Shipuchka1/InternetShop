using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InternetShop.Models
{
    public class Good
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string UrlImage { get; set; }
        public double Price { get; set; }
        public double BasePrice { get; set; }
        [Required]
        public bool isCategory { get; set; }
        public bool isDeleted { get; set; }
        public int? ParentId { get; set; }

        public static List<int?> GetAllSubCategoriesIds(List<int?> ids, bool firstCl = true)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            List<int?> goodsId =db.Goods.Where(w => w.isCategory && ids.Contains(w.ParentId) && !w.isDeleted).Select(s=> (int?)s.Id).ToList();
            if(goodsId.Count!=0)
            {
                List<int?> tempIds = Good.GetAllSubCategoriesIds(goodsId, false);
                goodsId.AddRange(tempIds);

            }
            if (firstCl)
            {
                goodsId.AddRange(ids);
            }
            return goodsId;
        }

        public static List<Good> GetAllSubCategoriesGoods(List<int?> ids)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            return db.Goods.Where(w=>!w.isDeleted&& !w.isCategory&&ids.Contains(w.ParentId)).ToList();
        }

        public List<Good> GetPath()
        {
            List<Good> goods = new List<Good>();
            goods.Add(this);
            Good temp = this;
            while(true)
            {
                if (temp.ParentId == null)
                    break;
                else
                {
                    temp = ModelService.GetGoodById((int)temp.ParentId);
                    goods.Add(temp);
                }
            }

            return goods;
        }

        public void CalcPrice()
        {
            Price = BasePrice;
            List<Good> goods = GetPath();
            List<int> ids = goods.Select(s => s.Id).ToList();
            Sale bestSale = ModelService.GetAllSaleGoodMaps().Where(w=>ids.Contains(w.GoodId)).Select(s=>s.Sale).Where(w=>w.FinishDate>DateTime.Now).OrderByDescending(o=>o.PercentSale).FirstOrDefault();
            if(bestSale!=null)
            {
                Price = BasePrice * (100 - bestSale.PercentSale) / 100;
            }
        }
    }
}