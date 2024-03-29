﻿using System;
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
            if (!TAIKHOAN.Instance.isLogin())
            {
                return View();
            }
            if (TAIKHOAN.Instance.PHANQUYEN.Equals("AD"))
            {
                Session["admin"] = TAIKHOAN.Instance;
                return RedirectToAction("DanhMucSanPham", "SanPham", new { area = "Admin" });
            }
            else
            {
                Session["customer"] = TAIKHOAN.Instance;
                NGUOIDUNG nGUOIDUNG = db.NGUOIDUNGs.FirstOrDefault(u => u.USERID.Equals(TAIKHOAN.Instance.USERID));
                ViewBag.HOTEN = nGUOIDUNG.HOTEN;
                return RedirectToAction("TrangChu", "TrangChu");
            }
        }
        [HttpPost]
        public ActionResult DangNhap(string username, string password)
        {
            TAIKHOAN.Instance.Login(username, password);
            if (TAIKHOAN.Instance != null && TAIKHOAN.Instance.ISENABLE)
            {
                if (TAIKHOAN.Instance.PHANQUYEN.Equals("AD"))
                {
                    Session["admin"] = TAIKHOAN.Instance;
                    return RedirectToAction("DanhMucSanPham", "SanPham", new {area = "Admin"});
                }
                else
                {
                    Session["customer"] = TAIKHOAN.Instance;
                    NGUOIDUNG nGUOIDUNG = db.NGUOIDUNGs.FirstOrDefault(u => u.USERID.Equals(TAIKHOAN.Instance.USERID));
                    ViewBag.HOTEN = nGUOIDUNG.HOTEN;
                    return RedirectToAction("TrangChu", "TrangChu");
                }
            }
            ViewBag.Fail = "Đăng nhập thất bại";
            return View();
        }

        public ActionResult DangXuat()
        {
            Session.Clear();
            TAIKHOAN.Instance.LogOut();
            return RedirectToAction("TrangChu", "TrangChu", new {area = ""});
        }
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GhiNhanDangKy([Bind(Include = "USERNAME, USERPASSWORD")] TAIKHOAN tAIKHOAN, [Bind(Include = "HOTEN, SDT, DIACHI, NGAYSINH, EMAIL, DIACHI2")] NGUOIDUNG nGUOIDUNG )
        {
            var t = db.TAIKHOANs;
            var nd = db.NGUOIDUNGs;

            var user = t.FirstOrDefault(u => u.USERNAME.ToLower().Trim().Equals(tAIKHOAN.USERNAME.Trim()));
            var email = nd.FirstOrDefault(n => n.EMAIL.Trim().Equals(nGUOIDUNG.EMAIL.Trim()));
            var sdt = nd.FirstOrDefault(n => n.SDT.Trim().Equals(nGUOIDUNG.SDT.Trim()));

            if (ModelState.IsValid)
            {
                if (user != null||email != null||sdt != null)
                {
                    return RedirectToAction("DangKy", "TaiKhoan", new { area = "" });
                }
                else
                {
                    string temp = t.ToList()[t.ToList().Count - 1].USERID;
                    string last = "";
                    for (int i = 1; i < temp.Length; i++)
                    {
                        last += temp[i];
                    }
                    int num = int.Parse(last);
                    last = "U";
                    int zero = 0;
                    if (num < 9) zero = 4;
                    else if (num < 99) zero = 3;
                    else if (num < 999) zero = 2;
                    else if (num < 9999) zero = 1;
                    else if (num < 99999) zero = 0;

                    for (int i = 0; i < zero; i++)
                    {
                        last += 0;
                    }

                    last += (num + 1);

                    tAIKHOAN.USERID = last;
                    tAIKHOAN.PHANQUYEN = "KH";
                    tAIKHOAN.ISENABLE = true;

                    nGUOIDUNG.USERID = last;
                    nGUOIDUNG.DIEMTICHLUY = 0;

                    db.NGUOIDUNGs.Add(nGUOIDUNG);
                    db.SaveChanges();
                    db.TAIKHOANs.Add(tAIKHOAN);
                    db.SaveChanges();
                    return RedirectToAction("TrangChu", "TrangChu", new { area = "" });
                }
            }

            return RedirectToAction("DangKy", "TaiKhoan", new { area = "" });
        }
    }
}