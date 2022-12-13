﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using WebBanNuocUong_TheCoffeeShop.Models;

namespace WebBanNuocUong_TheCoffeeShop.Controllers
{
    public class DatHangController : Controller
    {
        thecoffeeshopEntities db = new thecoffeeshopEntities();
        // GET: DatHang
        public ActionResult DatHang()
        {
            if (Session["customer"] == null)
            {
                if (Session["cart"] == null)
                {
                    return RedirectToAction("GioHang", "GioHang");
                }
                ViewBag.Cart = Session["cart"];
            }
            else
            {
                var user = Session["customer"] as TAIKHOAN;
                List<CTDONHANG> cart = db.CTDONHANGs.Where(d => d.MADH.Equals(user.USERID + "0000")).ToList();
                if(cart.Count == 0) 
                {
                    return RedirectToAction("GioHang", "GioHang");
                }
                ViewBag.Cart = cart;
            }
            return View();
        }


        [HttpPost]
        public ActionResult DatHang(string hoTen, string sdt, string diaChi, string MAKM)
        {
            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(diaChi) || string.IsNullOrEmpty(hoTen)) 
            {
                return View();
            }
            DONHANG dONHANG = new DONHANG();
            string day, month, year;
            DateTime date = DateTime.Now;
            day = int.Parse(date.Day.ToString()) > 9 ? date.Day.ToString() : "0" + date.Day.ToString();
            month = int.Parse(date.Month.ToString())>9? date.Month.ToString():"0"+ date.Month.ToString();
            year = date.Year.ToString();
            string madh = year[2].ToString() + year[3].ToString() + month + day;
            List<DONHANG> temp = db.DONHANGs.Where(d => d.MADH.Contains(madh)).ToList();
            dONHANG.MADH = madh;
            if (temp.Count > 0)
            {
                string macu = temp[temp.Count - 1].MADH;
                madh = "";
                for (int i = 6; i < macu.Length; i++)
                {
                    madh += macu[i];
                }
                int count = int.Parse(madh) + 1;
                if (count < 10)
                {
                    dONHANG.MADH += "000" + count;
                }
                else if (count < 100)
                {
                    dONHANG.MADH += "00" + count;
                }
                else if (count < 1000)
                {
                    dONHANG.MADH += "0" + count;
                }
                else if (count < 10000)
                {
                    dONHANG.MADH += count;
                }
            }
            else
            {
                dONHANG.MADH += "0001";
            }
            
            dONHANG.SDT = sdt;
            dONHANG.TENNGUOINHAN = hoTen;
            dONHANG.DIACHI = diaChi;
            decimal giam = 0;   
            dONHANG.THOIGIANGIAOHANG = date.AddMinutes(30);
            if (!string.IsNullOrEmpty(MAKM))
            {
                if(db.KHUYENMAIs.FirstOrDefault(k => k.MAKM.Equals(MAKM)) != null)
                {
                    dONHANG.MAKM = MAKM;    
                    giam = db.KHUYENMAIs.FirstOrDefault(k => k.MAKM.Equals(MAKM)).SOTIENGIAM;
                }
            }
            dONHANG.MATT = 2;
            dONHANG.NGAYLAP = date;
            
            dONHANG.SOTIENGIAMKM = giam;
            if (Session["customer"] == null)
            {
                List<Tuple<SANPHAM, int>> cart = (List<Tuple<SANPHAM, int>>)Session["cart"];
                foreach(var item in cart)
                {
                    SANPHAM sANPHAM = db.SANPHAMs.FirstOrDefault(s => s.MASP.Equals(item.Item1.MASP));
                    if(sANPHAM.SOLUONG < item.Item2)
                    {
                        return View();
                    }
                }
                decimal sum = cart.Sum(d => d.Item1.GIASP * d.Item2);
                dONHANG.TAMTINH = sum;
                dONHANG.TONGITEN = sum - giam;
                db.DONHANGs.Add(dONHANG);
                db.SaveChanges();
                foreach(var item in cart)
                {
                    CTDONHANG cTDONHANG = new CTDONHANG();
                    cTDONHANG.MASP = item.Item1.MASP;
                    cTDONHANG.SOLUONG = item.Item2;
                    cTDONHANG.MADH = dONHANG.MADH;
                    cTDONHANG.THANHTIEN = item.Item1.GIASP * item.Item2;
                    SANPHAM sANPHAM = db.SANPHAMs.FirstOrDefault(s => s.MASP.Equals(item.Item1.MASP));
                    sANPHAM.SOLUONG -= item.Item2;
                    if (sANPHAM.SOLUONG == 0)
                    {
                        sANPHAM.ISENABLE = false;
                    }
                    db.CTDONHANGs.Add(cTDONHANG);
                    db.Entry(sANPHAM).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                Session["cart"] = null;
            }
            else
            {
                var user = Session["customer"] as TAIKHOAN;
                List<CTDONHANG> cart = db.CTDONHANGs.Where(d => d.MADH.Equals(user.USERID + "0000")).ToList();
                foreach (var item in cart)
                {
                    SANPHAM sANPHAM = db.SANPHAMs.FirstOrDefault(s => s.MASP.Equals(item.MASP));
                    if (sANPHAM.SOLUONG < item.SOLUONG)
                    {
                        return View();
                    }
                }
                decimal sum = cart.Sum(d => d.THANHTIEN);
                dONHANG.TAMTINH = sum;
                dONHANG.TONGITEN = sum - giam;
                db.DONHANGs.Add(dONHANG);
                NGUOIDUNG nGUOIDUNG = db.NGUOIDUNGs.FirstOrDefault(n => n.USERID.Equals(user.USERID));
                nGUOIDUNG.DIEMTICHLUY +=(int)(dONHANG.TONGITEN / 1000);
                db.Entry(nGUOIDUNG).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                foreach (var item in cart)
                {
                    item.MADH = dONHANG.MADH;
                    SANPHAM sANPHAM = db.SANPHAMs.FirstOrDefault(s => s.MASP.Equals(item.MASP));
                    sANPHAM.SOLUONG -= item.SOLUONG;
                    if (sANPHAM.SOLUONG == 0)   
                    {
                        sANPHAM.ISENABLE = false;
                    }
                    db.Entry(sANPHAM).State = System.Data.Entity.EntityState.Modified;
                    db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                db.DONHANGs.Remove(db.DONHANGs.FirstOrDefault(d => d.MADH.Equals(user.USERID + "0000")));
                db.SaveChanges();
            }
            return RedirectToAction("ChiTietDonHang", "DonHang", new {area = "", MADH = dONHANG.MADH});
        }

        public ActionResult DatNgay(string MASP, int soLuong)
        {
            SANPHAM sANPHAM = db.SANPHAMs.FirstOrDefault(s => s.MASP.Equals(MASP));
            if (sANPHAM != null)
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
                    if (dONHANG == null)
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
                return RedirectToAction("DatHang", "DatHang");
            }
            return RedirectToAction("ChiTietSanPham", "SanPham");
        }
    }
}