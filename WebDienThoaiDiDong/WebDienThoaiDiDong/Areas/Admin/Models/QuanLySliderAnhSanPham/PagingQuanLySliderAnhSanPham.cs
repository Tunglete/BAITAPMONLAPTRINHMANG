using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebDienThoaiDiDong.Models;

namespace WebDienThoaiDiDong.Areas.Admin.Models.QuanLySliderAnhSanPham
{
    public class PagingQuanLySliderAnhSanPham
    {
        public int page { get; set; }
        public int totalpage { get; set; }
        public int record { get; set; }
        public List<ANH_SAN_PHAM> List { get; set; }
    }
}