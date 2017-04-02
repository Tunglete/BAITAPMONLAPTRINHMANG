using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDienThoaiDiDong.Models;
namespace WebDienThoaiDiDong.Controllers
{
    public class ChiTietSanPhamController : Controller
    {
        QuanLyBanDienThoaiEntities db = new QuanLyBanDienThoaiEntities();
        
        // GET: ChiTietSanPham
        public ActionResult Index(string tensanpham, int id)
        {
            ViewBag.lstSlidesanpham = db.ANH_SAN_PHAM.Where(n =>n.IsDeleted == false && n.MaSanPham == id).OrderByDescending(n => n.MaAnhSanPham).Take(4).ToList();
            var model = db.SAN_PHAM.FirstOrDefault(n => n.MaSanPham == id && n.IsDeleted == false);
            ViewBag.Tintucsanpham = db.TIN_TUC_SAN_PHAM.Where(n => n.MaSanPham == id && n.IsDeleted == false).ToList();
            ViewBag.lstSanphamlienquan = db.SAN_PHAM.Where(n => n.MaCTDM == model.MaCTDM && n.IsDeleted == false).ToList();
            
            TempData["Gia"] = model.GiaSanPham;
           return View(model);
        }
        public ActionResult SearchMau(string codesearch,int id)
         {
            var result = db.SAN_PHAM.FirstOrDefault(n => n.MaSanPham == id);
            
            
            if(codesearch == "")
            {
                ViewBag.Gia = result.GiaSanPham;
                ViewBag.activegroup = "";
            }
            else
            {
                ViewBag.Gia = db.CHI_TIET_SAN_PHAM.FirstOrDefault(n => n.Mau.Contains(codesearch)).Gia;
                ViewBag.activegroup = codesearch;
            }
            ViewBag.group = db.CHI_TIET_SAN_PHAM.Where(n => n.MaSanPham == id).ToList();
            return View(result);
        }
    }
}