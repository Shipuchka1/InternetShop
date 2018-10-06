
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace InternetShop.Models
{
    public class ModelService
    {

        public static List<Good> GetAllGood()
        {
     ApplicationDbContext db = new ApplicationDbContext();
            return db.Goods.Where(w=>!w.isDeleted).ToList();

    }

        public static List<Good> GetAllOnlyGood()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            return db.Goods.Where(w => !w.isDeleted&&!w.isCategory).ToList();

        }

        public static bool AddNewGood(Good good)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            try
            {
                good.isDeleted = false;
                db.Goods.Add(good);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static int GetCartCount(HttpSessionStateBase Session)
        {
            if (Session["Cart"] == null)
                Session["Cart"] = new Dictionary<int, int>();
            Dictionary<int, int> cart = (Dictionary<int, int>)Session["Cart"];
            return cart.Values.Sum();
           
        }

        public static List<Good> GetAllCategories()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            return db.Goods.Where(w => w.isCategory && !w.isDeleted).ToList();
        }

        public static Good GetGoodById(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            return db.Goods.FirstOrDefault(f => f.Id == id);
        }

        public static ApplicationUser GetUserById(string id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            return db.Users.FirstOrDefault(f => f.Id == id);
        }

        public static bool UpdateUser(ApplicationUser user)
        {
          
            try
            {
                ApplicationDbContext db = new ApplicationDbContext();
                ApplicationUser us = db.Users.FirstOrDefault(f => f.Id == user.Id);
                db.Entry(us).CurrentValues.SetValues(user);
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public static bool AddOrder(Order order)
        {
            try
            {
                ApplicationDbContext db = new ApplicationDbContext();
                db.Orders.Add(order);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public static List<Order> GetOrdersForUser(ApplicationUser user)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            return db.Orders.Where(w => w.UserId == user.Id).ToList(); 
        }

        public static Order GetOrderById(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            return db.Orders.FirstOrDefault(f => f.Id == id);
        }

        public static List<Voucher> GetAllVouchers()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            return db.Vouchers.Where(w=>w.FinishDate>DateTime.Now).ToList();
        }

        public static bool AddNewVoucher(Voucher voucher)
        {
            try
            {
                ApplicationDbContext db = new ApplicationDbContext();
                db.Vouchers.Add(voucher);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool EditVoucher(Voucher voucher)
        {
            try
            {
                ApplicationDbContext db = new ApplicationDbContext();
                Voucher v = db.Vouchers.FirstOrDefault(f => f.Id == voucher.Id);
                db.Entry(v).CurrentValues.SetValues(voucher);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool RemoveVoucher(int id)
        {
            try
            {
                ApplicationDbContext db = new ApplicationDbContext();
                Voucher voucher = db.Vouchers.FirstOrDefault(f => f.Id == id);
                db.Vouchers.Remove(voucher);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static Voucher GetVoucherById(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            return db.Vouchers.FirstOrDefault(f => f.Id == id);
        }

        public static List<OrderEntry> GetOrderEntriesByOrderId(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            return db.OrderEntries.Where(w => w.Order.Id == id).ToList();
        }


        public static bool AddSale(Sale sale)
        {
            try
            {
                ApplicationDbContext db = new ApplicationDbContext();
                db.Sale.Add(sale);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool EditSale(Sale sale)
        {
            try
            {
                ApplicationDbContext db = new ApplicationDbContext();
                Sale v = db.Sale.FirstOrDefault(f => f.Id == sale.Id);
                db.Entry(v).CurrentValues.SetValues(sale);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool RemoveSale(int id)
        {
            try
            {
                ApplicationDbContext db = new ApplicationDbContext();
                Sale sale = db.Sale.FirstOrDefault(f => f.Id == id);
                db.Sale.Remove(sale);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static Sale GetSaleById(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            return db.Sale.FirstOrDefault(f => f.Id == id);
        }

        public static List<Sale> GetAllSale()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            return db.Sale.Where(w=>w.FinishDate>DateTime.Now).ToList();
        }


        public static bool AddSaleGoodMap(SaleGoodMap saleGoodMap)
        {
            try
            {
                ApplicationDbContext db = new ApplicationDbContext();
                db.SaleGoodMaps.Add(saleGoodMap);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool EditSaleGoodMap(SaleGoodMap saleGoodMap)
        {
            try
            {
                ApplicationDbContext db = new ApplicationDbContext();
                SaleGoodMap v = db.SaleGoodMaps.FirstOrDefault(f => f.Id == saleGoodMap.Id);
                db.Entry(v).CurrentValues.SetValues(saleGoodMap);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool RemoveSaleGoodMap(int id)
        {
            try
            {
                ApplicationDbContext db = new ApplicationDbContext();
                SaleGoodMap saleGoodMap = db.SaleGoodMaps.FirstOrDefault(f => f.Id == id);
                db.SaleGoodMaps.Remove(saleGoodMap);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static SaleGoodMap GetSaleGoodMapById(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            return db.SaleGoodMaps.FirstOrDefault(f => f.Id == id);
        }

        public static List<SaleGoodMap> GetAllSaleGoodMaps()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            return db.SaleGoodMaps.ToList();
        }

    }
}