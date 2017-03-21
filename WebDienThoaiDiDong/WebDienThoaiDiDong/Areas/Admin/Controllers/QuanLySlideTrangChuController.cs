using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDienThoaiDiDong.Areas.Admin.Models.Quanlyslidetrangchu;
using WebDienThoaiDiDong.Models;

namespace WebDienThoaiDiDong.Areas.Admin.Controllers
{
    public class QuanLySlideTrangChuController : Controller
    {
        QuanLyBanDienThoaiEntities db = new QuanLyBanDienThoaiEntities();
        public List<HttpPostedFileBase> fileUpload = new List<HttpPostedFileBase>();
        // GET: Admin/QuanLySlideTrangChu
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DanhSachSlideTrangChuTable(int page)
        {
            int row = 5;
            int count = 0;
            int totalpages = 0;
            count = db.SLIDE_TRANG_CHU.Count(n => n.IsDeleted == false);
            if (count > 0)
            {
                totalpages = (int)Math.Ceiling((decimal)count / row);
            }
            else
            {
                totalpages = 0;
            }
            var start = row * (page - 1);
            var result = db.SLIDE_TRANG_CHU.Where(n => n.IsDeleted == false).OrderBy(n => n.Id).Skip(start).Take(row).ToList();
            PagingQuanLySlideTrangChu model = new PagingQuanLySlideTrangChu();
            model.totalpage = totalpages;
            model.record = count;
            model.List = result;
            model.page = page;
            return View(model);
        }
        public ActionResult Add()
        {
            return View(new SLIDE_TRANG_CHU());
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
        public bool Save()
        {
            if (Session["fileUpload"] != null)
            {
                fileUpload = (List<HttpPostedFileBase>)Session["fileUpload"];
                foreach (var item in fileUpload)
                {
                    if (item != null && item.ContentLength > 0)
                    {
                        var model = new SLIDE_TRANG_CHU();
                        string path = Path.Combine(Server.MapPath("~/Images/ImagesSlideHome"), item.FileName);
                        item.SaveAs(path);
                        model.HinhAnh = item.FileName;
                        model.IsDeleted = false;
                        db.SLIDE_TRANG_CHU.Add(model);
                        db.SaveChanges();
                    }
                }
            }
            Session["fileUpload"] = null;
            return true;
        }
        public bool Delete(int id)
        {
            var model = db.SLIDE_TRANG_CHU.FirstOrDefault(n => n.Id == id);
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