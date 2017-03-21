using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDienThoaiDiDong.Areas.Admin.Models.Quanlysanpham;
using WebDienThoaiDiDong.Models;

namespace WebDienThoaiDiDong.Areas.Admin.Controllers
{
    public class QuanLySanPhamController : Controller
    {
        QuanLyBanDienThoaiEntities db = new QuanLyBanDienThoaiEntities();
        public HttpPostedFileBase fileUpload;
        // GET: Admin/QuanLySanPham
        public ActionResult Index()
        {
            var result = db.SAN_PHAM.ToList();
            return View(result);
        }
        public ActionResult DanhSachSanPhamTable(int page)
        {
            int row = 4;
            int count = 0;
            int totalpages = 0;
            count = db.SAN_PHAM.Count(n => n.IsDeleted == false);
            if (count > 0)
            {
                totalpages = (int)Math.Ceiling((decimal)count / row);
            }
            else
            {
                totalpages = 0;
            }
            var start = row * (page - 1);

            var result = db.SAN_PHAM.Where(n => n.IsDeleted == false).OrderBy(n => n.TenSanPham).Skip(start).Take(row).ToList();
            PagingQuanLySanPham model = new PagingQuanLySanPham();
            model.totalpage = totalpages;
            model.record = count;
            model.List = result;
            model.page = page;
            return View(model);
        }
        public ActionResult ChiTietSanPham(int id)
        {
            var result = db.SAN_PHAM.FirstOrDefault(n => n.MaSanPham == id);
            ViewBag.Nameproduct = result.TenSanPham;
            return View(result);
        }
        public void UploadFile()
        {
            Session["fileUpload"] = Request.Files[0];
        }
        public ActionResult Edit(int id, int page)
        {
            ViewBag.page = page;
            ViewBag.lstDanhmuc = db.DANH_MUC.ToList();
            ViewBag.lstChitietdanhmuc = db.CHI_TIET_DANH_MUC.ToList();
            var sanpham = db.SAN_PHAM.FirstOrDefault(n => n.MaSanPham == id);
            if (sanpham != null)
            {
                if (sanpham.TheNhoNgoai == "Có")
                {
                    ViewBag.lstThenhongoai = new List<SelectListItem>
                {
                    new SelectListItem() {Text = "Có", Value="Có"},
                    new SelectListItem() {Text = "Không", Value="Không"},
                };
                }
                else
                {
                    ViewBag.lstThenhongoai = new List<SelectListItem>
                {
                    new SelectListItem() {Text = "Không", Value="Không"},
                    new SelectListItem() {Text = "Có", Value="Có"},
                };
                }
                if (sanpham.Blutooth == "Có")
                {
                    ViewBag.lstBlutooth = new List<SelectListItem>
                {
                    new SelectListItem() {Text = "Có", Value="Có"},
                    new SelectListItem() {Text = "Không", Value="Không"},
                };
                }
                else
                {
                    ViewBag.lstBlutooth = new List<SelectListItem>
                {
                    new SelectListItem() {Text = "Không", Value="Không"},
                    new SelectListItem() {Text = "Có", Value="Có"},
                };
                }
                ViewBag.checkadd = 0;
                return View(sanpham);
            }
            else
            {
                ViewBag.lstThenhongoai = new List<SelectListItem>
                {
                    new SelectListItem() {Text = "Có", Value="Có"},
                    new SelectListItem() {Text = "Không", Value="Không"},
                };
                ViewBag.lstBlutooth = new List<SelectListItem>
                {
                    new SelectListItem() {Text = "Có", Value="Có"},
                    new SelectListItem() {Text = "Không", Value="Không"},
                };
                ViewBag.checkadd = 1;
                return View(new SAN_PHAM());
            }

        }

        [ValidateInput(false)]
        public bool Save(SAN_PHAM sanpham, int catalog)
        {
            var model = db.SAN_PHAM.FirstOrDefault(n => n.MaSanPham == sanpham.MaSanPham);
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
                            string path = Path.Combine(Server.MapPath("~/Images/ImagesSliderProduct"), fileUpload.FileName);
                            fileUpload.SaveAs(path);
                            //xóa ảnh trong csdl
                            model.AnhDaiDien = "";
                            db.SaveChanges();

                            //thay ảnh mới
                            model.MaCTDM = catalog;
                            model.TenSanPham = sanpham.TenSanPham;
                            model.TieuDeSanPham = sanpham.TieuDeSanPham;
                            model.GiaSanPham = sanpham.GiaSanPham;
                            model.ManHinh = sanpham.ManHinh;
                            model.Cpu = sanpham.Cpu;
                            model.Ram = sanpham.Ram;
                            model.HeDieuHanh = sanpham.HeDieuHanh;
                            model.CameraChinh = sanpham.CameraChinh;
                            model.CameraPhu = sanpham.CameraPhu;
                            model.BoNhoTrong = sanpham.BoNhoTrong;
                            model.TheNhoNgoai = sanpham.TheNhoNgoai;
                            model.Blutooth = sanpham.Blutooth;
                            model.DungLuongPin = sanpham.DungLuongPin;
                            model.SoLuongTrongKho = sanpham.SoLuongTrongKho;
                            model.GiamGia = sanpham.GiamGia;
                            model.CheDoBaoHanh = sanpham.CheDoBaoHanh;
                            model.KhuyenMai = sanpham.KhuyenMai;
                            model.AnhDaiDien = fileUpload.FileName;
                            model.Video = sanpham.Video;
                            model.IsDeleted = sanpham.IsDeleted;
                            db.SaveChanges();
                        }
                    }
                    else// nếu k thực thiện thay đỏi ảnh đại diện thì vào đây
                    {
                        model.MaCTDM = catalog;
                        model.TenSanPham = sanpham.TenSanPham;
                        model.TieuDeSanPham = sanpham.TieuDeSanPham;
                        model.GiaSanPham = sanpham.GiaSanPham;
                        model.ManHinh = sanpham.ManHinh;
                        model.Cpu = sanpham.Cpu;
                        model.Ram = sanpham.Ram;
                        model.HeDieuHanh = sanpham.HeDieuHanh;
                        model.CameraChinh = sanpham.CameraChinh;
                        model.CameraPhu = sanpham.CameraPhu;
                        model.BoNhoTrong = sanpham.BoNhoTrong;
                        model.TheNhoNgoai = sanpham.TheNhoNgoai;
                        model.Blutooth = sanpham.Blutooth;
                        model.DungLuongPin = sanpham.DungLuongPin;
                        model.SoLuongTrongKho = sanpham.SoLuongTrongKho;
                        model.GiamGia = sanpham.GiamGia;
                        model.CheDoBaoHanh = sanpham.CheDoBaoHanh;
                        model.KhuyenMai = sanpham.KhuyenMai;
                        model.Video = sanpham.Video;
                        model.IsDeleted = sanpham.IsDeleted;
                        db.SaveChanges();
                    }

                }
                else
                {

                    SAN_PHAM model2 = new SAN_PHAM();
                    if (Session["fileUpload"] != null)
                    {
                        fileUpload = (HttpPostedFileBase)Session["fileUpload"];
                        if (fileUpload != null && fileUpload.ContentLength > 0)
                        {
                            string path = Path.Combine(Server.MapPath("~/Images/ImagesSliderProduct"), fileUpload.FileName);
                            fileUpload.SaveAs(path);
                            model2.MaCTDM = catalog;
                            model2.TenSanPham = sanpham.TenSanPham;
                            model2.TieuDeSanPham = sanpham.TieuDeSanPham;
                            model2.GiaSanPham = sanpham.GiaSanPham;
                            model2.ManHinh = sanpham.ManHinh;
                            model2.Cpu = sanpham.Cpu;
                            model2.Ram = sanpham.Ram;
                            model2.HeDieuHanh = sanpham.HeDieuHanh;
                            model2.CameraChinh = sanpham.CameraChinh;
                            model2.CameraPhu = sanpham.CameraPhu;
                            model2.BoNhoTrong = sanpham.BoNhoTrong;
                            model2.TheNhoNgoai = sanpham.TheNhoNgoai;
                            model2.Blutooth = sanpham.Blutooth;
                            model2.DungLuongPin = sanpham.DungLuongPin;
                            model2.SoLuongTrongKho = sanpham.SoLuongTrongKho;
                            model2.GiamGia = sanpham.GiamGia;
                            model2.CheDoBaoHanh = sanpham.CheDoBaoHanh;
                            model2.KhuyenMai = sanpham.KhuyenMai;
                            model2.AnhDaiDien = fileUpload.FileName;
                            model2.Video = sanpham.Video;
                            model2.IsDeleted = sanpham.IsDeleted;
                            db.SAN_PHAM.Add(model2);
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

        public ActionResult Search(string codesearch, int page)
        {
            int row = 4;
            int count = 0;
            int totalpages = 0;
            count = db.SAN_PHAM.Count(n => n.IsDeleted == false && n.TenSanPham.Contains(codesearch));
            if (count > 0)
            {
                totalpages = (int)Math.Ceiling((decimal)count / row);
            }
            else
            {
                totalpages = 0;
            }
            var start = row * (page - 1);

            var result = db.SAN_PHAM.Where(n => n.IsDeleted == false && n.TenSanPham.Contains(codesearch)).OrderBy(n => n.TenSanPham).Skip(start).Take(row).ToList();
            PagingQuanLySanPham model = new PagingQuanLySanPham();
            model.totalpage = totalpages;
            model.record = count;
            model.List = result;
            model.page = page;
            return View("DanhSachSanPhamTable", model);
        }
    }
}