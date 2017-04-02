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
        public ActionResult AddItem(long masanpham, int quantity, int giasanpham)
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

                var list = new List<CartItem>();
                list.Add(item);
                //Gán vào Session
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }
    }
}