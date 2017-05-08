using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace WebsiteQuaTangOnline.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {           
            return View();
        }

        [HttpGet]
        public ActionResult ProductPage(string id="",int min=1)
        {
            try
            {
                // lấy ra danh sách sản phẩm
                IEnumerable<WebsiteQuaTangOnline.Models.SANPHAM> listSanPham = WebsiteQuaTangOnline.Models.ModelMethod.LoadProductByCategory(id, min, 10);
                // lấy ra số trang
                ViewBag.sotrang = WebsiteQuaTangOnline.Models.ModelMethod.CountProduct(id)/10+1;
                // lưu loại sản phẩm 
                ViewBag.loaiSP = id;
                //lấy ra trang hiện tại
                ViewBag.trangHienTai = min;
                return View(listSanPham);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult NewsPage()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult InfoNews()
        {
            return View();
        }
        public ActionResult InfoProduct()
        {
            return View();
        }
        public ActionResult Cart()
        {
            return View();
        }
        public ActionResult Pay()
        {
            return View();
        }

    }
}
