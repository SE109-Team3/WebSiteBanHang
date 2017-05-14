using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using WebsiteQuaTangOnline.Models;

namespace WebsiteQuaTangOnline.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            try
            {
                // lấy ra danh sách sản phẩm
                IEnumerable<WebsiteQuaTangOnline.Models.SANPHAM> listSanPham = WebsiteQuaTangOnline.Models.ModelMethod.LoadTop8ProductByNew();
                IEnumerable<WebsiteQuaTangOnline.Models.TINTUC> listTinTuc = WebsiteQuaTangOnline.Models.ModelMethod.LoadTop4News();

                var viewModel = new IndexViewModel
                {
                    SANPHAMs = listSanPham,
                    TINTUCs = listTinTuc
                };

                return View(viewModel);
            }
            catch
            {
                return View();
            }
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

        public ActionResult NewsPage(int num=1)
        {
            try
            {
                // lấy ra danh sách tin tức
                IEnumerable<WebsiteQuaTangOnline.Models.TINTUC> listTinTuc = WebsiteQuaTangOnline.Models.ModelMethod.LoadNews(num, 4);
                return View(listTinTuc);
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(WebsiteQuaTangOnline.Models.LIENHE lienhe)
        {
            WebsiteQuaTangOnline.Models.ModelMethod.AddContact(lienhe);
            return RedirectToAction("Index");
        }
        public ActionResult InfoNews(int id)
        {
            try
            {
                // lấy ra tin tức có mã = id
                WebsiteQuaTangOnline.Models.TINTUC tt = WebsiteQuaTangOnline.Models.ModelMethod.LoadNewsInfo(id);
                // lấy danh sách tin tức khác
                ViewData["DSTin"] = WebsiteQuaTangOnline.Models.ModelMethod.LoadNews(1,10);
                return View(tt);
            }
            catch
            {
                return View();
            }
        }
        public ActionResult InfoProduct(string id)
        {
            try
            {
               // lấy ra sản phẩm có mã =id
                WebsiteQuaTangOnline.Models.SANPHAM sp = WebsiteQuaTangOnline.Models.ModelMethod.LoadProductInfo(id);
                ViewBag.loaiSP = sp.MaLoaiSanPham;
                // lấy danh sách sản phẩm liên quan
                ViewData["SanPhamLienQuan"] = WebsiteQuaTangOnline.Models.ModelMethod.LoadTop6ProductByCategory(sp.MaLoaiSanPham);
                return View(sp);
            }
            catch
            {
                return View();
            }
        }
       
        public ActionResult Cart()
        {
            WebsiteQuaTangOnline.Models.GIOHANG Gio = (WebsiteQuaTangOnline.Models.GIOHANG)Session["Gio"];
            return View(Gio);
        }
        public ActionResult AddProduct(string id)
        {
            GIOHANG gio = (GIOHANG)Session["Gio"];
            if(gio==null)
            {
                gio = new GIOHANG();
            }
            try
            {
                SANPHAM sp = new SANPHAM();
                sp = ModelMethod.LoadProductInfo(id);
                sp.SoLuong = 1;
                gio.Add(sp);
            }
            catch { }
            Session["Gio"] = gio;
            return RedirectToAction("Cart");
        }
        public ActionResult DeleteProduct(string id)
        {
            GIOHANG gio = (GIOHANG)Session["Gio"];
            try
            {
                gio.Delete(id);
            }
            catch { }
            Session["Gio"] = gio;
            return RedirectToAction("Cart");
        }

        public ActionResult UpdateProduct(string id,int soluong)
        {
            GIOHANG gio = (GIOHANG)Session["Gio"];
            try
            {
                gio.Update(id,soluong);
            }
            catch { }
            Session["Gio"] = gio;
            return RedirectToAction("Cart");
        }
        public ActionResult Pay()
        {
            GIOHANG gio = (GIOHANG)Session["Gio"];
            return View(gio);
        }
        [HttpPost]
        public ActionResult Pay(WebsiteQuaTangOnline.Models.HOADON hoadon)
        {
            // thêm hóa đơn
            hoadon.NgayLap = new DateTime();
            hoadon.NgayLap = DateTime.Today;
            hoadon.TrangThai = 0;
            WebsiteQuaTangOnline.Models.ModelMethod.AddBill(hoadon);
            // lấy danh sách giỏ hàng
            GIOHANG gio= (GIOHANG)Session["Gio"];
            // thêm chi tiết hóa đơn
            foreach(var item in gio.ListProduct)
            {
                // tạo chi tiết hóa đơn
                CTHD ct = new CTHD();
                ct.MaHoaDon = ModelMethod.LoadIdBill();
                ct.MaSanPham = item.MaSanPham;
                ct.SoLuong = item.SoLuong;
                ct.ThanhTien = item.SoLuong * item.DonGia;
                ModelMethod.AddDetailBill(ct);
                
            }
            return RedirectToAction("PaySuccess");
        }
        public ActionResult PaySuccess()
        {
            return View();
        }
    }
}
