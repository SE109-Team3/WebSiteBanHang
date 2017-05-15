using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteQuaTangOnline.Models;

namespace WebsiteQuaTangOnline.Controllers
{
    public class AdminController : Controller
    {

        public ActionResult HomeAdmin()
        {
            if(Session["DangNhap"]==null || (int)Session["DangNhap"]==0)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public ActionResult Login()
        {
            if(Session["DangNhap"]==null)
            {
                Session["DangNhap"] = 0;
            }
            if((int)(Session["DangNhap"])==1)
            {
                return RedirectToAction("HomeAdmin");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(string TaiKhoan,string MatKhau)
        {
            DANGNHAP tk = new DANGNHAP();
            tk.TaiKhoang = TaiKhoan;
            tk.MatKhau = MatKhau;
            if(ModelMethod.Login(tk))
            {
                Session["DangNhap"] = 1;
                return RedirectToAction("HomeAdmin");
            }
            return RedirectToAction("Login");
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
        public ActionResult UpdateNews(int id)
        {
           WebsiteQuaTangOnline.Models.TINTUC tin= WebsiteQuaTangOnline.Models.ModelMethod.LoadNewsInfo(id);
            return View(tin);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateNewsSuccess(WebsiteQuaTangOnline.Models.TINTUC tin)
        {
            // code update
            tin.NgaySua = System.DateTime.Today;
            WebsiteQuaTangOnline.Models.ModelMethod.UpdateNews(tin);
            
            return RedirectToAction("AdminNews");
        }
        public ActionResult AdminCategory()
        {
            // lấy danh sách loại sản phẩm
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
        public ActionResult AdminShipment(int num=1)
        {
            // lấy danh sách các hóa đơn chưa duyệt
            IEnumerable<WebsiteQuaTangOnline.Models.HOADON> dsHoaDon = WebsiteQuaTangOnline.Models.ModelMethod.LoadBill();
            // lấy trang hiện tại
            ViewBag.tranghientai = num;
            // lấy số lượng trang
            ViewBag.soluongtrang = (WebsiteQuaTangOnline.Models.ModelMethod.LoadBill().Count() / 10 + 1);
            dsHoaDon=dsHoaDon.Skip(20 * (num - 1)).Take(20);
            
            return View(dsHoaDon);
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
