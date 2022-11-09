using Microsoft.AspNetCore.Mvc;
using ProjectPRN211.Models;

namespace ProjectPRN211.Controllers
{
    public class CartController : Controller
    {
        MyOrderContext context = new MyOrderContext();

        public IActionResult CartDetail()
        {
            var data = context.TblCarts.ToList();
            ViewBag.Carts = data;
            float sum = 0;
            foreach (var item in data)
            {
                sum += (float)item.Gia * (float)item.Soluong;
            }
            ViewBag.Sum = sum;
            return View();
        }
    }
}
