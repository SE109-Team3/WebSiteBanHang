using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteQuaTangOnline.Controllers
{
    public class AdminController : Controller
    {

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult AdminProduct(int min = 1)
        {
            // Load sản phẩm
            try
            {
                // lấy ra danh sách sản phẩm
                IEnumerable<WebsiteQuaTangOnline.Models.SANPHAM> listSanPham = WebsiteQuaTangOnline.Models.ModelMethod.LoadProduct();
                listSanPham =listSanPham.Skip(10 * (min - 1)).Take(10).ToList();
                ViewBag.tranghientai = min;
                ViewBag.soluongtrang = (WebsiteQuaTangOnline.Models.ModelMethod.LoadProduct().Count() / 10 + 1);
                return View(listSanPham);
            }
            catch
            {
                return View();
            }
        }
        public ActionResult AddProduct()
        {
            return View();
        }
        public ActionResult UpdateProduct()
        {
            return View();
        }
        public ActionResult AdminNews(int num=1)
        {
            // lấy danh sách tin tức
            IEnumerable<WebsiteQuaTangOnline.Models.TINTUC> dsTinTuc = WebsiteQuaTangOnline.Models.ModelMethod.LoadNews(num, 15);
            return View(dsTinTuc);
        }
        public ActionResult AddNews()
        {
            return View();
        }
        public ActionResult UpdateNews()
        {
            return View();
        }
        public ActionResult AdminCategory()
        {
            IEnumerable<WebsiteQuaTangOnline.Models.LOAISANPHAM> dsLoaiSanPham = WebsiteQuaTangOnline.Models.ModelMethod.LoadCategory();
            return View(dsLoaiSanPham);
        }
        public ActionResult AddCategory()
        {
            return View();
        }
        public ActionResult UpdateCategory()
        {
            return View();
        }
        public ActionResult AdminShipment()
        {
            return View();
        }

        public ActionResult AdminContact(int num=1)
        {
            //lấy danh sách liên hệ
            IEnumerable<WebsiteQuaTangOnline.Models.LIENHE> dsLienHe = WebsiteQuaTangOnline.Models.ModelMethod.LoadContact();
            //Lấy ra dúng trang cần lấy
            var list = dsLienHe.Skip(15 * (num - 1)).Take(15).ToList();
            return View(list);
        }

        #region chưa hoàn thành
        public ActionResult AdminStatistical()
        {
            return View();
        }

        // chưa hoàn thành
        [HttpPost]
        public ActionResult AdminStatistical(int ltk = 1, int nam = 2017, int thang = 1, int ngay = 1)
        {
            // viết hàm thống kê
            return RedirectToAction("AdminStatistical");
        }
        #endregion
    }
}
