using InternetShop.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternetShop.Controllers
{
    public class GoodController : Controller
    {
        // GET: Good
        public ActionResult Index()
        {
            return View(ModelService.GetAllGood());
        }

        public ActionResult Create()
        {
            SelectList categories = new SelectList(ModelService.GetAllCategories(), "Id", "Name");
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Good good)
        {
            if (ModelState.IsValid && ModelService.AddNewGood(good))
                return RedirectToAction("Index");
            else return View();
        }

        [HttpPost]
        public PartialViewResult LeftSubMenuPartial(string id)
        {
            List<Good> goods = ModelService.GetAllCategories().Where(w => w.ParentId == Int32.Parse(id)).ToList();
            return PartialView(goods);
        }

        [HttpPost]
        public PartialViewResult GetGoodsList(int id)
        {

            List<Good> tmp = Good.GetAllSubCategoriesGoods(Good.GetAllSubCategoriesIds(new List<int?>() { id }));
            foreach (Good item in tmp)
            {
                item.CalcPrice();
            }
            return PartialView("MainGoodsPartial", tmp);
        }

        public ActionResult CartView()
        {
            if (Session["Cart"] == null)
                Session["Cart"] = new Dictionary<int, int>();
            Dictionary<int, int> cart = (Dictionary<int, int>)Session["Cart"];

            Dictionary<Good, int> goods = new Dictionary<Good, int>();
            foreach (var item in cart)
            {
                Good tmp = ModelService.GetGoodById(item.Key);
                tmp.CalcPrice();
                goods.Add(tmp, item.Value);
            }
            return View(goods);
        }

        public PartialViewResult AddToCart(int id = 0)
        {
            if (Session["Cart"] == null)
                Session["Cart"] = new Dictionary<int, int>();
            Dictionary<int, int> cart = (Dictionary<int, int>)Session["Cart"];
            if (id != 0)
            {
                if (cart.ContainsKey(id))
                    cart[id]++;
                else cart[id] = 1;
                Session["Cart"] = cart;
            }

            return PartialView(cart.Values.Sum());
        }

        [HttpPost]
        public JsonResult SetCountCart(int GoodId, int newCount)
        {
            string result = "error";
            Dictionary<int, int> cart = (Dictionary<int, int>)Session["Cart"];
            if (cart.ContainsKey(GoodId))
            {
                if (newCount<0)
                {
                    cart.Remove(GoodId);
                }
                else
                {
                cart[GoodId] = newCount;
                }
                Session["Cart"] = cart;
                result = "success";
            }

                return Json(result);
        }
    
        [Authorize]
        public ActionResult Checkout()
        {
            string id = User.Identity.GetUserId();
            ApplicationUser user = ModelService.GetUserById(id);
            if (user.IsLegalEntity == false)
            {
                if (string.IsNullOrEmpty(user.City) || string.IsNullOrEmpty(user.Street) || string.IsNullOrEmpty(user.House) || string.IsNullOrEmpty(user.Phone))
                    ViewBag.ControlMessage = "Вам необходимо заполнить адрес и телефон в личном кабинете";
                else ViewBag.ControlMessage = "Подтвердите заказ";
            }
            else
            {
                if (string.IsNullOrEmpty(user.IIN) || string.IsNullOrEmpty(user.KPP) || string.IsNullOrEmpty(user.Phone) || string.IsNullOrEmpty(user.LegalAddress))
                    ViewBag.ControlMessage = "Вам необходимо заполнить все данные в личном кабинете";
                else ViewBag.ControlMessage = "Подтвердите заказ";
            }
            
           return View();
        }

        [Authorize]
        public ActionResult MainPagePersonalAccount()
        {
            string id = User.Identity.GetUserId();
            ApplicationUser user = ModelService.GetUserById(id);
            return View(user);
        }

        [Authorize]
        public ActionResult PersonalAccount()
        {
            string id = User.Identity.GetUserId();
            ApplicationUser user = ModelService.GetUserById(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult PersonalAccount(ApplicationUser user)
        {
            if (ModelService.UpdateUser(user))
                return RedirectToAction("MainPagePersonalAccount");
            else
                return View(user);
        }

        [HttpPost]
        public PartialViewResult EntityOrIndividual(string arg)
        {
            if (arg == "entity")
                return PartialView("PersonalForEntity");
            else return PartialView("PersonalForIndividual");
        }

        public ActionResult AddOrder(double totalSum)
        {
            if(Session["Cart"]!=null)
            {
                Dictionary<int, int> cart = (Dictionary<int, int>)Session["Cart"];
                Order order = new Order();
                order.OrderDate = DateTime.Now;
                order.Sum = totalSum;
                order.IsPaid = false;
                if (Session["Promo"] != null)
                    order.VoucherId = (int)Session["Promo"];
                order.UserId = User.Identity.GetUserId();
                foreach (KeyValuePair<int,int> item in cart)
                {
                    OrderEntry temp = new OrderEntry();
                    temp.Good = ModelService.GetGoodById(item.Key);
                    temp.Good.CalcPrice();
                    temp.GoodId = item.Key;
                    temp.Order = order;
                    temp.Quantity = item.Value;
                    temp.EndPrice = temp.Good.Price;
                    temp.Good = null;
                    order.OrderEntries.Add(temp);
                }

                ViewBag.StateOrder = "error";
                if (ModelService.AddOrder(order))
                {
                    ViewBag.StateOrder = "success";
                }
                Session["Cart"] = null;
                Session["Promo"] = null;
            }
            
            return View();
        }

        public  ActionResult OrdersStory()
        {
            ApplicationUser user = ModelService.GetUserById(User.Identity.GetUserId());
            List<Order> orders = ModelService.GetOrdersForUser(user);
            return View(orders);
        }

        public ActionResult DetailsOrder(int id)
        {
            Order order = ModelService.GetOrderById(id);
            return View(order);
        }

        public ActionResult Promo(string keyword)
        {
            Voucher v = ModelService.GetAllVouchers().FirstOrDefault(f => f.KeyWord == keyword);
            if (v == null)
                return Json("error");
            else
            {
                Session["Promo"] = v.Id;
                return Json("success;"+v.PercentOff);
            }
        }

        public ActionResult GoodDetails(int id)
        {
            Good good = ModelService.GetGoodById(id);
            good.CalcPrice();
            return View(good);
        }
    }
}