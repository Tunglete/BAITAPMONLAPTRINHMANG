//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebDienThoaiDiDong.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CHI_TIET_DON_HANG
    {
        public int MaCTDH { get; set; }
        public int MaDonHang { get; set; }
        public int MaSanPham { get; set; }
        public Nullable<int> Gia { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual DON_HANG DON_HANG { get; set; }
        public virtual SAN_PHAM SAN_PHAM { get; set; }
    }
}
