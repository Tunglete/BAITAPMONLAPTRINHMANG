using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDienThoaiDiDong.Areas.Admin.Models.Danhsachgiaodich;
using WebDienThoaiDiDong.Models;

namespace WebDienThoaiDiDong.Areas.Admin.Controllers
{
    public class QuanLyGiaoDichController : Controller
    {
        QuanLyBanDienThoaiEntities db = new QuanLyBanDienThoaiEntities();
        // GET: Admin/QuanLyBanHang
        public ActionResult Index()
        {
            if (Session["TenQuanTri"] != null && Session["MaQuanTri"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "DangNhapAdmin");
            }
        }
        public ActionResult DanhSachGiaoDichTable(int page)
        {
            int row = 5;
            int count = 0;
            int totalpages = 0;
            count = db.DON_HANG.Count(n => n.IsDeleted == false);
            if (count > 0)
            {
                totalpages = (int)Math.Ceiling((decimal)count / row);
            }
            else
            {
                totalpages = 0;
            }
            var start = row * (page - 1);
            var md = from a in db.DON_HANG
                     join b in db.KHACH_HANG on a.MaKhachHang equals b.MaKhachHang
                     select new DanhSachGiaoDichViewModel()
                     {
                         MaDonHang = a.MaDonHang,
                         TenKhachHang = b.TenKhachHang,
                         TongGiaTriDonHang = a.TongGiaTriDonHang,
                         TrangThaiDonHang = a.TrangThaiDonHang,
                         NgayTao = a.NgayTao,
                         IsDeleted = a.IsDeleted
                     };
            var result = md.Where(n => n.IsDeleted == false).OrderBy(n => n.MaDonHang).Skip(start).Take(row).ToList();
            PagingDanhSachGiaoDich model = new PagingDanhSachGiaoDich();
            model.totalpage = totalpages;
            model.record = count;
            model.List = result;
            model.page = page;
            return View(model);
        }
        public ActionResult ChiTietGiaoDich(int id , string namecus)
        {
            ViewBag.namecus = namecus;
            var md = db.CHI_TIET_DON_HANG.Where(n => n.MaDonHang == id);
            var model = from a in md
                        join b in db.SAN_PHAM on a.MaSanPham equals b.MaSanPham
                        select new ChiTietDonHangViewModel()
                        {
                            TenSanPham = b.TenSanPham,
                            ManHinh = b.ManHinh,
                            Cpu = b.Cpu,
                            Ram = b.Ram,
                            HeDieuHanh = b.HeDieuHanh,
                            CameraChinh = b.CameraChinh,
                            CameraPhu = b.CameraPhu,
                            BoNhoTrong = b.BoNhoTrong,
                            TheNhoNgoai = b.TheNhoNgoai,
                            Blutooth = b.Blutooth,
                            DungLuongPin = b.DungLuongPin,
                            GiaSanPham = b.GiaSanPham,
                            SoLuong = a.SoLuong
                        };
            var lst = model.ToList();
            return View(lst);
        }

        public bool Delete(int id)
        {
            var model = db.DON_HANG.FirstOrDefault(n => n.MaDonHang == id);
            if (model != null)
            {
                model.IsDeleted = true;
                db.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }
        public ActionResult Search(string codesearch, int page)
        {
            int row = 5;
            int count = 0;
            int totalpages = 0;
            if (codesearch == "")
            {
                var md = from a in db.DON_HANG
                         join b in db.KHACH_HANG on a.MaKhachHang equals b.MaKhachHang
                         select new DanhSachGiaoDichViewModel()
                         {
                             MaDonHang = a.MaDonHang,
                             TenKhachHang = b.TenKhachHang,
                             TongGiaTriDonHang = a.TongGiaTriDonHang,
                             TrangThaiDonHang = a.TrangThaiDonHang,
                             NgayTao = a.NgayTao,
                             IsDeleted = a.IsDeleted
                         };
                count = md.Count(n => n.IsDeleted == false);
                if (count > 0)
                {
                    totalpages = (int)Math.Ceiling((decimal)count / row);
                }
                else
                {
                    totalpages = 0;
                }
                var start = row * (page - 1);
                var result = md.Where(n => n.IsDeleted == false).OrderBy(n => n.MaDonHang).Skip(start).Take(row).ToList();
                PagingDanhSachGiaoDich model = new PagingDanhSachGiaoDich();
                model.totalpage = totalpages;
                model.record = count;
                model.List = result;
                model.page = page;
                return View("DanhSachGiaoDichTable", model);
            }
            else
            {
                int start_gia =0;
                int end_gia = 0 ;
                if(codesearch == "Dưới 5 triệu")
                {  
                    start_gia = 0;
                    end_gia = 5000000;
                }
                if(codesearch == "Từ 5 - 10 triệu")
                {
                    start_gia = 5000000;
                    end_gia = 10000000;
                }
                if(codesearch == "Trên 10 triệu")
                {
                    start_gia = 10000000;
                    end_gia = 2000000000;
                }
                var md = from a in db.DON_HANG
                         join b in db.KHACH_HANG on a.MaKhachHang equals b.MaKhachHang
                         where a.TrangThaiDonHang == codesearch || (a.TongGiaTriDonHang>=start_gia&&a.TongGiaTriDonHang<=end_gia)
                         select new DanhSachGiaoDichViewModel()
                         {
                             MaDonHang = a.MaDonHang,
                             TenKhachHang = b.TenKhachHang,
                             TongGiaTriDonHang = a.TongGiaTriDonHang,
                             TrangThaiDonHang = a.TrangThaiDonHang,
                             NgayTao = a.NgayTao,
                             IsDeleted = a.IsDeleted
                         };
                count = md.Count(n => n.IsDeleted == false);
                if (count > 0)
                {
                    totalpages = (int)Math.Ceiling((decimal)count / row);
                }
                else
                {
                    totalpages = 0;
                }
                var start = row * (page - 1);
                var result = md.Where(n => n.IsDeleted == false).OrderBy(n => n.MaDonHang).Skip(start).Take(row).ToList();
                PagingDanhSachGiaoDich model = new PagingDanhSachGiaoDich();
                model.totalpage = totalpages;
                model.record = count;
                model.List = result;
                model.page = page;
                return View("DanhSachGiaoDichTable", model);
            }
        }
        public PartialViewResult EditPartial(int id = 1, string namecus = "", int page = 1)
        {
            ViewBag.Namecus = namecus;
            ViewBag.page = page;
            var model = db.DON_HANG.FirstOrDefault(n => n.MaDonHang == id);
            if(model.TrangThaiDonHang == "Chưa thanh toán")
            {
                ViewBag.lstTrangThaiDonHang = new List<SelectListItem> {
               new SelectListItem() {Text = "Chưa thanh toán", Value="Chưa thanh toán"},
               new SelectListItem() {Text = "Đã thanh toán", Value="Đã thanh toán"},
               new SelectListItem() {Text = "Chờ xử lý", Value="Chờ xử lý"}
               };
            }
            if(model.TrangThaiDonHang == "Đã thanh toán")
            {
                ViewBag.lstTrangThaiDonHang = new List<SelectListItem> {
               new SelectListItem() {Text = "Đã thanh toán", Value="Đã thanh toán"},
               new SelectListItem() {Text = "Chưa thanh toán", Value="Chưa thanh toán"},
               new SelectListItem() {Text = "Chờ xử lý", Value="Chờ xử lý"}
               };
            }
            if(model.TrangThaiDonHang == "Chờ xử lý")
            {
                ViewBag.lstTrangThaiDonHang = new List<SelectListItem> {
               new SelectListItem() {Text = "Chờ xử lý", Value="Chờ xử lý"},
               new SelectListItem() {Text = "Chưa thanh toán", Value="Chưa thanh toán"},
               new SelectListItem() {Text = "Đã thanh toán", Value="Đã thanh toán"}
               };
            }
            return PartialView(model);
        }
        public bool Save(DON_HANG model)
        {
            var lst = db.DON_HANG.FirstOrDefault(n => n.MaDonHang == model.MaDonHang);
            if (lst != null)
            {
                lst.MaKhachHang = model.MaKhachHang;
                lst.TrangThaiDonHang = model.TrangThaiDonHang;
                lst.TongGiaTriDonHang = model.TongGiaTriDonHang;
                lst.DiaChiNhanDonHang = model.DiaChiNhanDonHang;
                lst.NgayTao = model.NgayTao;
                lst.IsDeleted = model.IsDeleted;
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
}