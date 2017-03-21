using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebDienThoaiDiDong.Models;

namespace WebDienThoaiDiDong.Areas.Admin.Models.QuanLyTinTucSanPham
{
    public class PagingQuanLyTinTucSanPham
    {
        public int page { get; set; }
        public int totalpage { get; set; }
        public int record { get; set; }
        public List<TIN_TUC_SAN_PHAM> List { get; set; }
    }
}