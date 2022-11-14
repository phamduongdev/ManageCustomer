using Microsoft.AspNetCore.Mvc;
using ProjectPRN211.Models;
using System.Globalization;

namespace ProjectPRN211.Controllers
{
    public class HomeController : Controller
    {
        MyOrderContext context = new MyOrderContext();
        public IActionResult Tasks()
        {
            //ViewBag.listMatHang = context.TblMatHangs.OrderBy(item => item.TenHang).ToList();
            string name = HttpContext.Session.GetString("username");
            if (!string.IsNullOrEmpty(name))
            {
                ViewBag.listGVT = context.TblDonViTinhs.ToList();
                ViewBag.listMatHang = context.TblMatHangs.ToList();
                ViewBag.Size = context.TblCarts.ToList().Count;
                ViewBag.Categories = context.TblCategories.ToList();
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        public JsonResult GetUpdateData()
        {
            //var data = context.TblMatHangs.ToList();
            var data = context.TblMatHangs.Select(item => new
            {
                MaHang = item.MaHang,
                TenHang = item.TenHang,
                Dvt = item.Dvt,
                Gia = item.Gia,
                CategoryId = item.CategoryId,
                CategoryName = item.Category.CategoryName,
            }).ToList();
            return Json(data);
        }

        public JsonResult GetReportData()
        {
            var data = context.TblChiTietHds.Select(item => new
            {
                MaHd = item.MaHd,
                NgayHD = item.MaHdNavigation.NgayHd,
                MaKH = item.MaHdNavigation.MaKh,
                TenKH = context.TblKhachHangs.FirstOrDefault(x => x.MaKh == item.MaHdNavigation.MaKh).TenKh,
                MaHang = item.MaHang,
                TenHang = context.TblMatHangs.FirstOrDefault(x => x.MaHang == item.MaHang).TenHang,
                DVT = context.TblMatHangs.FirstOrDefault(x => x.MaHang == item.MaHang).Dvt,
                Gia = context.TblMatHangs.FirstOrDefault(x => x.MaHang == item.MaHang).Gia,
                Soluong = item.Soluong
            }).OrderBy(item => item.TenKH).ToList();
            return Json(data);
        }

        public JsonResult SearchReportData(string from, string to)
        {
            var datefrom = DateTime.ParseExact(from.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd 00:00:00");
            var dateto = DateTime.ParseExact(to.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd 00:00:00");
            DateTime dateTimeFrom = DateTime.ParseExact(DateTime.ParseExact(from.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd 00:00:00"), "yyyy-MM-dd 00:00:00", CultureInfo.InvariantCulture);
            DateTime dateTimeTo = DateTime.ParseExact(DateTime.ParseExact(to.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd 00:00:00"), "yyyy-MM-dd 00:00:00", CultureInfo.InvariantCulture);
            var data = context.TblChiTietHds.Select(item => new
            {
                MaHd = item.MaHd,
                NgayHD = item.MaHdNavigation.NgayHd,
                MaKH = item.MaHdNavigation.MaKh,
                TenKH = context.TblKhachHangs.FirstOrDefault(x => x.MaKh == item.MaHdNavigation.MaKh).TenKh,
                MaHang = item.MaHang,
                TenHang = context.TblMatHangs.FirstOrDefault(x => x.MaHang == item.MaHang).TenHang,
                DVT = context.TblMatHangs.FirstOrDefault(x => x.MaHang == item.MaHang).Dvt,
                Gia = context.TblMatHangs.FirstOrDefault(x => x.MaHang == item.MaHang).Gia,
                Soluong = item.Soluong
            })
            .ToList();
            var ChiTietHds = data.Where(item => item.NgayHD >= dateTimeFrom && item.NgayHD <= dateTimeTo).ToList();
            return Json(ChiTietHds);
        }

        public JsonResult AddMatHang(string ma, string ten, string dvt, string gia, string categoryId)
        {
            TblMatHang mathang = new TblMatHang
            {
                MaHang = ma,
                TenHang = ten,
                Dvt = dvt,
                Gia = float.Parse(gia, CultureInfo.InvariantCulture.NumberFormat),
                CategoryId = int.Parse(categoryId)
            };
            try
            {
                var mathangs = context.TblMatHangs.Where(item => item.MaHang.Equals(mathang.MaHang)).ToList();
                if (mathangs.Count() > 0)
                {
                    TempData["Message"] = "<script>alert('Duplicated');</script>";
                    return Json(TempData);
                }
                else
                {
                    context.TblMatHangs.Add(mathang);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            var data = context.TblMatHangs.ToList();
            return Json(data);
        }

        public JsonResult UpdateMatHang(string ma, string ten, string dvt, string gia)
        {
            TblMatHang mathang = new TblMatHang
            {
                MaHang = ma,
                TenHang = ten,
                Dvt = dvt,
                Gia = float.Parse(gia, CultureInfo.InvariantCulture.NumberFormat)
            };
            try
            {
                var mathangs = context.TblMatHangs.Where(item => item.MaHang.Equals(mathang.MaHang)).ToList();
                if (mathangs.Count() > 0)
                {
                    TempData["Message"] = "<script>alert('Duplicated');</script>";
                    return Json(TempData);
                }
                else
                {
                    context.TblMatHangs.Add(mathang);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            var data = context.TblMatHangs.ToList();
            return Json(data);
        }

        public JsonResult DeleteMatHanh(string ma)
        {
            TblMatHang mathang = context.TblMatHangs.Find(ma);
            context.TblMatHangs.Remove(mathang);
            context.SaveChanges();
            var data = context.TblMatHangs.ToList();
            return Json(data);
        }

        public JsonResult SearchKhachHang(string maKH, string tenKH)
        {
            var data = context.TblChiTietHds.Select(item => new
            {
                MaHd = item.MaHd,
                NgayHD = item.MaHdNavigation.NgayHd,
                MaKH = item.MaHdNavigation.MaKh,
                TenKH = context.TblKhachHangs.FirstOrDefault(x => x.MaKh == item.MaHdNavigation.MaKh).TenKh,
                Diachi = context.TblKhachHangs.FirstOrDefault(x => x.MaKh == item.MaHdNavigation.MaKh).Diachi
            })
            .ToList();
            var khachHang = data.Where(item => item.MaKH == maKH && item.TenKH == tenKH).ToList();
            return Json(khachHang);
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

        public JsonResult SearchHoaDonKhachHang(string maHD, string maKH)
        {
            var data = context.TblChiTietHds.Select(item => new
            {
                MaHd = item.MaHd,
                MaKH = item.MaHdNavigation.MaKh,
                NgayMua = item.MaHdNavigation.NgayHd,
                MaHang = item.MaHang,
                TenHang = context.TblMatHangs.FirstOrDefault(x => x.MaHang == item.MaHang).TenHang,
                DVT = context.TblMatHangs.FirstOrDefault(x => x.MaHang == item.MaHang).Dvt,
                Gia = context.TblMatHangs.FirstOrDefault(x => x.MaHang == item.MaHang).Gia,
                Soluong = item.Soluong
            }).Where(item => item.MaHd == Convert.ToInt32(maHD) && item.MaKH == maKH).ToList();
            return Json(data);
        }

        public JsonResult FillterProduct(string categoryId)
        {
            var data = context.TblMatHangs.ToList();
            if (int.Parse(categoryId) != 0)
            {
                data = data.Where(item => item.CategoryId == int.Parse(categoryId)).ToList();
            }
            ViewBag.CategoryId = categoryId;
            return Json(data);
        }

        public JsonResult SearchProduct(string value)
        {
            var data = context.TblMatHangs.ToList();
            if (!string.IsNullOrEmpty(value))
            {
                data = context.TblMatHangs.Where(item => item.TenHang.Contains(value)).ToList();
            }
            return Json(data);
        }

        public JsonResult SortPrice(string orderBy)
        {
            var data = context.TblMatHangs.ToList();
            if (orderBy.Equals("in"))
            {
                data = context.TblMatHangs.OrderBy(item => item.Gia).ToList();
            }
            else
            {
                data = context.TblMatHangs.OrderByDescending(item => item.Gia).ToList();
            }
            return Json(data);
        }
    }
}
