using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanNuocUong_TheCoffeeShop.Models;

namespace WebBanNuocUong_TheCoffeeShop.Areas.Admin.Controllers
{
    public class LoaiSanPhamController : Controller
    {
        thecoffeeshopEntities db = new thecoffeeshopEntities();
        // Post: Admin/LoaiSanPham
        [HttpPost]
        public ActionResult ThemLoaiSanPham([Bind(Include = "MALOAISP, TENLOAISP")] LOAISANPHAM lOAISANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.LOAISANPHAMs.Add(lOAISANPHAM);
                db.SaveChanges();
                return RedirectToAction("DanhMucSanPham", "SanPham");
            }
            return RedirectToAction("DanhMucSanPham", "SanPham");
        }


    }
}