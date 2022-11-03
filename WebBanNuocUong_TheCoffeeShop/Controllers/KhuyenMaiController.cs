using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanNuocUong_TheCoffeeShop.Models;

namespace WebBanNuocUong_TheCoffeeShop.Controllers
{
    public class KhuyenMaiController : Controller
    {
        thecoffeeshopEntities db = new thecoffeeshopEntities();
        // GET: KhuyenMai
        //public ActionResult DanhSachKhuyenMai()
        //{
        //    var khuyenMaiList = db.sp_XemKhuyenMai();
        //    List<KHUYENMAI> list = new List<KHUYENMAI>();
        //    foreach(var item in khuyenMaiList)
        //    {
        //        KHUYENMAI khuyenMai=new KHUYENMAI();
        //        khuyenMai.MAKM = item.MAKM;
        //        khuyenMai.TENKM=item.TENKM;
        //        khuyenMai.SOTIENGIAM = item.SOTIENGIAM;
        //        khuyenMai.ANHKM = item.ANHKM;
        //        khuyenMai.NGAYHETHAN=item.NGAYHETHAN;
        //        khuyenMai.SOLUONG = item.SOLUONG;
        //        khuyenMai.DIEUKIEN = item.DIEUKIEN;
        //        list.Add(khuyenMai);
        //    }
        //    return View(list);
        //}
    }
}