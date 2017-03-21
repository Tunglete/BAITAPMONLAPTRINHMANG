using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDienThoaiDiDong.Areas.Admin.Models.QuanLyChiTietDanhMuc;
using WebDienThoaiDiDong.Models;

namespace WebDienThoaiDiDong.Areas.Admin.Controllers
{
    public class QuanLyChiTietDanhMucController : Controller
    {
        QuanLyBanDienThoaiEntities db = new QuanLyBanDienThoaiEntities();
        // GET: Admin/QuanLyChiTietGiaoDich
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DanhSachChiTietDanhMucTable(int page)
        {
            int row = 4;
            int count = 0;
            int totalpages = 0;
            count = db.CHI_TIET_DANH_MUC.Count(n => n.IsDeleted == false && n.DANH_MUC.IsDelteted == false);
            if (count > 0)
            {
                totalpages = (int)Math.Ceiling((decimal)count / row);
            }
            else
            {
                totalpages = 0;
            }
            var start = row * (page - 1);

            var result = db.CHI_TIET_DANH_MUC.Where(n => n.IsDeleted == false && n.DANH_MUC.IsDelteted == false).OrderByDescending(n => n.NgayTao).Skip(start).Take(row).ToList();
            PagingQuanLyChiTietDanhMuc model = new PagingQuanLyChiTietDanhMuc();
            model.totalpage = totalpages;
            model.record = count;
            model.List = result;
            model.page = page;
            return View(model);
        }
        public ActionResult Edit(int id, int page)
        {
            ViewBag.lstDanhmuc = db.DANH_MUC.Where(n => n.IsDelteted == false).ToList();
            ViewBag.page = page;
            var ctdanhmuc = db.CHI_TIET_DANH_MUC.FirstOrDefault(n => n.MaCTDM == id);
            if (ctdanhmuc != null)
            {
                return View(ctdanhmuc);
            }
            else
            {
                return View(new CHI_TIET_DANH_MUC());
            }
        }
        public bool Save(CHI_TIET_DANH_MUC chitietdanhmuc, int catalog)
        {
            var model = db.CHI_TIET_DANH_MUC.FirstOrDefault(n => n.MaCTDM == chitietdanhmuc.MaCTDM);
            try
            {
                if (model != null)
                {
                    model.MaDanhMuc = catalog;
                    model.TenHang = chitietdanhmuc.TenHang;
                    model.NgayTao = chitietdanhmuc.NgayTao;
                    model.IsDeleted = chitietdanhmuc.IsDeleted;
                    db.SaveChanges();
                }
                else
                {
                    CHI_TIET_DANH_MUC model2 = new CHI_TIET_DANH_MUC();
                    model2.MaDanhMuc = catalog;
                    model2.TenHang = chitietdanhmuc.TenHang;
                    model2.NgayTao = DateTime.Now;
                    model2.IsDeleted = false;
                    db.CHI_TIET_DANH_MUC.Add(model2);
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
            var model = db.CHI_TIET_DANH_MUC.FirstOrDefault(n => n.MaCTDM == id);
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
    }
}