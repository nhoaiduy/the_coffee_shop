using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanNuocUong_TheCoffeeShop.Models;

namespace WebBanNuocUong_TheCoffeeShop.Controllers
{
    public class TimKiemController : Controller
    {
        thecoffeeshopEntities db = new thecoffeeshopEntities();
        // GET: TimKiem
        public ActionResult TimKiem(string searchString)
        {
            var sanPham = from s in db.SANPHAMs.ToList() select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                sanPham = sanPham.Where(s => s.TENSP.ToLower().Contains(searchString.ToLower()));
            }
            return View(sanPham.ToList());
        }
    }
}