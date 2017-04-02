using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDienThoaiDiDong.Models;

namespace WebDienThoaiDiDong.Controllers
{
    public class TrangChuController : Controller
    {
        QuanLyBanDienThoaiEntities db = new QuanLyBanDienThoaiEntities();
        private const string CartSession = "CartSession";
        public ActionResult Index()
        {
            ViewBag.lstSlide = db.SLIDE_TRANG_CHU.Where(n => n.IsDeleted == false).OrderByDescending(n => n.Id).Take(4).ToList();
            ViewBag.lstTincongnghe = db.TIN_TUC.Where(n => n.IsDeleted == false).OrderBy(n => n.NgayTao).Take(3).ToList();
            ViewBag.lstSanphammoi = db.SAN_PHAM.Where(n => n.IsDeleted == false && (n.CHI_TIET_DANH_MUC.MaDanhMuc == 1 || n.CHI_TIET_DANH_MUC.MaDanhMuc == 2)).OrderBy(n => n.NgayTao).Take(3).ToList();
            ViewBag.lstSanphamtieubieu = db.SAN_PHAM.Where(n => n.IsDeleted == false && (n.CHI_TIET_DANH_MUC.MaDanhMuc == 1 || n.CHI_TIET_DANH_MUC.MaDanhMuc == 2)).OrderByDescending(n => n.GiaSanPham).Take(12).ToList();
            ViewBag.lstPhukien = db.SAN_PHAM.Where(n => n.IsDeleted == false && n.CHI_TIET_DANH_MUC.MaDanhMuc == 3).OrderBy(n => n.NgayTao).Take(4).ToList();
            return View();
        }
        public ActionResult DangXuat()
        {
            Session["TenDangNhap"] = null;
            return RedirectToAction("Index");
        }
        public ActionResult Menu()
        {
            ViewBag.count1 = db.CHI_TIET_DANH_MUC.Count(n => n.IsDeleted == false && n.MaDanhMuc == 1);
            ViewBag.count2 = db.CHI_TIET_DANH_MUC.Count(n => n.IsDeleted == false && n.MaDanhMuc == 2);
            ViewBag.count3 = db.CHI_TIET_DANH_MUC.Count(n => n.IsDeleted == false && n.MaDanhMuc == 3);
            ViewBag.lst2 = db.CHI_TIET_DANH_MUC.Where(n => n.IsDeleted == false && n.MaDanhMuc == 2).ToList();
            ViewBag.lst3 = db.CHI_TIET_DANH_MUC.Where(n => n.IsDeleted == false && n.MaDanhMuc == 3).ToList();

            var result = db.CHI_TIET_DANH_MUC.Where(n => n.IsDeleted == false).OrderBy(n => n.MaDanhMuc).ToList();
            return View(result);
        }
        public PartialViewResult HeaderCart()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return PartialView(list);
        }
    }
}