using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanNuocUong_TheCoffeeShop.Models;

namespace WebBanNuocUong_TheCoffeeShop.Areas.Admin.Controllers
{
    public class SanPhamController : Controller
    {
        thecoffeeshopEntities db = new thecoffeeshopEntities();
        // GET: Admin/SanPham
        public ActionResult DanhMucSanPham(string searchString, string categoryName)
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("DangNhap", "TaiKhoan", new {area = "Admin"});
            }
            var sanPhamList = db.sp_DanhSachSanPham(categoryName);
            List<SANPHAM> list = new List<SANPHAM>();
            foreach(var item in sanPhamList)
            {
                SANPHAM sanPham = new SANPHAM();
                sanPham.MASP = item.MASP;
                sanPham.TENSP = item.TENSP;
                sanPham.ANHSP = item.ANHSP;
                sanPham.GIASP = item.GIASP;
                sanPham.SOLUONG = item.SOLUONG;
                sanPham.MALOAISP = item.MALOAISP;
                list.Add(sanPham);
            }
            ViewBag.MALOAISP = new SelectList(db.LOAISANPHAMs, "MALOAISP", "TENLOAISP");
            var temp = db.sp_HienThiDanhSachLoaiSanPham();
            List<LOAISANPHAM> lOAISANPHAMs = new List<LOAISANPHAM>();
            foreach (var item in temp)
            {
                LOAISANPHAM lOAISANPHAM = new LOAISANPHAM();
                lOAISANPHAM.MALOAISP = item.MALOAISP;
                lOAISANPHAM.TENLOAISP = item.TENLOAISP;
                lOAISANPHAMs.Add(lOAISANPHAM);
            }
            ViewBag.LOAISP = lOAISANPHAMs;
            return View(list);
        }
        [HttpPost]
        public ActionResult ThemSanPham([Bind(Include = "TENSP, GIASP, ANHSP, MOTASP, MALOAISP, SOLUONG")] SANPHAM sANPHAM, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                byte[] anh = new byte[image.ContentLength];
                image.InputStream.Read(anh, 0, image.ContentLength);
                string fileName = System.IO.Path.GetFileName(image.FileName);
                string urlName = Server.MapPath("~/Image/" + fileName);
                image.SaveAs(urlName);

                sANPHAM.ANHSP = "~/Image/" + fileName;
            }
            if (ModelState.IsValid)
            {
                var sanPhams = db.SANPHAMs.Where(s => s.MALOAISP.Equals(sANPHAM.MALOAISP));
                
                if(sanPhams.ToList().Count > 0)
                {
                    string temp = sanPhams.ToList()[sanPhams.ToList().Count - 1].MASP;
                    string last = "";
                    for (int i = 2; i < temp.Length; i++)
                    {
                        last += temp[i];
                    }
                    int num = int.Parse(last);
                    last = sANPHAM.MALOAISP;
                    int zero = 0;
                    if (num < 10) zero = 3;
                    else if (num < 100) zero = 2;
                    else if (num < 1000) zero = 1;
                    else if (num < 10000) zero = 0;

                    for (int i = 0; i < zero; i++)
                    {
                        last += 0;
                    }

                    last += (num + 1);
                    sANPHAM.MASP = last;
                }
                else
                {
                    sANPHAM.MASP = sANPHAM.MALOAISP+"0001";
                }
                

                db.SANPHAMs.Add(sANPHAM);
                db.SaveChanges();
                return RedirectToAction("DanhMucSanPham");
            }
            ViewBag.MALOAISP = new SelectList(db.LOAISANPHAMs, "MALOAISP", "TENLOAISP", sANPHAM.MALOAISP);
            return View("DanhMucSanPham");
        }

        public ActionResult ChiTiet(string productName)
        {
            var data = db.sp_ChiTietSanPham(productName);
            SANPHAM sanPham = new SANPHAM();
            foreach (var item in data)
            {
                sanPham.MASP = item.MASP;
                sanPham.TENSP = item.TENSP;
                sanPham.ANHSP = item.ANHSP;
                sanPham.GIASP = item.GIASP;
                sanPham.SOLUONG = item.SOLUONG;
                sanPham.MALOAISP = item.MALOAISP;
                sanPham.MOTASP = item.MOTASP;
                break;
            }
            ViewBag.MALOAISP = new SelectList(db.LOAISANPHAMs, "MALOAISP", "TENLOAISP", sanPham.MALOAISP);
            return View(sanPham);
        }

        [HttpPost]
        public ActionResult ChinhSuaSanPham([Bind(Include = "MASP,TENSP, GIASP, ANHSP, MOTASP, MALOAISP, SOLUONG")] SANPHAM sANPHAM, HttpPostedFileBase image)
        {
            SANPHAM sANPHAM1 = db.SANPHAMs.FirstOrDefault(s => s.MASP.Equals(sANPHAM.MASP));
            if (ModelState.IsValid)
            {
                if(sANPHAM1 != null)
                {
                    if (image != null && image.ContentLength > 0)
                    {
                        byte[] anh = new byte[image.ContentLength];
                        image.InputStream.Read(anh, 0, image.ContentLength);
                        string fileName = System.IO.Path.GetFileName(image.FileName);
                        string urlName = Server.MapPath("~/Image/" + fileName);
                        image.SaveAs(urlName);

                        sANPHAM1.ANHSP = "~/Image/" + fileName;
                    }
                    if (sANPHAM.TENSP != null)
                    {
                        sANPHAM1.TENSP = sANPHAM.TENSP;
                    }
                    if (sANPHAM.GIASP.ToString() != null)
                    {
                        sANPHAM1.GIASP = sANPHAM.GIASP;
                    }
                    if (sANPHAM.SOLUONG.ToString() != null)
                    {
                        sANPHAM1.SOLUONG = sANPHAM.SOLUONG;
                    }
                    if (sANPHAM.MOTASP != null)
                    {
                        sANPHAM1.MOTASP = sANPHAM.MOTASP;
                    }
                    if (sANPHAM.MALOAISP != null)
                    {
                        sANPHAM1.MALOAISP = sANPHAM.MALOAISP;
                    }
                    db.Entry(sANPHAM1).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("ChiTiet", new { @productName = sANPHAM1.TENSP });
                }
            }
            //ViewBag.MALOAISP = new SelectList(db.LOAISANPHAMs, "MALOAISP", "TENLOAISP", sANPHAM.MALOAISP);
            return View("ChiTiet", new { @productName = sANPHAM.TENSP });
        }

        [HttpPost]
        public ActionResult XoaSanPham(string MASP)
        {
            SANPHAM sANPHAM = db.SANPHAMs.FirstOrDefault(s => s.MASP.ToString().Equals(MASP));
            db.SANPHAMs.Remove(sANPHAM);
            db.SaveChanges();
            return RedirectToAction("DanhMucSanPham");
        }
        public ActionResult TimKiem()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TimKiem(string TENSP)
        {
            var sanPhams = db.SANPHAMs.Where(s => s.TENSP.ToLower().Trim().Contains(TENSP.ToLower().Trim()));
            ViewBag.SEARCHSTRING = TENSP;
            return View(sanPhams.ToList());
        }
    }
}