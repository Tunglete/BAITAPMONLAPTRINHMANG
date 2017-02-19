using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDienThoaiDiDong.Models;

namespace WebDienThoaiDiDong.Areas.Admin.Controllers
{
    public class DangNhapController : Controller
    {
        QuanLyBanDienThoaiEntities db = new QuanLyBanDienThoaiEntities();
        // GET: Admin/DangNhap
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            string username = f["name"];
            string password = f["pass"];
            ADMIN admin = db.ADMINs.SingleOrDefault(n => n.TenDangNhap == username && n.MatKhau == password);
            if (admin!=null)
            {
                ViewBag.TrangThai = "Đăng nhập thành công";
                Session["TaiKhoan"] = admin;
                return View();
            }
            else
            {
                ViewBag.TrangThai = "Tên tài khoản hoặc mật khẩu không đúng";
                return View();
            }
            
        }
    }
}