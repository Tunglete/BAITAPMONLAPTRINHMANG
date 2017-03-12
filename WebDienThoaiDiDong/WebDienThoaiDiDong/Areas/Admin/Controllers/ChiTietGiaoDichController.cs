using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDienThoaiDiDong.Areas.Admin.Models.Danhsachgiaodich;
using WebDienThoaiDiDong.Models;

namespace WebDienThoaiDiDong.Areas.Admin.Controllers
{
    public class ChiTietGiaoDichController : Controller
    {
        QuanLyBanDienThoaiEntities db = new QuanLyBanDienThoaiEntities();
        // GET: Admin/ChiTietGiaoDich
        public ActionResult Index()
        {
            return View();
        }

        //table đổ view ra index
        public ActionResult ChiTietGiaoDichTable( int page)
        {
            int row = 10;
            int count = 0;
            int totalpages = 0;
            count = db.CHI_TIET_DON_HANG.Count(n => n.IsDeleted == false);
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
                     join c in db.CHI_TIET_DON_HANG on a.MaDonHang equals c.MaDonHang
                     join d in db.SAN_PHAM on c.MaSanPham equals d.MaSanPham
                     
                     select new ChiTietDonHangViewModel()
                     {
                         MaCTDH = c.MaCTDH,
                         MaKhachHang = a.MaKhachHang,
                        TenKhachHang = b.TenKhachHang,
                         GiaSanPham = d.GiaSanPham,
                         TenSanPham = d.TenSanPham,
                         ManHinh = d.ManHinh,
                         Cpu = d.Cpu,
                         Ram = d.Ram,
                         HeDieuHanh = d.HeDieuHanh,
                         CameraChinh =d.CameraChinh,
                         CameraPhu = d.CameraPhu,
                         BoNhoTrong = d.BoNhoTrong,
                         TheNhoNgoai = d.TheNhoNgoai,
                         Blutooth = d.Blutooth,
                         DungLuongPin = d.DungLuongPin,
                         IsDeleted = c.IsDeleted,
                         SoLuong = c.SoLuong
                     };
            
            var result = md.Where(n => n.IsDeleted == false).OrderBy(n => n.MaKhachHang).Skip(start).Take(row).ToList();
            //phần chia span cho table
            int indexOfRowspan = 0;
            bool isRowspanfornextrow = true;
            foreach (var item in result)
            {
                item.IsRowspan = isRowspanfornextrow;
                indexOfRowspan++;
                item.NumberOfRowspan = md.Count(r=>r.MaKhachHang==item.MaKhachHang);
                if (indexOfRowspan == item.NumberOfRowspan)
                {
                    isRowspanfornextrow = true;
                    indexOfRowspan = 0;
                }
                else
                {
                    isRowspanfornextrow = false;
                }
            }
            PagingChiTietGiaoDich model = new PagingChiTietGiaoDich();
            model.totalpage = totalpages;
            model.record = count;
            model.List = result;
            model.page = page;
            return View(model);
        }

        //tìm kiếm theo tên sản phẩm
        public ActionResult SearchName(string codesearch , int page)
        {
            int row = 10;
            int count = 0;
            int totalpages = 0;
            count = db.CHI_TIET_DON_HANG.Count(n => n.IsDeleted == false);
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
                     join c in db.CHI_TIET_DON_HANG on a.MaDonHang equals c.MaDonHang
                     join d in db.SAN_PHAM on c.MaSanPham equals d.MaSanPham  
                     select new ChiTietDonHangViewModel()
                     {
                         MaCTDH = c.MaCTDH,
                         MaKhachHang = a.MaKhachHang,
                         TenKhachHang = b.TenKhachHang,
                         GiaSanPham = d.GiaSanPham,
                         TenSanPham = d.TenSanPham,
                         ManHinh = d.ManHinh,
                         Cpu = d.Cpu,
                         Ram = d.Ram,
                         HeDieuHanh = d.HeDieuHanh,
                         CameraChinh = d.CameraChinh,
                         CameraPhu = d.CameraPhu,
                         BoNhoTrong = d.BoNhoTrong,
                         TheNhoNgoai = d.TheNhoNgoai,
                         Blutooth = d.Blutooth,
                         DungLuongPin = d.DungLuongPin,
                         IsDeleted = c.IsDeleted,
                         SoLuong = c.SoLuong
                     };

            var result = md.Where(n => n.IsDeleted == false && n.TenKhachHang.Contains(codesearch)).OrderBy(n => n.MaKhachHang).Skip(start).Take(row).ToList();
            int indexOfRowspan = 0;
            bool isRowspanfornextrow = true;
            foreach (var item in result)
            {
                item.IsRowspan = isRowspanfornextrow;
                indexOfRowspan++;
                item.NumberOfRowspan = md.Count(r => r.MaKhachHang == item.MaKhachHang);
                if (indexOfRowspan == item.NumberOfRowspan)
                {
                    isRowspanfornextrow = true;
                    indexOfRowspan = 0;
                }
                else
                {
                    isRowspanfornextrow = false;
                }
            }
            PagingChiTietGiaoDich model = new PagingChiTietGiaoDich();
            model.totalpage = totalpages;
            model.record = count;
            model.List = result;
            model.page = page;
            return View("ChiTietGiaoDichTable",model);
        }
    }
}