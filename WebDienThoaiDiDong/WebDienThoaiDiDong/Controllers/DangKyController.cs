using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDienThoaiDiDong.Models;

namespace WebDienThoaiDiDong.Controllers
{
    public class DangKyController : Controller
    {
        QuanLyBanDienThoaiEntities db = new QuanLyBanDienThoaiEntities();
        // GET: DangKy
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DangKy(FormCollection f)
        {
            Session["ThongBao"] = null;
            string TENDANGNHAP = f["tendangnhap"];
            string ngaysinh = f["ngaysinh"];
            string gioitinh = f["gioitinh"];
            string diachi = f["address"];
            var khachhang = db.KHACH_HANG.FirstOrDefault(n => n.TenDangNhap == TENDANGNHAP);
            if (khachhang != null)
            {
               Session["ThongBao"] = "*Tên đăng nhập đã tồn tại";
                return RedirectToAction("Index","DangKy");
            }
            else
            {
                KHACH_HANG kh = new KHACH_HANG();
                kh.TenKhachHang = f["ho"] + f["ten"];
                kh.TenDangNhap = f["tendangnhap"];
                kh.MatKhau = f["matkhau"];
                kh.NgaySinh =DateTime.Parse(ngaysinh.ToString());
                if(gioitinh == "Nữ")
                {
                    kh.GioiTinh = false;
                }
                else
                {
                    kh.GioiTinh = true;
                }
                kh.Mail = f["email"];
                kh.DienThoai = f["phone"];
                kh.DiaChi = diachi;
                kh.NgayTao = DateTime.Now;
                kh.IsDeleted = false;
                db.KHACH_HANG.Add(kh);
                db.SaveChanges();
                return RedirectToAction("Index","DangNhap");
            }
        }
    }
}