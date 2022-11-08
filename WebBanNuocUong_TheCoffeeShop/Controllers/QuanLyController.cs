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
                nGUOIDUNG1.DIACHI2 = nGUOIDUNG1.DIACHI2;
                db.Entry(nGUOIDUNG1).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ThongTinCaNhan", "QuanLy", new { area = "" });
            }
            ViewBag.Fail = "Thông tin không được để trống";
            return RedirectToAction("ThongTinCaNhan", "QuanLy", new { area = "" });
        }
        public ActionResult DanhSachDonHang(string tinhTrang)
        {
            var user = Session["customer"] as TAIKHOAN;
            NGUOIDUNG nGUOIDUNG = db.NGUOIDUNGs.FirstOrDefault(n => n.USERID.Equals(user.USERID));
            var donHangs = db.DONHANGs.OrderByDescending(d => d.MADH).Where(d => d.SDT.Equals(nGUOIDUNG.SDT) && d.DIACHI.Equals(nGUOIDUNG.DIACHI) && d.TENNGUOINHAN.Equals(nGUOIDUNG.HOTEN)).ToList();
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
        /*Dành cho khách vãng lai*/
        public ActionResult TraCuuDonHang()
        {
            return View();
        }
    }
}