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
        public ActionResult HuyDon(string MADH)
        {
            DONHANG dONHANG = db.DONHANGs.FirstOrDefault(d => d.MADH.Equals(MADH));
            dONHANG.MATT = 8;
            db.Entry(dONHANG).State = System.Data.Entity.EntityState.Modified;
            if (Session["customer"] != null)
            {
                var user = Session["customer"] as TAIKHOAN;
                NGUOIDUNG nGUOIDUNG = db.NGUOIDUNGs.FirstOrDefault(n => n.USERID.Equals(user.USERID));
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
            db.SaveChanges();
            return RedirectToAction("ChiTietDonHang", "DonHang", new { area = "", MADH = MADH });
        }
    }
}