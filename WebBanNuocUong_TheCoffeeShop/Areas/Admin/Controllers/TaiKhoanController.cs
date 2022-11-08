﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.UI;
using WebBanNuocUong_TheCoffeeShop.Models;

namespace WebBanNuocUong_TheCoffeeShop.Areas.Admin.Controllers
{
    public class TaiKhoanController : Controller
    {
        thecoffeeshopEntities db = new thecoffeeshopEntities();

        public ActionResult DanhSachTaiKhoan(string hoTen, string quyen)
        {
            var users = db.NGUOIDUNGs.ToList();
            if (!string.IsNullOrEmpty(hoTen))
            {
                users = users.Where(u => u.HOTEN.ToLower().Contains(hoTen.Trim().ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(quyen))
            {
                if (quyen.Equals("NV Quản trị"))
                {
                    quyen = "AD";
                }
                else
                {
                    quyen = "KH";
                }
                var accounts = db.TAIKHOANs.Where(a => a.PHANQUYEN.Equals(quyen));
                var temp = from u in users
                           from a in accounts
                           where u.USERID == a.USERID
                           select u;
                users = temp.ToList();
            }
            List<string> quyens = new List<string>();
            quyens.Add("");
            quyens.Add("NV Quản trị");
            quyens.Add("Khách hàng");
            ViewBag.QUYEN = new SelectList(quyens);
            return View(users);
        }

        [HttpPost]
        public ActionResult ThemNVMoi([Bind(Include = "HOTEN, NGAYSINH, DIACHI, DIACHI2, SDT, EMAIL")] NGUOIDUNG nGUOIDUNG)
        {
            if (ModelState.IsValid)
            {
                db.sp_ThemNVQuanTri(nGUOIDUNG.HOTEN, nGUOIDUNG.NGAYSINH, nGUOIDUNG.DIACHI, nGUOIDUNG.DIACHI2, nGUOIDUNG.SDT, nGUOIDUNG.EMAIL);
            }
            var acc = db.TAIKHOANs.FirstOrDefault(t => t.USERNAME == nGUOIDUNG.SDT);
            return RedirectToAction("DanhSachTaiKhoan", "TaiKhoan", new {area = "Admin"});
        }

        public ActionResult ChiTietNguoiDung(string nguoiDung)
        {
            var user = db.NGUOIDUNGs.FirstOrDefault(u => u.USERID.Equals(nguoiDung));
            var acc = db.TAIKHOANs.FirstOrDefault(a => a.USERID.Equals(nguoiDung));
            ViewBag.TAIKHOAN = acc;
            return View(user);
        }

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
                nGUOIDUNG1.DIACHI2 = nGUOIDUNG.DIACHI2;
                db.Entry(nGUOIDUNG1).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ThongTinTaiKhoan", "TaiKhoan", new { area = "Admin" });
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