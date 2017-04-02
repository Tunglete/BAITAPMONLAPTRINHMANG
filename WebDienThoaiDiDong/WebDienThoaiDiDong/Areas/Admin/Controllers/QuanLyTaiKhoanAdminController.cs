using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDienThoaiDiDong.Areas.Admin.Models.QuanLyTaiKhoanAdmin;
using WebDienThoaiDiDong.Models;

namespace WebDienThoaiDiDong.Areas.Admin.Controllers
{
    public class QuanLyTaiKhoanAdminController : Controller
    {
        QuanLyBanDienThoaiEntities db = new QuanLyBanDienThoaiEntities();
        // GET: Admin/QuanLyTaiKhoanAdmin
        public ActionResult Index()
        {
            if (Session["TenQuanTri"] != null && Session["MaQuanTri"] != null)
            {
                var result = db.ADMINs.Where(n => n.IsDelete == false).ToList();
                return View(result);
            }
            else
            {
                return RedirectToAction("Index", "DangNhapAdmin");
            }
            
        }
        public ActionResult DanhSachTaiKhoanAdminTable(int page)
        {
            int row = 4;
            int count = 0;
            int totalpages = 0;
            count = db.ADMINs.Count(n => n.IsDelete == false);
            if (count > 0)
            {
                totalpages = (int)Math.Ceiling((decimal)count / row);
            }
            else
            {
                totalpages = 0;
            }
            var start = row * (page - 1);

            var result = db.ADMINs.Where(n => n.IsDelete == false).OrderBy(n => n.NgayTao).Skip(start).Take(row).ToList();
            PagingQuanLyTaiKhoanAdmin model = new PagingQuanLyTaiKhoanAdmin();
            model.totalpage = totalpages;
            model.record = count;
            model.List = result;
            model.page = page;
            return View(model);
        }
        public ActionResult Edit(int id, int page)
        {
            ViewBag.page = page;
            var admin = db.ADMINs.FirstOrDefault(n => n.MaQuanTri == id);
            if (admin != null)
            {
                return View(admin);
            }
            else
            {
                return View(new ADMIN());
            }
        }
        public bool Save(ADMIN admin)
        {
            var model = db.ADMINs.FirstOrDefault(n => n.MaQuanTri == admin.MaQuanTri);
            try
            {
                if (model != null)
                {
                    model.TenQuanTri = admin.TenQuanTri;
                    model.TenDangNhap = admin.TenDangNhap;
                    model.MatKhau = admin.MatKhau;
                    model.QuyenTruyCap = admin.QuyenTruyCap;
                    model.NgayTao = admin.NgayTao;
                    model.IsDelete = admin.IsDelete;
                    db.SaveChanges();
                }
                else
                {
                    ADMIN model2 = new ADMIN();
                    model2.TenQuanTri = admin.TenQuanTri;
                    model2.TenDangNhap = admin.TenDangNhap;
                    model2.MatKhau = admin.MatKhau;
                    model2.QuyenTruyCap = 1;
                    model2.NgayTao = DateTime.Now;
                    model2.IsDelete = false;
                    db.ADMINs.Add(model2);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
            var model = db.ADMINs.FirstOrDefault(n => n.MaQuanTri == id);
            if (model != null)
            {
                model.IsDelete = true;
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
            count = db.ADMINs.Count(n => n.IsDelete == false && n.TenQuanTri.Contains(codesearch));
            if (count > 0)
            {
                totalpages = (int)Math.Ceiling((decimal)count / row);
            }
            else
            {
                totalpages = 0;
            }
            var start = row * (page - 1);

            var result = db.ADMINs.Where(n => n.IsDelete == false && n.TenQuanTri.Contains(codesearch)).OrderBy(n => n.NgayTao).Skip(start).Take(row).ToList();
            PagingQuanLyTaiKhoanAdmin model = new PagingQuanLyTaiKhoanAdmin();
            model.totalpage = totalpages;
            model.record = count;
            model.List = result;
            model.page = page;
            return View("DanhSachTaiKhoanAdminTable", model);
        }
    }
}