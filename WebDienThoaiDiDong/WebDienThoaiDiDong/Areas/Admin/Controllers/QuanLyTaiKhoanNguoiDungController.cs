using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDienThoaiDiDong.Areas.Admin.Models.QuanLyTaiKhoanNguoiDung;
using WebDienThoaiDiDong.Models;

namespace WebDienThoaiDiDong.Areas.Admin.Controllers
{
    public class QuanLyTaiKhoanNguoiDungController : Controller
    {
        QuanLyBanDienThoaiEntities db = new QuanLyBanDienThoaiEntities();
        // GET: Admin/QuanLyTaiKhoanNguoiDung
        public ActionResult Index()
        {
            if (Session["TenQuanTri"] != null && Session["MaQuanTri"] != null)
            {
                var result = db.KHACH_HANG.Where(n => n.IsDeleted == false).ToList();
                return View(result);
            }
            else
            {
                return RedirectToAction("Index", "DangNhapAdmin");
            }
            
        }
        public ActionResult DanhSachTaiKhoanNguoiDungTable(int page)
        {
            int row = 4;
            int count = 0;
            int totalpages = 0;
            count = db.KHACH_HANG.Count(n => n.IsDeleted == false);
            if (count > 0)
            {
                totalpages = (int)Math.Ceiling((decimal)count / row);
            }
            else
            {
                totalpages = 0;
            }
            var start = row * (page - 1);

            var result = db.KHACH_HANG.Where(n => n.IsDeleted == false).OrderBy(n => n.NgayTao).Skip(start).Take(row).ToList();
            PagingQuanLyTaiKhoanNguoiDung model = new PagingQuanLyTaiKhoanNguoiDung();
            model.totalpage = totalpages;
            model.record = count;
            model.List = result;
            model.page = page;
            return View(model);
        }
        public ActionResult Edit(int id, int page)
        {
            ViewBag.page = page;
            
            ViewBag.lstGioiTinh = new List<SelectListItem> {
            new SelectListItem() {Text = "Nam", Value="True"},
            new SelectListItem() {Text = "Nữ", Value="False"}
            };
               
            return View(new KHACH_HANG());
        }
        public bool Save(KHACH_HANG khachhang)
        {   
            try
            {
                KHACH_HANG model2 = new KHACH_HANG();
                model2.TenKhachHang = khachhang.TenKhachHang;
                model2.TenDangNhap = khachhang.TenDangNhap;
                model2.MatKhau = khachhang.MatKhau;
                model2.NgaySinh = khachhang.NgaySinh;
                model2.GioiTinh = khachhang.GioiTinh;
                model2.Mail = khachhang.Mail;
                model2.DienThoai = khachhang.DienThoai;
                model2.DiaChi = khachhang.DiaChi;
                model2.NgayTao = DateTime.Now;
                model2.IsDeleted = false;
                db.KHACH_HANG.Add(model2);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
            var model = db.KHACH_HANG.FirstOrDefault(n => n.MaKhachHang == id);
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
            int row = 4;
            int count = 0;
            int totalpages = 0;
            count = db.KHACH_HANG.Count(n => n.IsDeleted == false && n.TenKhachHang.Contains(codesearch));
            if (count > 0)
            {
                totalpages = (int)Math.Ceiling((decimal)count / row);
            }
            else
            {
                totalpages = 0;
            }
            var start = row * (page - 1);

            var result = db.KHACH_HANG.Where(n => n.IsDeleted == false && n.TenKhachHang.Contains(codesearch)).OrderBy(n => n.NgayTao).Skip(start).Take(row).ToList();
            PagingQuanLyTaiKhoanNguoiDung model = new PagingQuanLyTaiKhoanNguoiDung();
            model.totalpage = totalpages;
            model.record = count;
            model.List = result;
            model.page = page;
            return View("DanhSachTaiKhoanNguoiDungTable", model);
        }
    }
}