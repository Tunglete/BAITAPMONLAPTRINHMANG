using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDienThoaiDiDong.Areas.Admin.Models;
using WebDienThoaiDiDong.Models;

namespace WebDienThoaiDiDong.Areas.Admin.Controllers
{
    public class QuanLyTinTucController : Controller
    {
        QuanLyBanDienThoaiEntities db = new QuanLyBanDienThoaiEntities();
        public HttpPostedFileBase fileUpload;
        // GET: Admin/QuanLyTinTuc
        public ActionResult Index()
        {
            var result = db.TIN_TUC.ToList();
            return View(result);
        }
        public ActionResult DanhSachTinTucTable(int page)
        {
            int row = 4;
            int count = 0;
            int totalpages = 0;
            count = db.TIN_TUC.Count(n => n.IsDeleted == false);
            if (count > 0)
            {
                totalpages = (int)Math.Ceiling((decimal)count / row);
            }
            else
            {
                totalpages = 0;
            }
            var start = row * (page - 1);

            var result = db.TIN_TUC.Where(n => n.IsDeleted == false).OrderBy(n => n.NgayTao).Skip(start).Take(row).ToList();
            PagingDanhSachTinTucTable model = new PagingDanhSachTinTucTable();
            model.totalpage = totalpages;
            model.record = count;
            model.List = result;
            model.page = page;
            return View(model);
        }
        public ActionResult Edit(int id, int page)
        {

            ViewBag.page = page;
            var tintuc = db.TIN_TUC.FirstOrDefault(n => n.MaTinTuc == id);
            if (tintuc != null)
            {
                return View(tintuc);
            }
            else
            {
                return View(new TIN_TUC());
            }
        }
        public void UploadFile()
        {
            Session["fileUpload"] = Request.Files[0];
        }
        [ValidateInput(false)]
        public bool Save(TIN_TUC tintuc)
        {
            var model = db.TIN_TUC.FirstOrDefault(n => n.MaTinTuc == tintuc.MaTinTuc);
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
                            string path = Path.Combine(Server.MapPath("~/Images/ImagesNews"), fileUpload.FileName);
                            fileUpload.SaveAs(path);
                            //xóa ảnh trong csdl
                            model.HinhAnh = "";
                            db.SaveChanges();

                            //thay ảnh mới
                            model.MaTinTuc = tintuc.MaTinTuc;
                            model.TenTinTuc = tintuc.TenTinTuc;
                            model.NoiDung = tintuc.NoiDung;
                            model.TacGia = tintuc.TacGia;
                            model.HinhAnh = fileUpload.FileName;
                            model.NgayTao = tintuc.NgayTao;
                            model.IsDeleted = tintuc.IsDeleted;
                            db.SaveChanges();
                        }
                    }
                    else// nếu k thực thiện thay đỏi ảnh đại diện thì vào đây
                    {
                        model.MaTinTuc = tintuc.MaTinTuc;
                        model.TenTinTuc = tintuc.TenTinTuc;
                        model.NoiDung = tintuc.NoiDung;
                        model.TacGia = tintuc.TacGia;
                        model.NgayTao = tintuc.NgayTao;
                        model.IsDeleted = tintuc.IsDeleted;
                        db.SaveChanges();
                    }

                }
                else
                {

                    TIN_TUC model2 = new TIN_TUC();
                    if (Session["fileUpload"] != null)
                    {
                        fileUpload = (HttpPostedFileBase)Session["fileUpload"];
                        if (fileUpload != null && fileUpload.ContentLength > 0)
                        {
                            string path = Path.Combine(Server.MapPath("~/Images/ImagesNews"), fileUpload.FileName);
                            fileUpload.SaveAs(path);
                            model2.MaTinTuc = tintuc.MaTinTuc;
                            model2.TenTinTuc = tintuc.TenTinTuc;
                            model2.NoiDung = tintuc.NoiDung;
                            model2.TacGia = tintuc.TacGia;
                            model2.HinhAnh = fileUpload.FileName;
                            model2.NgayTao = DateTime.Now;
                            model2.IsDeleted = false;
                            db.TIN_TUC.Add(model2);
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
            var model = db.TIN_TUC.FirstOrDefault(n => n.MaTinTuc == id);
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
            count = db.TIN_TUC.Count(n => n.IsDeleted == false && n.TacGia.Contains(codesearch));
            if (count > 0)
            {
                totalpages = (int)Math.Ceiling((decimal)count / row);
            }
            else
            {
                totalpages = 0;
            }
            var start = row * (page - 1);

            var result = db.TIN_TUC.Where(n => n.IsDeleted == false && n.TacGia.Contains(codesearch)).OrderBy(n => n.NgayTao).Skip(start).Take(row).ToList();
            PagingDanhSachTinTucTable model = new PagingDanhSachTinTucTable();
            model.totalpage = totalpages;
            model.record = count;
            model.List = result;
            model.page = page;
            return View("DanhSachTinTucTable", model);
        }
    }
}