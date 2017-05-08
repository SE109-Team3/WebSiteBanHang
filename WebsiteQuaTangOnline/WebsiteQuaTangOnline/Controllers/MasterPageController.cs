using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteQuaTangOnline.Controllers
{
    public class MasterPageController : Controller
    {

        public ActionResult CategorySideBar()
        {
            try
            {
                IEnumerable<WebsiteQuaTangOnline.Models.LOAISANPHAM> lstLoaiSanPham = WebsiteQuaTangOnline.Models.ModelMethod.LoadCategory();
                return View(lstLoaiSanPham);

            }
            catch
            {
                return View();
            }
            
        }
        public ActionResult HotProductSideBar()
        {
            try
            {
                IEnumerable<WebsiteQuaTangOnline.Models.SANPHAM> lstSanPham = WebsiteQuaTangOnline.Models.ModelMethod.LoadProductByHot();
                return View(lstSanPham);

            }
            catch
            {
                return View();
            }

        }
        public ActionResult NewsSideBar()
        {
            try
            {
                IEnumerable<WebsiteQuaTangOnline.Models.TINTUC> lstTinTuc = WebsiteQuaTangOnline.Models.ModelMethod.LoadTop4News();
                return View(lstTinTuc);

            }
            catch
            {
                return View();
            }

        }
    }
}
