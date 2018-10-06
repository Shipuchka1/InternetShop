using InternetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternetShop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

       
        public ActionResult VoucherList()
        {
            return View(ModelService.GetAllVouchers());
        }

        public ActionResult AddNewVoucher()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AddNewVoucher(Voucher voucher)
        {
            if (ModelService.AddNewVoucher(voucher))
                return RedirectToAction("VoucherList");
            else return View(voucher);
        }
        public ActionResult EditVoucher(int id )
        {
            Voucher v = ModelService.GetVoucherById(id);
            return View(v);
        }
        [HttpPost]
        public ActionResult EditVoucher(Voucher v)
        {
            if (ModelService.EditVoucher(v))
                return RedirectToAction("VoucherList");
            else return View(v);
        }

        public ActionResult DeleteVoucher(int id)
        {
            ModelService.RemoveVoucher(id);
            return RedirectToAction("VoucherList");
           
        }

        public ActionResult SaleList()
        {
            return View(ModelService.GetAllSale());
        }

        public ActionResult AddSale()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSale(Sale sale)
        {
            if (ModelService.AddSale(sale))
                return RedirectToAction("SaleList");
            else return View();
        }

        public ActionResult EditSale(int id)
        {
            Sale v = ModelService.GetSaleById(id);
            return View(v);
        }
        [HttpPost]
        public ActionResult EditSale(Sale v)
        {
            if (ModelService.EditSale(v))
                return RedirectToAction("SaleList");
            else return View(v);
        }

        public ActionResult DeleteSale(int id)
        {
            ModelService.RemoveSale(id);
            return RedirectToAction("SaleList");

        }


        public ActionResult SaleGoodMapList()
        {
            return View(ModelService.GetAllSaleGoodMaps());
        }

        public ActionResult AddSaleGoodMap()
        {
            SelectList goods = new SelectList(ModelService.GetAllGood(), "Id", "Name");
            ViewBag.Goods = goods;

            SelectList sales = new SelectList(ModelService.GetAllSale(), "Id", "Name");
            ViewBag.Sales = sales;
            return View();
        }

        [HttpPost]
        public ActionResult AddSaleGoodMap(SaleGoodMap saleGoodMap)
        {
           
            if (ModelService.AddSaleGoodMap(saleGoodMap))
                return RedirectToAction("SaleGoodMapList");
            else return View();
        }

        public ActionResult EditSaleGoodMap(int id)
        {
            SaleGoodMap v = ModelService.GetSaleGoodMapById(id);
            return View(v);
        }
        [HttpPost]
        public ActionResult EditSaleGoodMap(SaleGoodMap v)
        {
            if (ModelService.EditSaleGoodMap(v))
                return RedirectToAction("SaleGoodMapList");
            else return View(v);
        }

        public ActionResult DeleteSaleGoodMap(int id)
        {
            ModelService.RemoveSaleGoodMap(id);
            return RedirectToAction("SaleGoodMapList");

        }

    }
}