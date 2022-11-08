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
            var donHangs = db.DONHANGs.OrderByDescending(d => d.MADH).Where(d => d.MATT >1).ToList();
            if (!string.IsNullOrEmpty(tinhTrang))
            {
                donHangs = donHangs.Where(d => d.TINHTRANG.TINHTRANG1.Equals(tinhTrang)).ToList();
            }
            ViewBag.TINHTRANG = db.TINHTRANGs.Select(t => t.TINHTRANG1).ToList();
            return View(donHangs.ToList());
        }

        public ActionResult ChiTietDonHang(string MADH)
        {
            DONHANG dONHANG = db.DONHANGs.FirstOrDefault(d => d.MADH.Equals(MADH));
            ViewBag.MATT = new SelectList(db.TINHTRANGs.ToList(), "MATT", "TINHTRANG1", dONHANG.MATT );
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

        public ActionResult SetStatus(int MATT, string MADH)
        {
            DONHANG dONHANG = db.DONHANGs.FirstOrDefault(d => d.MADH.Equals(MADH));
            dONHANG.MATT = MATT;
            if (MATT == 8)
            {
                NGUOIDUNG nGUOIDUNG = db.NGUOIDUNGs.FirstOrDefault(n => n.HOTEN.Equals(dONHANG.TENNGUOINHAN)&&n.SDT.Equals(dONHANG.SDT) && n.DIACHI.Equals(dONHANG.DIACHI));
                nGUOIDUNG.DIEMTICHLUY -= (int)(dONHANG.TONGITEN / 1000);
                db.Entry(nGUOIDUNG).State = System.Data.Entity.EntityState.Modified;
                List<CTDONHANG> cTDONHANGs = db.CTDONHANGs.Where(c => c.MADH.Equals(MADH)).ToList();
                foreach (var item in cTDONHANGs)
                {
                    SANPHAM sANPHAM = db.SANPHAMs.FirstOrDefault(s => s.MASP.Equals(item.MASP));
                    sANPHAM.SOLUONG += item.SOLUONG;
                    db.Entry(sANPHAM).State = System.Data.Entity.EntityState.Modified;
                }
            }
            db.Entry(dONHANG).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ChiTietDonHang", "DonHang", new { area = "Admin", MADH = MADH });
        }
    }
}