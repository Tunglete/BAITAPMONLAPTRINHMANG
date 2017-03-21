using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebDienThoaiDiDong.Models;
namespace WebDienThoaiDiDong.Areas.Admin.Models.Quanlysanpham
{
    public class PagingQuanLySanPham
    {
        public int page { get; set; }
        public int totalpage { get; set; }
        public int record { get; set; }
        public List<SAN_PHAM> List { get; set; }
    }
}