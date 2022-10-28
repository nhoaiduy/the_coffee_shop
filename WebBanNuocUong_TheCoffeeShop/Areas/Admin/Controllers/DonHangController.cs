using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanNuocUong_TheCoffeeShop.Models;

namespace WebBanNuocUong_TheCoffeeShop.Areas.Admin.Controllers
{
    public class DonHangController : Controller
    {
        thecoffeeshopEntities db = new thecoffeeshopEntities();

        // GET: Admin/DonHang
        public ActionResult DanhSachDonHang(string tinhTrang)
        {
            var donHangs = db.DONHANGs.OrderByDescending(d => d.MADH).ToList();
            if (!string.IsNullOrEmpty(tinhTrang))
            {
                if(tinhTrang.Equals("Tất cả"))
                {
                    ViewBag.TINHTRANG = db.TINHTRANGs.ToList();
                    return View(donHangs.ToList());
                }
                donHangs = donHangs.Where(d => d.TINHTRANG.Equals(tinhTrang)).ToList();
            }
            ViewBag.TINHTRANG = db.TINHTRANGs.ToList();
            return View(donHangs.ToList());
        }

        public ActionResult ChiTietDonHang(string MADH)
        {
            DONHANG dONHANG = db.DONHANGs.FirstOrDefault(d => d.MADH.Equals(MADH));
            ViewBag.TINHTRANG = db.TINHTRANGs.ToList();
            return View(dONHANG);
        }

        public ActionResult TimKiem()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TimKiem(string MADH)
        {
            var donHangs = db.DONHANGs.Where(d => d.MADH.ToLower().Trim().Equals(MADH.ToLower().Trim()));
            ViewBag.SEARCHSTRING = MADH;
            return View(donHangs.ToList());
        }

        public ActionResult SetStatus(string status, string MADH)
        {
            DONHANG dONHANG = db.DONHANGs.FirstOrDefault(d => d.MADH.Equals(MADH));
            dONHANG.TINHTRANG = status;
            db.Entry(dONHANG).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ChiTietDonHang", "DonHang", new { MADH = MADH });
        }
    }
}