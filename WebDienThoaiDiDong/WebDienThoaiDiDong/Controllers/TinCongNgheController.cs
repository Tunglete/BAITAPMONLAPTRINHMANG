using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDienThoaiDiDong.Models;
using PagedList;

namespace WebDienThoaiDiDong.Controllers
{
    public class TinCongNgheController : Controller
    {
        QuanLyBanDienThoaiEntities db = new QuanLyBanDienThoaiEntities();
        // GET: TinCongNghe
        public ActionResult Index(int? page)
        {
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(db.TIN_TUC.OrderByDescending(n => n.NgayTao).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ChiTietTin(int ?id)
        {
            var result = db.TIN_TUC.FirstOrDefault(n => n.MaTinTuc == id);
            ViewBag.lstTinlienquan = db.TIN_TUC.Where(n => n.TacGia.Contains(result.TacGia)).Take(4).ToList();
            ViewBag.lstTinnoibat = db.TIN_TUC.OrderByDescending(n => n.NgayTao).Take(4).ToList();
            return View(result);
        }
    }
}