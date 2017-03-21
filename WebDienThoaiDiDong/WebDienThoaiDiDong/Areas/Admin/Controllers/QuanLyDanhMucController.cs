using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDienThoaiDiDong.Areas.Admin.Models.Quanlydanhmuc;
using WebDienThoaiDiDong.Models;

namespace WebDienThoaiDiDong.Areas.Admin.Controllers
{
    public class QuanLyDanhMucController : Controller
    {
        QuanLyBanDienThoaiEntities db = new QuanLyBanDienThoaiEntities();
        // GET: Admin/QuanLyDanhMuc
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DanhSachDanhMucTable(int page)
        {
            int row = 4;
            int count = 0;
            int totalpages = 0;
            count = db.DANH_MUC.Count(n => n.IsDelteted == false);
            if (count > 0)
            {
                totalpages = (int)Math.Ceiling((decimal)count / row);
            }
            else
            {
                totalpages = 0;
            }
            var start = row * (page - 1);

            var result = db.DANH_MUC.Where(n => n.IsDelteted == false).OrderBy(n => n.NgayTao).Skip(start).Take(row).ToList();
            PagingQuanLyDanhMuc model = new PagingQuanLyDanhMuc();
            model.totalpage = totalpages;
            model.record = count;
            model.List = result;
            model.page = page;
            return View(model);
        }
        public ActionResult Edit(int id, int page)
        {
            ViewBag.page = page;
            var sanpham = db.DANH_MUC.FirstOrDefault(n => n.MaDanhMuc == id);
            if (sanpham != null)
            {    
                return View(sanpham);
            }
            else
            {  
                return View(new DANH_MUC());
            }
        }
        public bool Save(DANH_MUC danhmuc)
        {
            var model = db.DANH_MUC.FirstOrDefault(n => n.MaDanhMuc == danhmuc.MaDanhMuc);
            try
            {
                if (model != null)
                {
                    model.TenDanhMuc = danhmuc.TenDanhMuc;
                    model.NgayTao = danhmuc.NgayTao;
                    model.IsDelteted = danhmuc.IsDelteted;
                    db.SaveChanges();
                }
                else
                {
                    DANH_MUC model2 = new DANH_MUC();
                    model2.TenDanhMuc = danhmuc.TenDanhMuc;
                    model2.NgayTao = DateTime.Now;
                    model2.IsDelteted = false;
                    db.DANH_MUC.Add(model2);
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
            var model = db.DANH_MUC.FirstOrDefault(n => n.MaDanhMuc == id);
            if (model != null)
            {
                model.IsDelteted = true;
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