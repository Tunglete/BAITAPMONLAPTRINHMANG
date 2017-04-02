using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDienThoaiDiDong.Areas.Admin.Models.QuanLySliderAnhSanPham;
using WebDienThoaiDiDong.Models;

namespace WebDienThoaiDiDong.Areas.Admin.Controllers
{
    public class QuanLySliderAnhSanPhamController : Controller
    {
        QuanLyBanDienThoaiEntities db = new QuanLyBanDienThoaiEntities();

        public List<HttpPostedFileBase> fileUpload = new List<HttpPostedFileBase>();
        // GET: Admin/QuanLyHinhAnhSanPham
        public ActionResult Index()
        {
            if (Session["TenQuanTri"] != null && Session["MaQuanTri"] != null)
            {
                var result = db.SAN_PHAM.ToList();
                return View(result);
            }
            else
            {
                return RedirectToAction("Index", "DangNhapAdmin");
            }
            
        }
        public ActionResult DanhSachAnhSanPhamTable(int page)
        {
            int row = 5;
            int count = 0;
            int totalpages = 0;
            count = db.ANH_SAN_PHAM.Count(n => n.IsDeleted == false);
            if (count > 0)
            {
                totalpages = (int)Math.Ceiling((decimal)count / row);
            }
            else
            {
                totalpages = 0;
            }
            var start = row * (page - 1);
            var result = db.ANH_SAN_PHAM.Where(n => n.IsDeleted == false).OrderBy(n => n.MaAnhSanPham).Skip(start).Take(row).ToList();
            PagingQuanLySliderAnhSanPham model = new PagingQuanLySliderAnhSanPham();
            model.totalpage = totalpages;
            model.record = count;
            model.List = result;
            model.page = page;
            return View(model);
        }
        public ActionResult Add()
        {
            ViewBag.lstSanpham = db.SAN_PHAM.ToList();  
            return View(new ANH_SAN_PHAM());
        }
        //upload file to sever
        [HttpPost]
        public void UploadFile()
        {
            foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    if (file != null && file.ContentLength > 0)
                    {
                        if (Session["fileUpload"] == null)
                        {
                            fileUpload.Add(file);
                        }
                        else
                        {
                            fileUpload = (List<HttpPostedFileBase>)Session["fileUpload"];
                            fileUpload.Add(file);
                        }
                    }
                }
                Session["fileUpload"] = fileUpload;
        }
        public bool Save(int catalog)
        {
            if (Session["fileUpload"] != null)
            {
                fileUpload = (List<HttpPostedFileBase>)Session["fileUpload"];
                foreach (var item in fileUpload)
                {
                    if (item != null && item.ContentLength > 0)
                    {
                        var model = new ANH_SAN_PHAM();
                        string path = Path.Combine(Server.MapPath("~/Images/ImagesSliderProduct"), item.FileName);
                        item.SaveAs(path);
                        model.MaSanPham = catalog;
                        model.AnhMinhHoa = item.FileName;
                        model.IsDeleted = false;
                        db.ANH_SAN_PHAM.Add(model);
                        db.SaveChanges();
                    }
                }
            }
            Session["fileUpload"] = null;
            return true;
        }

        public bool Delete(int id)
        {
            var model = db.ANH_SAN_PHAM.FirstOrDefault(n => n.MaAnhSanPham == id);
            if (model != null)
            {
                model.IsDeleted= true;
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
            int row = 5;
            int count = 0;
            int totalpages = 0;
            count = db.ANH_SAN_PHAM.Count(n => n.IsDeleted == false && n.SAN_PHAM.TenSanPham.Contains(codesearch));
            if (count > 0)
            {
                totalpages = (int)Math.Ceiling((decimal)count / row);
            }
            else
            {
                totalpages = 0;
            }
            var start = row * (page - 1);

            var result = db.ANH_SAN_PHAM.Where(n => n.IsDeleted == false && n.SAN_PHAM.TenSanPham.Contains(codesearch)).OrderBy(n => n.SAN_PHAM.TenSanPham).Skip(start).Take(row).ToList();
            PagingQuanLySliderAnhSanPham model = new PagingQuanLySliderAnhSanPham();
            model.totalpage = totalpages;
            model.record = count;
            model.List = result;
            model.page = page;
            return View("DanhSachAnhSanPhamTable", model);
        }
    }
}