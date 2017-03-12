using System;
using System.Linq;
using System.Web.Mvc;
using WebDienThoaiDiDong.Areas.Admin.Models.Danhsachgiaodich;
using WebDienThoaiDiDong.Models;


namespace WebDienThoaiDiDong.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        QuanLyBanDienThoaiEntities db = new QuanLyBanDienThoaiEntities();
        // GET: Admin/HomeAdmin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TopBanChayPartial()
        {
            return PartialView();
        }
        public ActionResult ThongKeDuLieuPartial()
        {
            return PartialView();
        }
        public ActionResult DanhsachgiaodichPartial()
        {
            return PartialView();
        }
        
       public ActionResult DanhsachgiaodichTable(int page)
        {
            int row = 1;
            int count = 0;
            int totalpages = 0;
            count = db.DON_HANG.Count(n => n.IsDeleted == false);
            if (count > 0)
            {
                totalpages = (int)Math.Ceiling((decimal)count / row);
            }
            else
            {
                totalpages = 0;
            }
            var start = row * (page - 1);
            var md = from a in db.DON_HANG
                     join b in db.KHACH_HANG on a.MaKhachHang equals b.MaKhachHang
                     select new DanhSachGiaoDichViewModel()
                     {
                         MaDonHang = a.MaDonHang,
                         TenKhachHang = b.TenKhachHang,
                         TongGiaTriDonHang = a.TongGiaTriDonHang,
                         TrangThaiDonHang = a.TrangThaiDonHang,
                         NgayTao = a.NgayTao,
                         IsDeleted = a.IsDeleted
                     };
            var result = md.Where(n => n.IsDeleted == false).OrderBy(n => n.MaDonHang).Skip(start).Take(row).ToList();
            PagingDanhSachGiaoDich model = new PagingDanhSachGiaoDich();
            model.totalpage = totalpages;
            model.record = count;
            model.List = result;
            model.page = page;
            return View(model);
        }
        public ActionResult ChiTietGiaoDich(int id , string namecus)
        {
            ViewBag.namecus = namecus;
            var md = db.CHI_TIET_DON_HANG.Where(n => n.MaDonHang == id);
            var model = from a in md
                        join b in db.SAN_PHAM on a.MaSanPham equals b.MaSanPham
                        select new ChiTietDonHangViewModel()
                        {
                            TenSanPham = b.TenSanPham,
                            ManHinh = b.ManHinh,
                            Cpu = b.Cpu,
                            Ram = b.Ram,
                            HeDieuHanh =b.HeDieuHanh,
                            CameraChinh = b.CameraChinh,
                            CameraPhu = b.CameraPhu,
                            BoNhoTrong = b.BoNhoTrong,
                            TheNhoNgoai = b.TheNhoNgoai,
                            Blutooth = b.Blutooth,
                            DungLuongPin = b.DungLuongPin,
                            GiaSanPham = b.GiaSanPham,
                            SoLuong = a.SoLuong
                        };
            var lst = model.ToList();
            return View(lst);
        }
        public bool Delete(int? id)
        {
            var model = db.DON_HANG.FirstOrDefault(n => n.MaDonHang == id);
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