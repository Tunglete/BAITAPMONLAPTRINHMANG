using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDienThoaiDiDong.Areas.Admin.Models.QuanLyTinTucSanPham;
using WebDienThoaiDiDong.Models;

namespace WebDienThoaiDiDong.Areas.Admin.Controllers
{
    public class QuanLyTinTucSanPhamController : Controller
    {
        QuanLyBanDienThoaiEntities db = new QuanLyBanDienThoaiEntities();
        public HttpPostedFileBase fileUpload;
        // GET: Admin/QuanLyTinTucSanPham
        public ActionResult Index()
        {
            var result = db.SAN_PHAM.ToList();
            return View(result);
        }
        public ActionResult DanhSachTinTucSanPhamTable(int page)
        {
            int row = 4;
            int count = 0;
            int totalpages = 0;
            count = db.TIN_TUC_SAN_PHAM.Count(n => n.IsDeleted == false);
            if (count > 0)
            {
                totalpages = (int)Math.Ceiling((decimal)count / row);
            }
            else
            {
                totalpages = 0;
            }
            var start = row * (page - 1);
            var result = db.TIN_TUC_SAN_PHAM.Where(n => n.IsDeleted == false).OrderBy(n => n.NgayTao).Skip(start).Take(row).ToList();
            PagingQuanLyTinTucSanPham model = new PagingQuanLyTinTucSanPham();
            model.totalpage = totalpages;
            model.record = count;
            model.List = result;
            model.page = page;
            return View(model);
        }
        public ActionResult Edit(int id, int page)
        {
            ViewBag.lstSanpham = db.SAN_PHAM.ToList();
            ViewBag.page = page;
            var tintucsp = db.TIN_TUC_SAN_PHAM.FirstOrDefault(n => n.TTSP_Id == id);
            if (tintucsp != null)
            {
                return View(tintucsp);
            }
            else
            {
                return View(new TIN_TUC_SAN_PHAM());
            }
        }
        public void UploadFile()
        {
            Session["fileUpload"] = Request.Files[0];
        }
        [ValidateInput(false)]
        public bool Save(TIN_TUC_SAN_PHAM tintucsp, int catalog)
        {
            var model = db.TIN_TUC_SAN_PHAM.FirstOrDefault(n => n.TTSP_Id == tintucsp.TTSP_Id);
            try
            {
                if (model != null)
                {
                    if (Session["fileUpload"] != null)
                    {
                        fileUpload = (HttpPostedFileBase)Session["fileUpload"];
                        if (fileUpload != null && fileUpload.ContentLength > 0)
                        {
                            fileUpload = (HttpPostedFileBase)Session["fileUpload"];
                            string path = Path.Combine(Server.MapPath("~/Images/ImagesProductNews"), fileUpload.FileName);
                            fileUpload.SaveAs(path);
                            //xóa ảnh trong csdl
                            model.AnhTinTuc = "";
                            db.SaveChanges();

                            //thay ảnh mới
                            model.TTSP_Id = tintucsp.TTSP_Id;
                            model.MaSanPham = catalog;
                            model.TieuDeTinTuc = tintucsp.TieuDeTinTuc;
                            model.AnhTinTuc = fileUpload.FileName;
                            model.NoiDungTinTuc = tintucsp.NoiDungTinTuc;
                            model.NgayTao = tintucsp.NgayTao;
                            model.IsDeleted = tintucsp.IsDeleted;
                            db.SaveChanges();
                        }
                    }
                    else// nếu k thực thiện thay đỏi ảnh đại diện thì vào đây
                    {
                        model.TTSP_Id = tintucsp.TTSP_Id;
                        model.MaSanPham = catalog;
                        model.TieuDeTinTuc = tintucsp.TieuDeTinTuc;
                        model.NoiDungTinTuc = tintucsp.NoiDungTinTuc;
                        model.NgayTao = tintucsp.NgayTao;
                        model.IsDeleted = tintucsp.IsDeleted;
                        db.SaveChanges();
                    }

                }
                else
                {

                    TIN_TUC_SAN_PHAM model2 = new TIN_TUC_SAN_PHAM();
                    if (Session["fileUpload"] != null)
                    {
                        fileUpload = (HttpPostedFileBase)Session["fileUpload"];
                        if (fileUpload != null && fileUpload.ContentLength > 0)
                        {
                            string path = Path.Combine(Server.MapPath("~/Images/ImagesProductNews"), fileUpload.FileName);
                            fileUpload.SaveAs(path);
                            model2.TTSP_Id = tintucsp.TTSP_Id;
                            model2.MaSanPham = catalog;
                            model2.TieuDeTinTuc = tintucsp.TieuDeTinTuc;
                            model2.AnhTinTuc = fileUpload.FileName;
                            model2.NoiDungTinTuc = tintucsp.NoiDungTinTuc;
                            model2.NgayTao = DateTime.Now;
                            model2.IsDeleted = false;
                            db.TIN_TUC_SAN_PHAM.Add(model2);
                            db.SaveChanges();
                        }

                    }
                }
                Session["fileUpload"] = null;
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool Delete(int id)
        {
            var model = db.TIN_TUC_SAN_PHAM.FirstOrDefault(n => n.TTSP_Id == id);
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
            count = db.TIN_TUC_SAN_PHAM.Count(n => n.IsDeleted == false && n.SAN_PHAM.TenSanPham.Contains(codesearch));
            if (count > 0)
            {
                totalpages = (int)Math.Ceiling((decimal)count / row);
            }
            else
            {
                totalpages = 0;
            }
            var start = row * (page - 1);

            var result = db.TIN_TUC_SAN_PHAM.Where(n => n.IsDeleted == false && n.SAN_PHAM.TenSanPham.Contains(codesearch)).OrderBy(n => n.NgayTao).Skip(start).Take(row).ToList();
            PagingQuanLyTinTucSanPham model = new PagingQuanLyTinTucSanPham();
            model.totalpage = totalpages;
            model.record = count;
            model.List = result;
            model.page = page;
            return View("DanhSachTinTucSanPhamTable", model);
        }
    }
}