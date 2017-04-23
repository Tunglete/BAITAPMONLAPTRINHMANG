using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDienThoaiDiDong.Models;

namespace WebDienThoaiDiDong.Controllers
{
    public class MayTinhBangController : Controller
    {
        QuanLyBanDienThoaiEntities db = new QuanLyBanDienThoaiEntities();
        // GET: MayTinhBang
        public ActionResult Index(string tenhang = "", int count = 0)
        {
            ViewBag.lstHangsanxuat = db.CHI_TIET_DANH_MUC.Where(n => n.IsDeleted == false && n.MaDanhMuc == 2).OrderBy(n => n.NgayTao).ToList();
            if (tenhang != "")
            {
                count = count + 4;
                TempData["lstMayTinhBang"] = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false && n.CHI_TIET_DANH_MUC.TenHang.Contains(tenhang)).OrderBy(n => n.NgayTao).Take(count).ToList();
                var a = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false && n.CHI_TIET_DANH_MUC.TenHang.Contains(tenhang)).OrderBy(n => n.NgayTao).ToList();
                TempData["countSpconlai"] = a.Skip(count).Count();
            }
            return View();
        }
        public ActionResult MayTinhBang(int count)
        {
            count = count + 4;
            ViewBag.lstMayTinhBang = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false).OrderBy(n => n.NgayTao).Take(count).ToList();
            var a = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false).OrderBy(n => n.NgayTao).ToList();
            ViewBag.countSpconlai = a.Skip(count).Count();
            if (TempData["lstMayTinhBang"] != null)
            {
                ViewBag.lstMayTinhBang = TempData["lstMayTinhBang"];
                ViewBag.countSpconlai = TempData["countSpconlai"];
            }
            return View();
        }
        public ActionResult Search(string codesearch1, string codesearch2, string codesearch3, int count)
        {
            var Tenhang = db.CHI_TIET_DANH_MUC.FirstOrDefault(n => n.TenHang == codesearch2);
            count = count + 4;
            switch (codesearch1)
            {
                case "Dưới 1 triệu":
                    {
                        ViewBag.lstMayTinhBang = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false && n.GiaSanPham <= 1000000).OrderBy(n => n.NgayTao).Take(count).ToList();
                        var a = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false && n.GiaSanPham <= 1000000).OrderBy(n => n.NgayTao).ToList();
                        ViewBag.countSpconlai = a.Skip(count).Count();
                        break;
                    }
                case "Dưới 2 triệu":
                    {
                        ViewBag.lstMayTinhBang = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false && n.GiaSanPham <= 2000000).OrderBy(n => n.NgayTao).Take(count).ToList();
                        var a = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false && n.GiaSanPham <= 2000000).OrderBy(n => n.NgayTao).ToList();
                        ViewBag.countSpconlai = a.Skip(count).Count();
                        break;
                    }
                case "Dưới 3 triệu":
                    {
                        ViewBag.lstMayTinhBang = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false && n.GiaSanPham <= 3000000).OrderBy(n => n.NgayTao).Take(count).ToList();
                        var a = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false && n.GiaSanPham <= 3000000).OrderBy(n => n.NgayTao).ToList();
                        ViewBag.countSpconlai = a.Skip(count).Count();
                        break;
                    }
                case "Dưới 5 triệu":
                    {
                        ViewBag.lstMayTinhBang = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false && n.GiaSanPham <= 5000000).OrderBy(n => n.NgayTao).Take(count).ToList();
                        var a = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false && n.GiaSanPham <= 5000000).OrderBy(n => n.NgayTao).ToList();
                        ViewBag.countSpconlai = a.Skip(count).Count();
                        break;
                    }
                case "Dưới 8 triệu":
                    {
                        ViewBag.lstMayTinhBang = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false && n.GiaSanPham <= 8000000).OrderBy(n => n.NgayTao).Take(count).ToList();
                        var a = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false && n.GiaSanPham <= 8000000).OrderBy(n => n.NgayTao).ToList();
                        ViewBag.countSpconlai = a.Skip(count).Count();
                        break;
                    }
                case "Dưới 10 triệu":
                    {
                        ViewBag.lstMayTinhBang = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false && n.GiaSanPham <= 10000000).OrderBy(n => n.NgayTao).Take(count).ToList();
                        var a = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false && n.GiaSanPham <= 10000000).OrderBy(n => n.NgayTao).ToList();
                        ViewBag.countSpconlai = a.Skip(count).Count();
                        break;
                    }
                case "Trên 10 triệu":
                    {
                        ViewBag.lstMayTinhBang = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false && n.GiaSanPham >= 10000000).OrderBy(n => n.NgayTao).Take(count).ToList();
                        var a = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false && n.GiaSanPham >= 10000000).OrderBy(n => n.NgayTao).ToList();
                        ViewBag.countSpconlai = a.Skip(count).Count();
                        break;
                    }


            }
            if (Tenhang != null && codesearch2 == Tenhang.TenHang)
            {
                if (codesearch3 == "")
                {
                    ViewBag.lstMayTinhBang = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false && n.CHI_TIET_DANH_MUC.TenHang.Contains(Tenhang.TenHang)).OrderBy(n => n.NgayTao).Take(count).ToList();
                    var a = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false && n.CHI_TIET_DANH_MUC.TenHang.Contains(Tenhang.TenHang)).OrderBy(n => n.NgayTao).ToList();
                    ViewBag.countSpconlai = a.Skip(count).Count();
                }
                else
                {
                    if (codesearch3 == "Giá giảm dần")
                    {
                        ViewBag.lstMayTinhBang = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false && n.CHI_TIET_DANH_MUC.TenHang.Contains(Tenhang.TenHang)).OrderByDescending(n => n.GiaSanPham).Take(count).ToList();
                        var a = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false && n.CHI_TIET_DANH_MUC.TenHang.Contains(Tenhang.TenHang)).OrderByDescending(n => n.GiaSanPham).ToList();
                        ViewBag.countSpconlai = a.Skip(count).Count();
                    }
                    else
                    {
                        ViewBag.lstMayTinhBang = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false && n.CHI_TIET_DANH_MUC.TenHang.Contains(Tenhang.TenHang)).OrderBy(n => n.GiaSanPham).Take(count).ToList();
                        var a = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false && n.CHI_TIET_DANH_MUC.TenHang.Contains(Tenhang.TenHang)).OrderBy(n => n.GiaSanPham).ToList();
                        ViewBag.countSpconlai = a.Skip(count).Count();
                    }

                }

            }

            if (Tenhang != null && codesearch2 == Tenhang.TenHang && codesearch1 != "")
            {
                switch (codesearch1)
                {
                    case "Dưới 1 triệu":
                        {
                            ViewBag.lstMayTinhBang = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false && n.GiaSanPham <= 1000000 && n.CHI_TIET_DANH_MUC.TenHang.Contains(Tenhang.TenHang)).OrderBy(n => n.NgayTao).Take(count).ToList();
                            var a = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false && n.GiaSanPham <= 1000000 && n.CHI_TIET_DANH_MUC.TenHang.Contains(Tenhang.TenHang)).OrderBy(n => n.NgayTao).ToList();
                            ViewBag.countSpconlai = a.Skip(count).Count();
                            break;
                        }
                    case "Dưới 2 triệu":
                        {
                            ViewBag.lstMayTinhBang = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false && n.GiaSanPham <= 2000000 && n.CHI_TIET_DANH_MUC.TenHang.Contains(Tenhang.TenHang)).OrderBy(n => n.NgayTao).Take(count).ToList();
                            var a = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false && n.GiaSanPham <= 2000000 && n.CHI_TIET_DANH_MUC.TenHang.Contains(Tenhang.TenHang)).OrderBy(n => n.NgayTao).ToList();
                            ViewBag.countSpconlai = a.Skip(count).Count();
                            break;
                        }
                    case "Dưới 3 triệu":
                        {
                            ViewBag.lstMayTinhBang = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false && n.GiaSanPham <= 3000000 && n.CHI_TIET_DANH_MUC.TenHang.Contains(Tenhang.TenHang)).OrderBy(n => n.NgayTao).Take(count).ToList();
                            var a = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false && n.GiaSanPham <= 3000000 && n.CHI_TIET_DANH_MUC.TenHang.Contains(Tenhang.TenHang)).OrderBy(n => n.NgayTao).ToList();
                            ViewBag.countSpconlai = a.Skip(count).Count();
                            break;
                        }
                    case "Dưới 5 triệu":
                        {
                            ViewBag.lstMayTinhBang = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false && n.GiaSanPham <= 5000000 && n.CHI_TIET_DANH_MUC.TenHang.Contains(Tenhang.TenHang)).OrderBy(n => n.NgayTao).Take(count).ToList();
                            var a = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false && n.GiaSanPham <= 5000000 && n.CHI_TIET_DANH_MUC.TenHang.Contains(Tenhang.TenHang)).OrderBy(n => n.NgayTao).ToList();
                            ViewBag.countSpconlai = a.Skip(count).Count();
                            break;
                        }
                    case "Dưới 8 triệu":
                        {
                            ViewBag.lstMayTinhBang = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false && n.GiaSanPham <= 8000000 && n.CHI_TIET_DANH_MUC.TenHang.Contains(Tenhang.TenHang)).OrderBy(n => n.NgayTao).Take(count).ToList();
                            var a = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false && n.GiaSanPham <= 8000000 && n.CHI_TIET_DANH_MUC.TenHang.Contains(Tenhang.TenHang)).OrderBy(n => n.NgayTao).ToList();
                            ViewBag.countSpconlai = a.Skip(count).Count();
                            break;
                        }
                    case "Dưới 10 triệu":
                        {
                            ViewBag.lstMayTinhBang = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false && n.GiaSanPham <= 10000000 && n.CHI_TIET_DANH_MUC.TenHang.Contains(Tenhang.TenHang)).OrderBy(n => n.NgayTao).Take(count).ToList();
                            var a = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false && n.GiaSanPham <= 10000000 && n.CHI_TIET_DANH_MUC.TenHang.Contains(Tenhang.TenHang)).OrderBy(n => n.NgayTao).ToList();
                            ViewBag.countSpconlai = a.Skip(count).Count();
                            break;
                        }
                    case "Trên 10 triệu":
                        {
                            ViewBag.lstMayTinhBang = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false && n.GiaSanPham >= 10000000 && n.CHI_TIET_DANH_MUC.TenHang.Contains(Tenhang.TenHang)).OrderBy(n => n.NgayTao).Take(count).ToList();
                            var a = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false && n.GiaSanPham >= 10000000 && n.CHI_TIET_DANH_MUC.TenHang.Contains(Tenhang.TenHang)).OrderBy(n => n.NgayTao).ToList();
                            ViewBag.countSpconlai = a.Skip(count).Count();
                            break;
                        }


                }
            }
            if (codesearch1 == "" && codesearch2 == "" && codesearch3 != "")
            {
                if (codesearch3 == "Giá giảm dần")
                {
                    ViewBag.lstMayTinhBang = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false).OrderByDescending(n => n.GiaSanPham).Take(count).ToList();
                    var a = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false).OrderByDescending(n => n.GiaSanPham).ToList();
                    ViewBag.countSpconlai = a.Skip(count).Count();
                }
                else
                {
                    ViewBag.lstMayTinhBang = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false).OrderBy(n => n.GiaSanPham).Take(count).ToList();
                    var a = db.SAN_PHAM.Where(n => n.CHI_TIET_DANH_MUC.MaDanhMuc == 2 && n.IsDeleted == false).OrderBy(n => n.GiaSanPham).ToList();
                    ViewBag.countSpconlai = a.Skip(count).Count();
                }
            }
            return View("MayTinhBang");
        }
    }
}