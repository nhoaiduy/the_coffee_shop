using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public ActionResult ThongTinCaNhan()
        {
            var user = Session["customer"] as TAIKHOAN;
            NGUOIDUNG nGUOIDUNG = db.NGUOIDUNGs.FirstOrDefault(n => n.USERID.Equals(user.USERID));
            return View(nGUOIDUNG);
        }
        [HttpPost]
        public ActionResult ChinhSuaThongTin([Bind(Include = "HOTEN, SDT, DIACHI, NGAYSINH, EMAIL, DIACHI2")] NGUOIDUNG nGUOIDUNG)
        {
            if (ModelState.IsValid)
            {
                var user = Session["customer"] as TAIKHOAN;
                NGUOIDUNG nGUOIDUNG1 = db.NGUOIDUNGs.FirstOrDefault(u => u.USERID.Equals(user.USERID));
                if (nGUOIDUNG.HOTEN != null)
                {
                    nGUOIDUNG1.HOTEN = nGUOIDUNG.HOTEN;
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
                nGUOIDUNG1.DIACHI2 = nGUOIDUNG1.DIACHI2;
                db.Entry(nGUOIDUNG1).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ThongTinCaNhan", "QuanLy", new { area = "" });
            }
            ViewBag.Fail = "Thông tin không được để trống";
            return RedirectToAction("ThongTinCaNhan", "QuanLy", new { area = "" });
        }
        public ActionResult DanhSachDonHang(string tinhTrang = "", string MADH = "")
        {
            var user = Session["customer"] as TAIKHOAN;
            NGUOIDUNG nGUOIDUNG = db.NGUOIDUNGs.FirstOrDefault(n => n.USERID.Equals(user.USERID));
            var donHangs = db.DONHANGs.OrderByDescending(d => d.MADH)
                .Where(d => d.SDT.Equals(nGUOIDUNG.SDT)).ToList();
            if (!string.IsNullOrEmpty(MADH))
            {
                donHangs = donHangs.Where(d => d.MADH.ToLower().Trim().Equals(MADH.ToLower().Trim())).ToList();
                ViewBag.SEARCHSTRING = MADH;
                ViewBag.TINHTRANG = db.TINHTRANGs.ToList();
                return View(donHangs.ToList());
            }
            if (!string.IsNullOrEmpty(tinhTrang))
            {
                if (tinhTrang.Equals("Tất cả"))
                {
                    ViewBag.TINHTRANG = db.TINHTRANGs.ToList();
                    return View(donHangs.ToList());
                }
                donHangs = donHangs.Where(d => d.TINHTRANG.TINHTRANG1.Equals(tinhTrang)).ToList();
            }
            ViewBag.TINHTRANG = db.TINHTRANGs.ToList();
            return View(donHangs.ToList());
        }

        public ActionResult DoiMatKhau()
        {
            var user = Session["customer"] as TAIKHOAN;
            TAIKHOAN tAIKHOAN = db.TAIKHOANs.FirstOrDefault(n => n.USERID.Equals(user.USERID));

            return View(tAIKHOAN);
        }
        [HttpPost]
        public ActionResult ChinhSuaMatKhau(string mkhientai, string mkmoi, string mkmoicf)
        {
            if (ModelState.IsValid)
            {
                var user = Session["customer"] as TAIKHOAN;
                TAIKHOAN tAIKHOAN1 = db.TAIKHOANs.FirstOrDefault(u => u.USERID.Equals(user.USERID));
                if (!String.IsNullOrEmpty(mkhientai))
                {
                    if (mkhientai==tAIKHOAN1.USERPASSWORD)
                    {
                        if (!String.IsNullOrEmpty(mkmoi) && !String.IsNullOrEmpty(mkmoicf) && mkmoi.Equals(mkmoicf))
                        {
                            tAIKHOAN1.USERPASSWORD = mkmoi;
                            db.Entry(tAIKHOAN1).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("DoiMatKhau", "QuanLy", new { area = "" });
                        }
                        else
                        {
                            ViewBag.Fail = "Mật khẩu không chính xác";
                            return RedirectToAction("DoiMatKhau", "QuanLy", new { area = "" });
                        }
                    }
                    else
                    {
                        ViewBag.Fail = "Mật khẩu không chính xác";
                        return RedirectToAction("DoiMatKhau", "QuanLy", new { area = "" });
                    }
                }
            }
            ViewBag.Fail = "Thông tin không được để trống";
            return RedirectToAction("DoiMatKhau", "QuanLy", new { area = "" });
        }
        public ActionResult DoiTenDangNhap()
        {
            var user = Session["customer"] as TAIKHOAN;
            TAIKHOAN tAIKHOAN = db.TAIKHOANs.FirstOrDefault(n => n.USERID.Equals(user.USERID));

            return View(tAIKHOAN);
        }
        [HttpPost]
        public ActionResult GhiNhanDoiTenDangNhap(string username, string mkhientai)
        {
            if (ModelState.IsValid)
            {
                var user = Session["customer"] as TAIKHOAN;
                TAIKHOAN tAIKHOAN1 = db.TAIKHOANs.FirstOrDefault(u => u.USERID.Equals(user.USERID));
                if (!String.IsNullOrEmpty(username)&&!String.IsNullOrEmpty(mkhientai))
                {
                    if (mkhientai == tAIKHOAN1.USERPASSWORD)
                    {
                        var temp = db.TAIKHOANs.FirstOrDefault(u => u.USERNAME.ToLower().Trim().Equals(username.Trim()));
                        if(temp != null)
                        {
                            return RedirectToAction("DoiTenDangNhap", "QuanLy", new { area = "" });
                        }
                        else
                        {
                            tAIKHOAN1.USERNAME = username;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        ViewBag.Fail = "Kiểm tra lại thông tin";
                        return RedirectToAction("DoiTenDangNhap", "QuanLy", new { area = "" });
                    }
                }
            }
            ViewBag.Fail = "Thông tin không được để trống";
            return RedirectToAction("DoiTenDangNhap", "QuanLy", new { area = "" });
        }
        /*Dành cho khách vãng lai*/
        
        public ActionResult TraCuuDonHang(string searchMaDH)
        {
            var donHang = from d in db.DONHANGs select d;
            if (!String.IsNullOrEmpty(searchMaDH))
            {
                donHang = donHang.Where(d => d.MADH.ToLower().Equals(searchMaDH.ToLower().Trim()));
                return View(donHang.ToList());
            }
            return View(new List<DONHANG>());
        }

    }
}