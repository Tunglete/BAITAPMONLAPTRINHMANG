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
    
    public partial class ANH_SAN_PHAM
    {
        public int MaAnhSanPham { get; set; }
        public int MaSanPham { get; set; }
        public string AnhMinhHoa { get; set; }
    
        public virtual SAN_PHAM SAN_PHAM { get; set; }
    }
}