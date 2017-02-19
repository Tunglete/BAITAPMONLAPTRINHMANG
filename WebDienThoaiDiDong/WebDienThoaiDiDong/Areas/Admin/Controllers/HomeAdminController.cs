using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebDienThoaiDiDong.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        // GET: Admin/HomeAdmin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DoanhSoPartial()
        {
            return PartialView();
        }
        public ActionResult ThongKeDuLieuPartial()
        {
            return PartialView();
        }
    }
}