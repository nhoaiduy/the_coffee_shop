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
            //var donhang = db.DONHANGs.Distinct().OrderByDescending(d=>d.TONGITEN);
            //if (searchString != null)
            //{
            //    var ngay = db.DONHANGs.Where(d => d.TENNGUOINHAN.Trim().Equals(searchString.Trim()));
            //    List<DONHANG> list = new List<DONHANG>();
            //    foreach (var item in ngay)
            //    {
            //        DONHANG dONHANG = new DONHANG();
            //        dONHANG.MADH = item.MADH;
            //        dONHANG.TENNGUOINHAN = item.TENNGUOINHAN;
            //        dONHANG.SDT = item.SDT;
            //        dONHANG.TONGITEN = item.TONGITEN;
            //        list.Add(dONHANG);
            //    }
            //    return View(list);
            //}
            //return View(donhang);

            var users = db.NGUOIDUNGs.ToList().Where(x => x.TAIKHOAN.PHANQUYEN.Equals("KH"));
            List<decimal> counts = new List<decimal>();
            List<NGUOIDUNG> temp = new List<NGUOIDUNG>();

            foreach (var user in users)
            {
                var donHangs = from d in db.DONHANGs.Where(d=>d.MATT.Equals(6) || d.MATT.Equals(7)).ToList()
                               where d.SDT == user.SDT
                               select d;
                if (donHangs.ToList().Count > 0)
                {
                    counts.Add(donHangs.ToList().Sum(d=>d.TONGITEN));
                    temp.Add(user);
                }

            }
            foreach (var user in users)
            {
                bool flag = false;
                foreach (var u in temp)
                {
                    if (u.SDT == user.SDT)
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    temp.Add(user);
                    counts.Add(0);
                }
            }
            List<Tuple<NGUOIDUNG, decimal>> thongKe = new List<Tuple<NGUOIDUNG, decimal>>();
            for (int i = 0; i < counts.Count; i++)
            {
                thongKe.Add(new Tuple<NGUOIDUNG, decimal>(temp[i], counts[i]));
            }
            var ordered = thongKe.OrderByDescending(x => x.Item2).ToList();
            ViewBag.ThongKe = ordered;
            return View();
        }
    }
}