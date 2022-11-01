using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanNuocUong_TheCoffeeShop.Models;

namespace WebBanNuocUong_TheCoffeeShop.Areas.Admin.Controllers
{
    public class KhuyenMaiController : Controller
    {
        thecoffeeshopEntities db = new thecoffeeshopEntities();
        // GET: Admin/KhuyenMai
        public ActionResult DanhSachKhuyenMai()
        {
            var khuyenMaiList = db.sp_XemKhuyenMai();
            List<KHUYENMAI> list = new List<KHUYENMAI>();
            foreach (var item in khuyenMaiList)
            {
                KHUYENMAI khuyenMai = new KHUYENMAI();
                khuyenMai.MAKM = item.MAKM;
                khuyenMai.TENKM = item.TENKM;
                khuyenMai.SOTIENGIAM = item.SOTIENGIAM;
                khuyenMai.ANHKM = item.ANHKM;
                khuyenMai.NGAYHETHAN = item.NGAYHETHAN;
                khuyenMai.SOLUONG = item.SOLUONG;
                khuyenMai.DIEUKIEN = item.DIEUKIEN;
                list.Add(khuyenMai);
            }
            return View(list);
        }
        public ActionResult ChiTiet(string TenKM)
        {
            KHUYENMAI khuyenMai = db.KHUYENMAIs.FirstOrDefault(k => k.TENKM.Equals(TenKM));
            //KHUYENMAI km = new KHUYENMAI();
            //foreach (var item in khuyenMai)
            //{
            //    km.MAKM = item.MAKM;
            //    km.TENKM = item.TENKM;
            //    km.SOTIENGIAM = item.SOTIENGIAM;
            //    km.ANHKM = item.ANHKM;
            //    km.NGAYHETHAN = item.NGAYHETHAN;
            //    km.SOLUONG = item.SOLUONG;
            //    km.DIEUKIEN = item.DIEUKIEN;
            //    km.MOTAKM = item.MOTAKM;
            //    break;
            //}
            return View(khuyenMai);
        }
        [HttpPost]
        public ActionResult ThemKhuyenMai([Bind(Include = "TENKM, ANHKM, DIEUKIEN, SOTIENGIAM, SOLUONG, MOTAKM, NGAYHETHAN")] KHUYENMAI kHUYENMAI, HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                byte[] anh = new byte[image.ContentLength];
                image.InputStream.Read(anh, 0, image.ContentLength);
                string fileName = System.IO.Path.GetFileName(image.FileName);
                string urlName = Server.MapPath("~/Image/" + fileName);
                image.SaveAs(urlName);

                kHUYENMAI.ANHKM = "~/Image/" + fileName;
            }
            else kHUYENMAI.ANHKM = "https://thumbs.dreamstime.com/b/halloween-sale-21599574.jpg";
            if (ModelState.IsValid)
            {
                var khuyenMais = db.KHUYENMAIs.ToList();

                if (khuyenMais.ToList().Count > 0)
                {
                    string temp = khuyenMais.ToList()[khuyenMais.ToList().Count - 1].MAKM;                     
                    string last = "";
                    for (int i = 2; i < temp.Length; i++)
                    {
                        last += temp[i]; // last = 001
                    }
                    int num = int.Parse(last);
                    int zero = 0;
                    if (num < 10) zero = 2;
                    else if (num < 100) zero = 1;
                    else if (num < 1000) zero = 0;
                    for (int i = 0; i < zero; i++)
                    {
                        last += 0;
                    }
                    last += (num + 1);
                    kHUYENMAI.MAKM = "KM" + last;
                }
                else
                {
                    kHUYENMAI.MAKM = "KM001";
                }
                //if (kHUYENMAI == null)
                //{
                //    kHUYENMAI.MAKM = "KM003";
                //}             
                    db.KHUYENMAIs.Add(kHUYENMAI);
                    db.SaveChanges();
                    return RedirectToAction("DanhSachKhuyenMai", "KhuyenMai", new {Area = "Admin"});                                
            }
            return RedirectToAction("DanhSachKhuyenMai", "KhuyenMai", new { Area = "Admin" });
        }
        [HttpPost]
        public ActionResult ChinhSuaKhuyenMai([Bind(Include = "MAKM,TENKM, ANHKM, MOTAKM, SOTIENGIAM, DIEUKIEN, SOLUONG, NGAYHETHAN")] KHUYENMAI kHUYENMAI, HttpPostedFileBase image)
        {
            KHUYENMAI kHUYENMAI1 = db.KHUYENMAIs.FirstOrDefault(s => s.MAKM.Equals(kHUYENMAI.MAKM));
            if (ModelState.IsValid)
            {
                if (kHUYENMAI1 != null)
                {
                    if (image != null && image.ContentLength > 0)
                    {
                        byte[] anh = new byte[image.ContentLength];
                        image.InputStream.Read(anh, 0, image.ContentLength);
                        string fileName = System.IO.Path.GetFileName(image.FileName);
                        string urlName = Server.MapPath("~/Image/" + fileName);
                        image.SaveAs(urlName);
                        kHUYENMAI.ANHKM = "~/Image/" + fileName;
                    }
                    if (kHUYENMAI.TENKM != null)
                    {
                        kHUYENMAI1.TENKM = kHUYENMAI.TENKM;
                    }
                    if (kHUYENMAI.SOTIENGIAM.ToString() != null)
                    {
                        kHUYENMAI1.SOTIENGIAM = kHUYENMAI.SOTIENGIAM;
                    }
                    if (kHUYENMAI.SOLUONG.ToString() != null)
                    {
                        kHUYENMAI1.SOLUONG = kHUYENMAI.SOLUONG;
                    }
                    if (kHUYENMAI.DIEUKIEN.ToString() != null)
                    {
                        kHUYENMAI1.DIEUKIEN = kHUYENMAI.DIEUKIEN;
                    }
                    if (kHUYENMAI.NGAYHETHAN != null)
                    {
                        kHUYENMAI1.NGAYHETHAN = kHUYENMAI.NGAYHETHAN;
                    }
                    if (kHUYENMAI.MOTAKM != null)
                    {
                        kHUYENMAI1.MOTAKM = kHUYENMAI.MOTAKM;
                    }
                    db.Entry(kHUYENMAI1).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("ChiTiet","KhuyenMai", new { @TenKM = kHUYENMAI1.TENKM, area = "Admin" });
                }
            }
            return RedirectToAction("ChiTiet", "KhuyenMai", new { @TenKM = kHUYENMAI1.TENKM, area = "Admin" });
        }
        [HttpPost]
        public ActionResult XoaKhuyenMai(string MAKM)
        {
            KHUYENMAI kHUYENMAI = db.KHUYENMAIs.FirstOrDefault(k => k.MAKM.ToString().Equals(MAKM));
            db.KHUYENMAIs.Remove(kHUYENMAI);
            db.SaveChanges();
            return RedirectToAction("DanhSachKhuyenMai", "KhuyenMai", new { Area = "Admin" });
        }
    }
}