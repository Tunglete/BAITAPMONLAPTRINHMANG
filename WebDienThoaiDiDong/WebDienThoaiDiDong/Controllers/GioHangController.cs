using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDienThoaiDiDong.Models;
using System.Web.Script.Serialization;

namespace WebDienThoaiDiDong.Controllers
{
    public class GioHangController : Controller
    {
        QuanLyBanDienThoaiEntities db = new QuanLyBanDienThoaiEntities();
        private const string  CartSession = "CartSession";
        // GET: GioHang
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            
            return View(list);
        }
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];
            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(n => n.Sanpham.MaSanPham == item.Sanpham.MaSanPham);
                if(jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }

            }
            Session[CartSession] = sessionCart;
            return Json(new {
                status = true
            });
        }
        public JsonResult DeleteItem(int id)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];
            sessionCart.RemoveAll(n => n.Sanpham.MaSanPham == id);
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;
            return Json(new
            {
                status = true
            });
        }
        public ActionResult AddItem(long masanpham, int quantity, int giasanpham, string mausac)
        {
            var sanpham = db.SAN_PHAM.FirstOrDefault(n => n.MaSanPham == masanpham);
            var cart = Session[CartSession];
            if(cart != null)
            {
                var list = (List<CartItem>)cart;
                if(list.Exists(n => n.Sanpham.MaSanPham == masanpham))
                {
                    foreach (var item in list)
                    {
                        if (item.Sanpham.MaSanPham == masanpham)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    // tạo mới đối tượng cartitem
                    var item = new CartItem();
                    item.Sanpham = sanpham;
                    item.Quantity = quantity;
                    item.Giasanpham = giasanpham;
                    item.Mausac = mausac;
                    list.Add(item);
                }
                //Gán vào Session
                Session[CartSession] = list;
                
            }
            else
            {
                //Tạo mới đối tượng cartItem
                var item = new CartItem();
                item.Sanpham = sanpham;
                item.Quantity = quantity;
                item.Giasanpham = giasanpham;
                item.Mausac = mausac;

                var list = new List<CartItem>();
                list.Add(item);
                //Gán vào Session
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }

        public JsonResult DatHang(string name = "", string phone = "", string city = "", string address = "" )
        {
            if(!name.Equals("") && !phone.Equals("") && !city.Equals("") && !address.Equals("")){
                // thêm khách hàng với khách hàng không lưu tên đăng nhập mà lưu như một tài khoản tạm thời
                KHACH_HANG kh = new KHACH_HANG();
                kh.TenKhachHang = name;
                kh.DienThoai = phone;
                kh.DiaChi =  address + "" + city;
                kh.IsDeleted = false;
                db.KHACH_HANG.Add(kh);
                db.SaveChanges();

                // thêm đơn hàng
                var countKh = db.KHACH_HANG.Count();
                DON_HANG dh = new DON_HANG();
                var cart = (List<CartItem>)Session[CartSession];
                var tonggiatridonhang = 0;
                foreach (var item in cart)
                {
                    tonggiatridonhang += item.Giasanpham * item.Quantity;

                }
                dh.MaKhachHang = countKh;
                dh.TrangThaiDonHang = "Chưa thanh toán";
                dh.NgayTao = DateTime.Now;
                dh.TongGiaTriDonHang = tonggiatridonhang;
                dh.DiaChiNhanDonHang = address + "" + city;
                dh.IsDeleted = false;
                db.DON_HANG.Add(dh);
                db.SaveChanges();

                // Thêm chi tiết đơn hàng
                var countDonhang = db.DON_HANG.Count() +1;
                CHI_TIET_DON_HANG ctdh = new CHI_TIET_DON_HANG();
                foreach (var item in cart)
                {
                    ctdh.MaDonHang = countDonhang;
                    ctdh.MaSanPham = item.Sanpham.MaSanPham;
                    ctdh.Gia = item.Giasanpham;
                    ctdh.SoLuong = item.Quantity;
                    ctdh.MauSac = item.Mausac;
                    ctdh.IsDeleted = false;
                    db.CHI_TIET_DON_HANG.Add(ctdh);
                    db.SaveChanges();
                }

            }
            if(Session["TenDangNhap"] != null)
            {
                var tendangnhap = Session["TenDangNhap"];
                var model = db.KHACH_HANG.FirstOrDefault(n => n.TenDangNhap == tendangnhap.ToString());
                if(model != null)
                {
                    
                    // thêm đơn hàng
                    DON_HANG dh = new DON_HANG();
                    var cart = (List<CartItem>)Session[CartSession];
                    var tonggiatridonhang = 0;
                    foreach (var item in cart)
                    {
                        tonggiatridonhang += item.Giasanpham * item.Quantity;

                    }
                    dh.MaKhachHang = model.MaKhachHang;
                    dh.TrangThaiDonHang = "Chưa thanh toán";
                    dh.NgayTao = DateTime.Now;
                    dh.TongGiaTriDonHang = tonggiatridonhang;
                    dh.DiaChiNhanDonHang = model.DiaChi;
                    dh.IsDeleted = false;
                    db.DON_HANG.Add(dh);
                    db.SaveChanges();

                    // Thêm chi tiết đơn hàng
                    var countDonhang = db.DON_HANG.Count() + 1;
                    CHI_TIET_DON_HANG ctdh = new CHI_TIET_DON_HANG();
                    foreach (var item in cart)
                    {
                        ctdh.MaDonHang = countDonhang;
                        ctdh.MaSanPham = item.Sanpham.MaSanPham;
                        ctdh.Gia = item.Giasanpham;
                        ctdh.SoLuong = item.Quantity;
                        ctdh.MauSac = item.Mausac;
                        ctdh.IsDeleted = false;
                        db.CHI_TIET_DON_HANG.Add(ctdh);
                        db.SaveChanges();
                    }
                }
            }
            Session[CartSession] = null;
            return Json(new {
                status = true
            });
        }
    }
}