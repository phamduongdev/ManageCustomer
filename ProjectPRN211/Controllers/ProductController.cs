using Microsoft.AspNetCore.Mvc;
using ProjectPRN211.Models;
using System.Globalization;

namespace ProjectPRN211.Controllers
{
    public class ProductController : Controller
    {
        MyOrderContext context = new();
        public IActionResult ProductDetail(string maHang)
        {
            ViewBag.Size = context.TblCarts.ToList().Count;
            TblMatHang matHang = context.TblMatHangs.FirstOrDefault(item => item.MaHang.Equals(maHang));
            var data = context.TblMatHangs.Where(item => item.CategoryId == matHang.CategoryId && item.TenHang != matHang.TenHang).ToList();
            ViewBag.Related = data;
            return View(matHang);
        }

        public JsonResult AddToCart(string maHang, string tenHang, string dvt, string gia, string soLuong)
        {
            TblCart cart = new TblCart
            {
                MaHang = maHang,
                TenHang = tenHang,
                Dvt = dvt,
                Gia = float.Parse(gia, CultureInfo.InvariantCulture.NumberFormat),
                Soluong = int.Parse(soLuong)
            };
            try
            {
                var carts = context.TblCarts.Where(item => item.MaHang.Equals(maHang)).ToList();
                if (carts.Count() > 0)
                {
                    carts[0].Soluong = carts[0].Soluong + int.Parse(soLuong);
                    context.SaveChanges();
                }
                else
                {
                    context.TblCarts.Add(cart);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            var data = context.TblCarts.Count();
            return Json(data);
        }
    }
}
