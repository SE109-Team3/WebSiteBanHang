using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteQuaTangOnline.Models
{
    public class IndexViewModel
    {
        public IEnumerable<WebsiteQuaTangOnline.Models.SANPHAM> SANPHAMs { get; set; }
        public IEnumerable<WebsiteQuaTangOnline.Models.TINTUC> TINTUCs { get; set; }
    }
}