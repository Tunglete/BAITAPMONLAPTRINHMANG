using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDienThoaiDiDong.Models;

namespace WebDienThoaiDiDong.Controllers
{
    public class DatHangController : Controller
    {
        // GET: DatHang
        QuanLyBanDienThoaiEntities db = new QuanLyBanDienThoaiEntities();
        List<SAN_PHAM> lstSanpham = new List<SAN_PHAM>();
        public ActionResult MuaHangOnline(int masanpham = 0)
        {
            var sanpham = db.SAN_PHAM.FirstOrDefault(n => n.MaSanPham == masanpham);
            if(sanpham != null)
            {
                if(Session["sanpham"] == null)
                {
                    Session["Sosanpham"] = 1;
                    lstSanpham.Add(sanpham);
                }
                else
                {
                    lstSanpham = (List<SAN_PHAM>)Session["sanpham"];
                    lstSanpham.Add(sanpham);
                }
                
               
            }
                Session["sanpham"] = lstSanpham;
            return View();
        }
        public ActionResult ListViewSanPham()
        {
            lstSanpham = (List<SAN_PHAM>)Session["sanpham"];
            
            if (Session["sanpham"] != null)
            {
                ViewBag.lstSanPham = lstSanpham;
            }
            return View();
        }
        public ActionResult XoaSanPham(int id)
        {
            lstSanpham = (List<SAN_PHAM>)Session["sanpham"];
            if (Session["sanpham"] != null)
            {
                foreach (var item in lstSanpham)
                {
                    if( item.MaSanPham == id)
                    {
                        lstSanpham.Remove(item);
                        break;
                    }
                }
                ViewBag.lstSanPham = lstSanpham;
            }
            return View("ListViewSanPham");
        }
        public ActionResult MuaTaiCuaHang(int masanpham = 0)
        {
            return View();
        }
    }
}