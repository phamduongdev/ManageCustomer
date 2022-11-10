using Microsoft.AspNetCore.Mvc;
using ProjectPRN211.Models;

namespace ProjectPRN211.Controllers
{
    public class OrderController : Controller
    {
        MyOrderContext context = new();
        public IActionResult OrderHistory()
        {
            var data1 = context.TblHoaDons.ToList();
            ViewBag.Orders = data1;
            var data2 = context.TblChiTietHds.ToList();
            ViewBag.ChiTietHds = data2;
            var data3 = context.TblMatHangs.ToList();
            ViewBag.MatHangs = data3;
            ViewBag.Size = context.TblCarts.ToList().Count;
            return View();
        }
    }
}
