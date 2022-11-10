using Microsoft.AspNetCore.Mvc;
using ProjectPRN211.Models;
using System.Text.Json.Nodes;

namespace ProjectPRN211.Controllers
{
    public class CartController : Controller
    {
        MyOrderContext context = new MyOrderContext();

        public IActionResult CartDetail()
        {
            var data = context.TblCarts.ToList();
            ViewBag.Carts = data;
            ViewBag.Size = data.Count;
            float sum = 0;
            foreach (var item in data)
            {
                sum += (float)item.Gia * (float)item.Soluong;
            }
            ViewBag.Sum = sum;
            return View();
        }

        public JsonResult CheckOut(string data_order)
        {
            var objects = JsonArray.Parse(data_order);
            var maHD = 0;
            try
            {
                TblKhachHang khachHang = new TblKhachHang
                {
                    MaKh = objects[0]["maKH"].ToString(),
                    TenKh = objects[0]["tenKH"].ToString()
                };
                context.TblKhachHangs.Add(khachHang);
                context.SaveChanges();
                TblHoaDon hoaDon = new TblHoaDon()
                {
                    MaKh = objects[0]["maKH"].ToString(),
                    NgayHd = DateTime.Now,
                };
                context.TblHoaDons.Add(hoaDon);
                context.SaveChanges();
                var tblHoaDon = context.TblHoaDons.OrderBy(item => item.MaHd).LastOrDefault().MaHd;
                maHD = (int)tblHoaDon;
                for (int i = 1; i < objects.AsArray().ToArray().Length; i++)
                {
                    TblChiTietHd chiTiet = new TblChiTietHd
                    {
                        MaHd = maHD,
                        MaHang = objects.AsArray().ToArray()[i]["maHang"].ToString(),
                        Soluong = int.Parse(objects.AsArray().ToArray()[i]["soLuong"].ToString())
                    };
                    context.TblChiTietHds.Add(chiTiet);
                    context.SaveChanges();
                }
                context.TblCarts.RemoveRange(context.TblCarts.ToList());
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json("Error " + ex.Message);
            }
            return Json("OK");
        }

        public JsonResult CancelSingle(string maHang)
        {
            TblCart cart = context.TblCarts.FirstOrDefault(item => item.MaHang == maHang);
            if (cart != null)
            {
                context.TblCarts.Remove(cart);
                context.SaveChanges();
            }
            var data = context.TblCarts.ToList();
            ViewBag.Carts = data;
            if (data.Count > 0)
            {
                float sum = 0;
                foreach (var item in data)
                {
                    sum += (float)item.Gia * (float)item.Soluong;
                }
                ViewBag.Sum = sum;
            }
            return Json(new { ViewBag.Carts, ViewBag.Sum });
        }

        public JsonResult CancelAll()
        {
            context.TblCarts.RemoveRange(context.TblCarts.ToList());
            context.SaveChanges();
            var data = context.TblCarts.ToList();
            ViewBag.Carts = data;
            return Json(new { ViewBag.Carts });
        }

        public JsonResult UpdateQuantity(string soLuong, string maHang)
        {
            var cart = context.TblCarts.FirstOrDefault(item => item.MaHang == maHang);
            if (cart != null)
            {
                cart.Soluong = int.Parse(soLuong);
                context.SaveChanges();
            }
            var data = context.TblCarts.ToList();
            ViewBag.Carts = data;
            float sum = 0;
            foreach (var item in data)
            {
                sum += (float)item.Gia * (float)item.Soluong;
            }
            ViewBag.Sum = sum;
            return Json(new { ViewBag.Carts, ViewBag.Sum });
        }
    }
}
