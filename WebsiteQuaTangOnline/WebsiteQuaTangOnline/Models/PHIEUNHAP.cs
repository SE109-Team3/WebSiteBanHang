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
    
    public partial class PHIEUNHAP
    {
        public PHIEUNHAP()
        {
            this.CT_PHIEUNHAP = new HashSet<CT_PHIEUNHAP>();
        }
    
        public int MaPhieuNhap { get; set; }
        public System.DateTime NgayNhap { get; set; }
        public string NhaSanXuat { get; set; }
        public int TongTien { get; set; }
    
        public virtual ICollection<CT_PHIEUNHAP> CT_PHIEUNHAP { get; set; }
    }
}
