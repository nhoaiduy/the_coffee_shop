using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanNuocUong_TheCoffeeShop.Models;

namespace WebBanNuocUong_TheCoffeeShop.Controllers
{
    public class SanPhamController : Controller
    {
        thecoffeeshopEntities db = new thecoffeeshopEntities();

        // GET: CusSanPham
        public ActionResult DanhMucSanPham(string categoryName)
        {
            var sanPhamList = db.sp_DanhSachSanPham(categoryName);
            List<SANPHAM> list = new List<SANPHAM>();
            foreach (var item in sanPhamList)
            {
                if (item.ISENABLE)
                {
                    SANPHAM sanPham = new SANPHAM();
                    sanPham.MASP = item.MASP;
                    sanPham.TENSP = item.TENSP;
                    sanPham.ANHSP = item.ANHSP;
                    sanPham.GIASP = item.GIASP;
                    sanPham.SOLUONG = item.SOLUONG;
                    sanPham.MALOAISP = item.MALOAISP;
                    sanPham.ISENABLE = item.ISENABLE;
                    list.Add(sanPham);
                }
            }
            ViewBag.MALOAISP = new SelectList(db.LOAISANPHAMs, "MALOAISP", "TENLOAISP");
            ViewBag.LOAISP = db.LOAISANPHAMs.ToList();
            return View(list);
        }
        public ActionResult ChiTietSanPham(string productName)
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
            return View(sanPham);
        }
    }
}