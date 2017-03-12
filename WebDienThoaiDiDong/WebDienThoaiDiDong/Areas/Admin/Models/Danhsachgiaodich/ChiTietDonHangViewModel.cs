using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebDienThoaiDiDong.Models;

namespace WebDienThoaiDiDong.Areas.Admin.Models.Danhsachgiaodich
{
    public class ChiTietDonHangViewModel:CHI_TIET_DON_HANG
    {
        public Nullable<int> MaKhachHang { get; set; }
        public string TenKhachHang { get; set; }
        public Nullable<int> GiaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string ManHinh { get; set; }
        public string Cpu { get; set; }
        public string Ram { get; set; }
        public string HeDieuHanh { get; set; }
        public string CameraChinh { get; set; }
        public string CameraPhu { get; set; }
        public string BoNhoTrong { get; set; }
        public string TheNhoNgoai { get; set; }
        public string Blutooth { get; set; }
        public string DungLuongPin { get; set; }
        public int tonggia { get { return ((int)GiaSanPham*(int)SoLuong); } }
        public bool IsRowspan { get; set; }
        public int NumberOfRowspan { get; set; }
    }
}