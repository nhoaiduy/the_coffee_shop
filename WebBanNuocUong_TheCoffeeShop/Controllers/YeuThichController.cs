using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanNuocUong_TheCoffeeShop.Models;

namespace WebBanNuocUong_TheCoffeeShop.Controllers
{
    public class YeuThichController : Controller
    {
        thecoffeeshopEntities db = new thecoffeeshopEntities();
        // GET: YeuThich
        public ActionResult YeuThich(string MASP)
        {
            var user = Session["customer"] as TAIKHOAN;
            SANPHAM sp = user.NGUOIDUNG.SANPHAMs.FirstOrDefault(s => s.MASP.Equals(MASP));
            if(sp != null)
            {
                user.NGUOIDUNG.SANPHAMs.Remove(sp);
            }
            else
            {
                sp = db.SANPHAMs.FirstOrDefault(s => s.MASP.Equals(MASP));
                user.NGUOIDUNG.SANPHAMs.Add(sp);
            }
            sp = db.SANPHAMs.FirstOrDefault(s => s.MASP.Equals(MASP));
            db.SaveChanges();
            return RedirectToAction("ChiTietSanPham", "SanPham", new { @productName = sp.TENSP, area = "" });
        }
    }
}