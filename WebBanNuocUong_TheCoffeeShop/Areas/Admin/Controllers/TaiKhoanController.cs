using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanNuocUong_TheCoffeeShop.Models;

namespace WebBanNuocUong_TheCoffeeShop.Areas.Admin.Controllers
{
    public class TaiKhoanController : Controller
    {
        thecoffeeshopEntities db = new thecoffeeshopEntities();
        // GET: Admin/TaiKhoan
        public ActionResult ThongTinTaiKhoan()
        {
            var user = Session["admin"] as TAIKHOAN;
            NGUOIDUNG nGUOIDUNG = db.NGUOIDUNGs.FirstOrDefault(n => n.USERID.Equals(user.USERID));
            return View(nGUOIDUNG);
        }

        [HttpPost]
        public ActionResult ChinhSuaThongTin([Bind(Include = "HOTEN, SDT, DIACHI, NGAYSINH, EMAIL")] NGUOIDUNG nGUOIDUNG)
        {
            if (ModelState.IsValid)
            {
                var user = Session["admin"] as TAIKHOAN;
                NGUOIDUNG nGUOIDUNG1 = db.NGUOIDUNGs.FirstOrDefault(u => u.USERID.Equals(user.USERID));
                if(nGUOIDUNG.HOTEN != null)
                {
                    nGUOIDUNG1.HOTEN = nGUOIDUNG.HOTEN;
                }
                if (nGUOIDUNG.SDT != null)
                {
                    nGUOIDUNG1.SDT = nGUOIDUNG.SDT;
                }
                if (nGUOIDUNG.DIACHI != null)
                {
                    nGUOIDUNG1.DIACHI = nGUOIDUNG.DIACHI;
                }
                if (nGUOIDUNG.NGAYSINH != null)
                {
                    nGUOIDUNG1.NGAYSINH = nGUOIDUNG.NGAYSINH;
                }
                if (nGUOIDUNG.EMAIL != null)
                {
                    nGUOIDUNG1.EMAIL = nGUOIDUNG.EMAIL;
                }
                db.Entry(nGUOIDUNG1).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhMucSanPham", "SanPham", new { area = "Admin" });
            }
            ViewBag.Fail = "Thông tin không được để trống";
            return RedirectToAction("ThongTinTaiKhoan", "TaiKhoan", new {area = "Admin"});
        }

        [HttpPost]
        public ActionResult DoiMatKhau(string MATKHAU, string MATKHAUMOI, string NMATKHAUMOI)
        {
            TAIKHOAN user = Session["admin"] as TAIKHOAN;
            if (!user.USERPASSWORD.Equals(MATKHAU))
            {
                ViewBag.Fail = "Mật khẩu không đúng";
                return RedirectToAction("ThongTinTaiKhoan", "TaiKhoan", new { area = "Admin" });
            }
            if (!MATKHAUMOI.Equals(NMATKHAUMOI))
            {
                ViewBag.Fail = "Mật khẩu không trùng khớp";
                return RedirectToAction("ThongTinTaiKhoan", "TaiKhoan", new { area = "Admin" });
            }
            user.USERPASSWORD = MATKHAUMOI;
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ThongTinTaiKhoan", "TaiKhoan", new { area = "Admin" });
        }
    }
}