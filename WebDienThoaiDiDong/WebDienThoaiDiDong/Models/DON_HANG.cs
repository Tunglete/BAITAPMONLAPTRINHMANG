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
    
    public partial class DON_HANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DON_HANG()
        {
            this.CHI_TIET_DON_HANG = new HashSet<CHI_TIET_DON_HANG>();
        }
    
        public int MaDonHang { get; set; }
        public Nullable<int> MaKhachHang { get; set; }
        public string TrangThaiDonHang { get; set; }
        public Nullable<System.DateTime> NgayTao { get; set; }
        public Nullable<int> TongGiaTriDonHang { get; set; }
        public string DiaChiNhanDonHang { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHI_TIET_DON_HANG> CHI_TIET_DON_HANG { get; set; }
        public virtual KHACH_HANG KHACH_HANG { get; set; }
    }
}
