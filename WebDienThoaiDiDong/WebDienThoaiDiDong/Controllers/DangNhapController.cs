using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDienThoaiDiDong.Models;

namespace WebDienThoaiDiDong.Controllers
{
    public class DangNhapController : Controller
    {
        QuanLyBanDienThoaiEntities db = new QuanLyBanDienThoaiEntities();
        // GET: DangNhap
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DangNhap(FormCollection f)
        {
            string username = f["name"];
            string password = f["pass"];
            var khachhang = db.KHACH_HANG.FirstOrDefault(n => n.TenDangNhap == username && n.MatKhau == password);
            if (khachhang != null)
            {
                Session["TenDangNhap"] = username;
               return RedirectToAction("Index", "TrangChu");
            }else
            {
                ViewBag.ThongBao = "Tên đăng nhập hoặc tài khoản không đúng";
                return View("Index");
            }
        }
    }
}