using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanNuocUong_TheCoffeeShop.Models;

namespace WebBanNuocUong_TheCoffeeShop.Areas.Admin.Controllers
{
    public class ThongKeController : Controller
    {
        thecoffeeshopEntities db = new thecoffeeshopEntities();
        // GET: Admin/ThongKe
        public ActionResult DanhMucThongKe(string searchString)
        {
            var donhang = db.DONHANGs.ToList();
            if (searchString != null)
            {
                var ngay = db.sp_ThongKeTheoNgay(searchString);
                List<DONHANG> list = new List<DONHANG>();
                foreach (var item in ngay)
                {
                    DONHANG dONHANG = new DONHANG();
                    dONHANG.MADH = item.MADH;
                    dONHANG.TENNGUOINHAN = item.TENNGUOINHAN;
                    dONHANG.SDT = item.SDT;
                    dONHANG.TONGITEN = item.TONGITEN;
                    list.Add(dONHANG);
                }
                return View(list);
            }
            return View(donhang);
        }
        public ActionResult ThongKeTheoThang(string searchString)
        {
            var donhang = db.DONHANGs.ToList();
            if (searchString != null)
            {
                var thang = db.DONHANGs.Where(d => d.NGAYLAP.Value.Month.ToString().Equals(searchString.Trim())).ToList();
                return View(thang);
            }
            return View(donhang);
        }
        public ActionResult ThongKeTheoNam(string searchString)
        {
            var donhang = db.DONHANGs.ToList();
            if (searchString != null)
            {
                var nam = db.DONHANGs.Where(d => d.NGAYLAP.Value.Year.ToString().Equals(searchString.Trim())).ToList();
                return View(nam);
            }
            return View(donhang);
        }
        public ActionResult ThongKeTheoNguoiDung(string searchString)
        {
            var donhang = db.DONHANGs.ToList();
            if (searchString != null)
            {
                var ngay = db.sp_ThongKeTheoNguoiDung(searchString.Trim());
                List<DONHANG> list = new List<DONHANG>();
                foreach (var item in ngay)
                {
                    DONHANG dONHANG = new DONHANG();
                    dONHANG.MADH = item.MADH;
                    dONHANG.TENNGUOINHAN = item.TENNGUOINHAN;
                    dONHANG.SDT = item.SDT;
                    dONHANG.TONGITEN = item.TONGITEN;
                    list.Add(dONHANG);
                }
                return View(list);
            }
            return View(donhang);
        }
    }
}