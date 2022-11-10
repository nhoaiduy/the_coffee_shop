using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebBanNuocUong_TheCoffeeShop.Models;

namespace WebBanNuocUong_TheCoffeeShop.Controllers
{
    public class TaiKhoanController : Controller
    {
        thecoffeeshopEntities db = new thecoffeeshopEntities();
        // GET: TaiKhoan
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(string username, string password)
        {
            var isUser = db.TAIKHOANs.FirstOrDefault(u => u.USERNAME.Equals(username.Trim()) && u.USERPASSWORD.Equals(password.Trim()));
            if (isUser != null)
            {
                if (isUser.ISENABLE)
                {
                    if (isUser.PHANQUYEN.Equals("AD"))
                    {
                        Session["admin"] = isUser;
                        return RedirectToAction("DanhMucSanPham", "SanPham", new { area = "Admin" });
                    }
                    else
                    {
                        Session["customer"] = isUser;
                        NGUOIDUNG nGUOIDUNG = db.NGUOIDUNGs.FirstOrDefault(u => u.USERID.Equals(isUser.USERID));
                        ViewBag.HOTEN = nGUOIDUNG.HOTEN;
                        return RedirectToAction("TrangChu", "TrangChu");
                    }
                }
            }
            ViewBag.Fail = "Đăng nhập thất bại";
            return View();
        }

        public ActionResult DangXuat()
        {
            Session.Clear();
            return RedirectToAction("TrangChu", "TrangChu", new {area = ""});
        }
    }
}