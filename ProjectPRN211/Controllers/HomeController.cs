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
            ViewBag.listGVT = context.TblDonViTinhs.ToList();
            ViewBag.listMatHang = context.TblMatHangs.ToList();
            return View();
        }

        public JsonResult GetUpdateData()
        {
            var data = context.TblMatHangs.ToList();
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

        public JsonResult AddMatHang(string ma, string ten, string dvt, string gia)
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
    }
}
