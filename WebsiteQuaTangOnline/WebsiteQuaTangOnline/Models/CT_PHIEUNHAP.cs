//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebsiteQuaTangOnline.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CT_PHIEUNHAP
    {
        public int MaChiTiet { get; set; }
        public int MaPhieuNhap { get; set; }
        public string MaSanPham { get; set; }
        public int SoLuong { get; set; }
        public int DonGia { get; set; }
        public int ThanhTien { get; set; }
    
        public virtual PHIEUNHAP PHIEUNHAP { get; set; }
        public virtual SANPHAM SANPHAM { get; set; }
    }
}
