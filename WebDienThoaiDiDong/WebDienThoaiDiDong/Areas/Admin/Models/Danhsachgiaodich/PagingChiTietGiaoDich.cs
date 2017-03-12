using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDienThoaiDiDong.Areas.Admin.Models.Danhsachgiaodich
{
    public class PagingChiTietGiaoDich
    {
        public int page { get; set; }
        public int totalpage { get; set; }
        public int record { get; set; }
        public List<ChiTietDonHangViewModel> List { get; set; }
    }
}