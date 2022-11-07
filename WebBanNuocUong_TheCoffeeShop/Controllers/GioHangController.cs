using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanNuocUong_TheCoffeeShop.Models;

namespace WebBanNuocUong_TheCoffeeShop.Controllers
{
    public class GioHangController : Controller
    {
        thecoffeeshopEntities db = new thecoffeeshopEntities();
        public ActionResult ThemGioHang(string MASP, int soLuong)
        {
            SANPHAM sANPHAM = db.SANPHAMs.FirstOrDefault(s => s.MASP.Equals(MASP));
            if(sANPHAM != null)
            {
                if (Session["customer"] == null)
                {
                    List<Tuple<SANPHAM, int>> cart = new List<Tuple<SANPHAM, int>>();
                    if (Session["cart"] != null)
                    {
                        cart = (List<Tuple<SANPHAM, int>>)Session["cart"];
                    }
                    if (cart.FindIndex(s => s.Item1.MASP.Equals(MASP)) > -1)
                    {
                        int index = cart.FindIndex(s => s.Item1.MASP.Equals(MASP));
                        cart[index] = Tuple.Create(sANPHAM, soLuong);
                    }
                    else
                    {
                        cart.Add(Tuple.Create(sANPHAM, soLuong));
                    }
                    Session["cart"] = cart;
                }
                else
                {
                    List<CTDONHANG> cart = new List<CTDONHANG>();
                    var user = Session["customer"] as TAIKHOAN;
                    DONHANG dONHANG = db.DONHANGs.FirstOrDefault(d => d.MADH.Equals(user.USERID + "0000"));
                    if(dONHANG == null)
                    {
                        dONHANG = new DONHANG();
                        dONHANG.MADH = user.USERID + "0000";
                        dONHANG.MATT = 1;
                        dONHANG.TENNGUOINHAN = user.NGUOIDUNG.HOTEN;
                        dONHANG.SDT = user.NGUOIDUNG.SDT;
                        dONHANG.DIACHI = user.NGUOIDUNG.DIACHI;
                        dONHANG.TAMTINH = 0;
                        dONHANG.TONGITEN = 0;
                        db.DONHANGs.Add(dONHANG);
                        db.SaveChanges();
                    }
                    cart = db.CTDONHANGs.Where(d => d.MADH.Equals(user.USERID + "0000")).ToList();
                    if (cart.FindIndex(s => s.MASP.Equals(MASP)) > -1)
                    {
                        int index = cart.FindIndex(s => s.MASP.Equals(MASP));
                        cart[index].SOLUONG = soLuong;
                        db.Entry(cart[index]).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        CTDONHANG cTDONHANG = new CTDONHANG();
                        cTDONHANG.MASP = MASP;
                        cTDONHANG.MADH = user.USERID + "0000";
                        cTDONHANG.SOLUONG = soLuong;
                        cTDONHANG.THANHTIEN = sANPHAM.GIASP * soLuong;
                        db.CTDONHANGs.Add(cTDONHANG);
                        cart.Add(cTDONHANG);
                    }
                    dONHANG.TAMTINH = cart.Sum(d => d.THANHTIEN);
                    dONHANG.TONGITEN = cart.Sum(d => d.THANHTIEN);
                    db.Entry(dONHANG).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("GioHang", "GioHang");
            }
            return RedirectToAction("GioHang", "GioHang");
        }

        public ActionResult GioHang()
        {
            if (Session["customer"] == null)
            {
                ViewBag.Cart = Session["cart"];
            }
            else
            {
                var user = Session["customer"] as TAIKHOAN;
                ViewBag.Cart = db.CTDONHANGs.Where(d => d.MADH.Equals(user.USERID + "0000")).ToList();
            }
            return View();
        }

        public ActionResult XoaChiTiet(string MASP)
        {
            if (Session["customer"] == null)
            {
                List<Tuple<SANPHAM, int>> cart = (List<Tuple<SANPHAM, int>>)Session["cart"];
                cart.RemoveAt(cart.FindIndex(c => c.Item1.MASP.Equals(MASP)));
                Session["cart"] = cart;
            }
            else
            {
                var user = Session["customer"] as TAIKHOAN;
                CTDONHANG cTDONHANG = db.CTDONHANGs.FirstOrDefault(c => c.MASP.Equals(MASP) && c.MADH.Equals(user.USERID + "0000"));
                db.CTDONHANGs.Remove(cTDONHANG);
                db.SaveChanges();
            }
            return RedirectToAction("GioHang", "GioHang");
        }
    }
}