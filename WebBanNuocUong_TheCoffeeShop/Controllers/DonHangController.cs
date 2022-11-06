using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanNuocUong_TheCoffeeShop.Models;

namespace WebBanNuocUong_TheCoffeeShop.Controllers
{
    public class DonHangController : Controller
    {
        thecoffeeshopEntities db = new thecoffeeshopEntities();
        // GET: DonHang
        public ActionResult ChiTietDonHang(string MADH)
        {
            DONHANG dONHANG = db.DONHANGs.FirstOrDefault(d => d.MADH.Equals(MADH));
            if(dONHANG != null)
            {
                return View(dONHANG);
            }
            return RedirectToAction("TrangChu", "TrangChu");
        }
    }
}