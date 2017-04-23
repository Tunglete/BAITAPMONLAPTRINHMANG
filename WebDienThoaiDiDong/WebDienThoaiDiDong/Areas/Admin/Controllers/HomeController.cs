using System;
using System.Linq;
using System.Web.Mvc;
using WebDienThoaiDiDong.Areas.Admin.Models.Danhsachgiaodich;
using WebDienThoaiDiDong.Models;


namespace WebDienThoaiDiDong.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        QuanLyBanDienThoaiEntities db = new QuanLyBanDienThoaiEntities();
        // GET: Admin/HomeAdmin
        public ActionResult Index()
        {
            if (Session["TenQuanTri"] != null && Session["MaQuanTri"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "DangNhapAdmin");
            }
        }
        public ActionResult TopBanChayPartial()
        {
            var group = db.CHI_TIET_DON_HANG.GroupBy(n => n.MaSanPham);
            ViewBag.count1 = 0;
            ViewBag.count2 = 0;
            ViewBag.count3 = 0;
            ViewBag.sp1 = "";
            ViewBag.sp2 = "";
            ViewBag.sp3 = "";
            ViewBag.phantram1 = 0.0;
            ViewBag.phantram2 = 0.0;
            ViewBag.phantram3 = 0.0;
            int countsp = db.CHI_TIET_DON_HANG.Count();
            int[] sapxep = new int[100];
            int demSapxep = 0;
            foreach (var item in group)
            {
              sapxep[demSapxep] = item.Count();
                demSapxep++;
                
            }
            demSapxep = 0;
            for (int i = 0; i < group.Count() - 1; i++)
            {
                for (int j = i + 1; j < group.Count(); j++)
                {
                    if (sapxep[j] > sapxep[i])
                    {
                        int temp = sapxep[i];
                        sapxep[i] = sapxep[j];
                        sapxep[j] = temp;
                    }
                }
            }
            bool check1 = true;
            bool check2 = true;
            bool check3 = true;
            foreach (var item in group)
            {
                if(item.Count() == sapxep[0] && check1 == true)
                {
                    ViewBag.count1 = item.Count();
                    ViewBag.sp1 = db.SAN_PHAM.FirstOrDefault(n => n.MaSanPham == item.Key).TenSanPham;
                    ViewBag.phantram1 = (float)(ViewBag.count1) / (float)(countsp)*100;
                    check1 = false;
                    continue;
                    
                }
                if(item.Count() == sapxep[1] && check2 == true)
                {
                    ViewBag.count2 = item.Count();
                    ViewBag.sp2 = db.SAN_PHAM.FirstOrDefault(n => n.MaSanPham == item.Key).TenSanPham;
                    ViewBag.phantram2 = (float)(ViewBag.count2) / (float)(countsp) * 100;
                    check2 = false;
                    continue;
                }
                if(item.Count() == sapxep[2] && check3 == true)
                {
                    ViewBag.count3 = item.Count();
                    ViewBag.sp3 = db.SAN_PHAM.FirstOrDefault(n => n.MaSanPham == item.Key).TenSanPham;
                    ViewBag.phantram3 = (float)(ViewBag.count3) / (float)(countsp) * 100;
                    check3 = false;
                    continue;
                }
            }
            return PartialView();
        }
        public ActionResult ThongKeDuLieuPartial()
        {
            ViewBag.Tongtaikhoan = db.KHACH_HANG.Count();
            //ViewBag.Dathangtrongngay = db.DON_HANG.Count(n => n.NgayTao.ToString().Trim().Contains("/20"));
            var all = db.DON_HANG;
            int count = 0;
            foreach (var item in all)
            {
                if(item.NgayTao.ToString().Contains("/"+ DateTime.Now.Day.ToString() +"/") == true)
                {
                    count++;
                }
            }
            ViewBag.Dathangtrongngay = count;
            var a = db.DON_HANG.FirstOrDefault(n => n.MaDonHang == 8).NgayTao.ToString();
            ViewBag.Tongdondathang = db.DON_HANG.Count();
            ViewBag.Hoadoncho = db.DON_HANG.Where(n => n.TrangThaiDonHang == "Chưa Thanh Toán").Count();
            return PartialView();
        }
        public ActionResult DanhsachgiaodichPartial()
        {
            return PartialView();
        }
        
       public ActionResult DanhsachgiaodichTable(int page)
        {
            int row = 5;
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
            var result = md.Where(n => n.IsDeleted == false).OrderByDescending(n => n.NgayTao).Skip(start).Take(row).ToList();
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
        public ActionResult DangXuat()
        {
            Session["TenQuanTri"] = null;
            Session["MaQuanTri"] = null;
            return RedirectToAction("Index", "DangNhap");
        }
    }
}