using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDienThoaiDiDong.Models
{
    [Serializable]
    public class CartItem
    {
        public SAN_PHAM Sanpham { get; set; }
        public  int Quantity { get; set; }
        public int Giasanpham { get; set; }
    }
}