using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanNuocUong_TheCoffeeShop.Models;

namespace WebBanNuocUong_TheCoffeeShop.Controllers
{
    public class QuanLyController : Controller
    {
        thecoffeeshopEntities db=new thecoffeeshopEntities();
        // GET: QuanLy
        public ActionResult TTCaNhan()
        {
            return View();
        }
        public ActionResult GioHang()
        {
            return View();
        }
        public ActionResult DonHang(string tinhTrang)
        {
            var donHangs = db.DONHANGs.OrderByDescending(d => d.MADH).ToList();
            if (!string.IsNullOrEmpty(tinhTrang))
            {
                if (tinhTrang.Equals("Tất cả"))
                {
                    ViewBag.TINHTRANG = db.TINHTRANGs.ToList();
                    return View(donHangs.ToList());
                }
                donHangs = donHangs.Where(d => d.TINHTRANG.Equals(tinhTrang)).ToList();
            }
            ViewBag.TINHTRANG = db.TINHTRANGs.ToList();
            return View(donHangs.ToList());
        }
        public ActionResult ChiTietDonHang()
        {

            return View();
        }
        public ActionResult DoiMatKhau()
        {
            return View();
        }
        /*Dành cho khách vãng lai*/
        public ActionResult GioHangMoi()
        {
            return View();
        }
        public ActionResult TraCuuDonHang()
        {
            return View();
        }
    }
}