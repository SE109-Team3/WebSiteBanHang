using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;

namespace WebsiteQuaTangOnline.Models
{
    public class ModelMethod
    {
        static QLBanHangOnlineEntities db = new QLBanHangOnlineEntities();
        /// <summary>
        /// Phương thức mã hóa MD5
        /// </summary>
        /// <param name="s">Chuổi cần mã hóa</param>
        /// <returns>Chuổi đã mã hóa MD5</returns>
        public static string GetMD5(string s)
        {
            string str_md5 = "";
            byte[] mang = System.Text.Encoding.UTF8.GetBytes(s);

            MD5CryptoServiceProvider my_md5 = new MD5CryptoServiceProvider();
            mang = my_md5.ComputeHash(mang);

            foreach (byte b in mang)
            {
                str_md5 += b.ToString("x2");//Nếu là "X2" thì kết quả sẽ tự chuyển sang ký tự in Hoa
            }

            return str_md5;
        }
        #region Các phương thức cho DANGNHAP
        public static void AddAdmin(DANGNHAP taikhoan)
        {
            db.DANGNHAPs.Add(taikhoan);
            db.SaveChanges();
        }
        public static void UpdateAdmin(DANGNHAP taikhoan)
        {
            var data = db.DANGNHAPs.Where(tk => (tk.TaiKhoang.Equals(taikhoan.TaiKhoang) && (tk.MatKhau.Equals(GetMD5(taikhoan.MatKhau))))).SingleOrDefault();
            if (data != null)
            {
                data.MatKhau = taikhoan.MatKhau;
            }
            db.SaveChanges();
        }
        #endregion
        #region Các phương thức cho LOAISANPHAM
        public static void AddCategory(LOAISANPHAM loaisp)
        {
            db.LOAISANPHAMs.Add(loaisp);
            db.SaveChanges();
        }
        public static void UpdateCategory(LOAISANPHAM loaisp)
        {
            var data = db.LOAISANPHAMs.Where(lsp => lsp.MaLoaiSanPham.Equals(loaisp.MaLoaiSanPham)).Single();
            if (data != null)
            {
                data.TenLoaiSanPham = loaisp.TenLoaiSanPham;
            }
            db.SaveChanges();
        }
        public static void DeleteCategory(string Id)
        {
            var data = db.LOAISANPHAMs.Where(lsp => lsp.MaLoaiSanPham.Equals(Id)).SingleOrDefault();
            try
            {
                if (data != null)
                {
                    db.LOAISANPHAMs.Remove(data);
                }
                db.SaveChanges();
            }
            catch
            { }
        }
        public static IEnumerable<LOAISANPHAM> LoadCategory()
        {
            var data = (from lsp in db.LOAISANPHAMs
                        select lsp);
            return data;
        }
        #endregion
        #region các phương thức cho SANPHAM
        /// <summary>
        /// Phương thức thêm mới nột sản phẩm
        /// </summary>
        /// <param name="sp">Sản phẩm cần thêm mới</param>
        public static void AddProduct(SANPHAM sp)
        {
            db.SANPHAMs.Add(sp);
            db.SaveChanges();
        }

        /// <summary>
        /// Phương thức Cập nhật thông tin sản phẩm 
        /// </summary>
        /// <param name="sp">Sản phẩm cần cập nhật thông thin</param>
        public static void UpdateProduct(SANPHAM sp)
        {
            var data = db.SANPHAMs.Where(sanpham => sanpham.MaSanPham.Equals(sp.MaSanPham)).SingleOrDefault();
            if (data != null)
            {
                data.TenSanPham = sp.TenSanPham;
                data.NhaSanXuat = sp.NhaSanXuat;
                data.UrlHinhAnh = sp.UrlHinhAnh;
                data.MoTa = sp.MoTa;
                data.SoLuong = sp.SoLuong;
                data.SoLuotMua = sp.SoLuotMua;
            }
            db.SaveChanges();
        }

        /// <summary>
        /// Phương thức xóa sản phẩm
        /// </summary>
        /// <param name="id">mã sản phẩm cần xóa</param>
        public static void DeleteProduct(string id)
        {
            var data = db.SANPHAMs.Where(sanpham => sanpham.MaSanPham.Equals(id)).SingleOrDefault();
            if (data != null)
            {
                db.SANPHAMs.Remove(data);
            }
            db.SaveChanges();
        }

        /// <summary>
        /// Phương thức trả về top 8 sản phẩm có ID giảm dần (mới nhất).
        /// </summary>
        /// <returns>Danh sách sản phẩm</returns>
        public static IEnumerable<SANPHAM> LoadTop8ProductByNew()
        {
            IEnumerable<SANPHAM> data;
            data = (from sp in db.SANPHAMs orderby sp.MaSanPham select sp);

            return data.Take(8);
        }

        /// <summary>
        /// Phương thức lấy danh sách sản phẩm theo thể loại
        /// </summary>
        /// <param name="malsp">Mã loại sản phẩm</param>
        /// <param name="min">chỉ số trang trong bảng phân trang</param>
        /// <param name="number">số lượng bảng gi lấy về</param>
        /// <returns>Danh sách sản phẩm</returns>
        public static IEnumerable<SANPHAM> LoadProductByCategory(string malsp,int min=1, int number=10)
        {
            IEnumerable<SANPHAM> temp;
            
            if(malsp=="")
            {
                temp = (from sp in db.SANPHAMs select sp).ToList();
            }
            else
            {
                temp = (from sp in db.SANPHAMs where sp.MaLoaiSanPham.Equals(malsp) select sp).ToList();
            }
            var list=temp.Skip(number * (min - 1)).Take(number).ToList();
            return list;
        }
        public static IEnumerable<SANPHAM> LoadTop6ProductByCategory(string malsp)
        {
            IEnumerable<SANPHAM> temp;

            if (malsp == "")
            {
                temp = (from sp in db.SANPHAMs select sp).ToList();
            }
            else
            {
                temp = (from sp in db.SANPHAMs where sp.MaLoaiSanPham.Equals(malsp) select sp).ToList();
            }  
            return temp.Take(6);
        }
        public static int CountProduct(string malsp)
        {
            IEnumerable<SANPHAM> temp;
            if (malsp == "")
            {
                temp = (from sp in db.SANPHAMs select sp).ToList();
            }
            else
            {
                temp = (from sp in db.SANPHAMs where sp.MaLoaiSanPham.Equals(malsp) select sp).ToList();
            }
            return temp.Count();
        }

        /// <summary>
        /// Phương thức lấy về 5 sản phẩm mua nhiều nhất
        /// </summary>
        /// <returns>danh sách sản phẩm</returns>
        public static IEnumerable<SANPHAM> LoadProductByHot()
        {
            IEnumerable<SANPHAM> data = (from sp in db.SANPHAMs orderby sp.SoLuotMua select sp);
            return data.Take(5);
        }

        /// <summary>
        /// Phương thức lấy về danh sách sản phẩm có ID tăng dần
        /// Lấy từ bảng ghi min-1 đến bảng ghi min+number-1
        /// </summary>
        /// <param name="min">chỉ số trang trong bảng phân trang</param>
        /// <param name="number">số lượng bảng ghi cần lấy</param>
        /// <returns></returns>
        public static IEnumerable<SANPHAM> LoadProduct()
        {
            var temp = (from sp in db.SANPHAMs select sp).ToList();
            return temp;
        }

        /// <summary>
        /// Phương thức lấy về thông tin chi tiết của 1 sản phẩm theo ID
        /// </summary>
        /// <param name="ID">mã sản phẩm</param>
        /// <returns>Thông tin sản phẩm</returns>
        public static SANPHAM LoadProductInfo(string ID)
        {
            var data = db.SANPHAMs.Where(sanpham => sanpham.MaSanPham.Equals(ID)).Single();
            return data;
        }
        #endregion
        #region Các phương thức cho TINTUC
        /// <summary>
        /// Phương thức lấy về danh sách tin tức
        /// </summary>
        /// <param name="min">chỉ số trang trong bảng phân trang</param>
        /// <param name="number">số lượng bảng ghi 1 trang</param>
        /// <returns>danh sách tin tức</returns>
        public static IEnumerable<TINTUC> LoadNews(int min=1, int number=4)
        {
            IEnumerable<TINTUC> data;
            data = (from t in db.TINTUCs
                    select t);
            return data.Skip(number * (min - 1)).Take(number).ToList();
        }
        /// <summary>
        /// Phương thức lấy top 4 tin tức mới nhất
        /// </summary>
        /// <returns>Danh sách tin</returns>
        public static IEnumerable<TINTUC> LoadTop4News()
        {
            IEnumerable<TINTUC> data;
            data = (from t in db.TINTUCs
                    orderby t.MaTinTuc
                    select t);
            return data.Take(4);
        }

        /// <summary>
        /// Phương thức lấy về thông tin chi tiết của tin tức
        /// </summary>
        /// <param name="id">mã tin cần lấy chi tiết tin</param>
        /// <returns>Thông tin tin tức</returns>
        public static TINTUC LoadNewsInfo(int id)
        {
            TINTUC data;
            data = (from t in db.TINTUCs
                    where t.MaTinTuc.Equals(id)
                    select t).Single();
            return data;
        }

        /// <summary>
        /// Phương thức thêm tin tức mới
        /// </summary>
        /// <param name="tin">tin tức cần thêm</param>
        public static void AddNews(TINTUC tin)
        {
            db.TINTUCs.Add(tin);
            db.SaveChanges();
        }

        /// <summary>
        /// Phương thức cập nhật thông tin tin tức
        /// </summary>
        /// <param name="tin">tin tức cần cập nhật</param>
        public static void UpdateNews(TINTUC tin)
        {
            TINTUC data;
            data = (from t in db.TINTUCs
                    where t.MaTinTuc.Equals(tin.MaTinTuc)
                    select t).Single();
            if (data != null)
            {
                data.MoTa = tin.MoTa;
                data.NgayDang = tin.NgayDang;
                data.NgaySua = tin.NgaySua;
                data.NoiDung = tin.NoiDung;
                data.SoLuotxem = tin.SoLuotxem;
                data.TacGia = tin.TacGia;
                data.TieuDe = tin.TieuDe;
                data.UrlHinhAnh = tin.UrlHinhAnh;
                db.SaveChanges();
            }
        }

        /// <summary>
        ///  Phương thức xóa một tin tin tức
        /// </summary>
        /// <param name="id">mã tin tức cần xóa</param>
        public static void DeleteNews(int id)
        {
            var data = db.TINTUCs.Where(tin => tin.MaTinTuc.Equals(id)).Single();
            if (data != null)
            {
                db.TINTUCs.Remove(data);
            }
            db.SaveChanges();
        }

        #endregion
        #region Các phương thức cho HOADON
        /// <summary>
        /// Phương thức thêm một hóa đơn
        /// </summary>
        /// <param name="hoadon">Hóa đơn cần thêm</param>
        public static void AddBill(HOADON hoadon)
        {
            db.HOADONs.Add(hoadon);
            db.SaveChanges();
        }

        /// <summary>
        /// Phương thức sửa một hóa đơn
        /// </summary>
        /// <param name="hoadon">Hóa đơn cần sửa</param>
        public static void UpdateBill(HOADON hoadon)
        {
            var data = (from hd in db.HOADONs
                        where hd.MaHoaDon == hoadon.MaHoaDon
                        select hd).Single();
            if (data != null)
            {
                data.NoiDung = hoadon.NoiDung;
                data.SoDienThoai = hoadon.SoDienThoai;
                data.TenKhachHang = hoadon.TenKhachHang;
                data.TrangThai = hoadon.TrangThai;
                data.DiaChi = hoadon.DiaChi;
                data.Email = hoadon.Email;
                data.NgayLap = hoadon.NgayLap;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Phương thức tính thành tiền của hóa đơn
        /// </summary>
        /// <param name="id">mã hóa đơn cần tính</param>
        /// <returns>thành tiền của hóa đơn</returns>
        public static int IntoMoney(int id)
        {
            int tongtien = 0;
            // lấy ra hóa đơn
            var hoadon = (from hd in db.HOADONs
                          where hd.MaHoaDon == id
                          select hd).Single();
            if (hoadon != null)
            {
                // lấy về danh sách chi tiết hóa đơn
                var cthd = (from ct in db.CTHDs
                            where ct.MaHoaDon == id
                            select ct);
                foreach (var ct in cthd)
                {
                    tongtien += ct.ThanhTien;
                }

            }
            return tongtien;
        }

        /// <summary>
        /// Phương thức sửa trạng thái hóa đơn.
        /// Dùng khi duyệt một hóa đơn
        /// </summary>
        /// <param name="Id">mã hóa đơn cần sửa trạng thái</param>
        public static void UpdateBillStatus(int Id)
        {
            var data = (from hd in db.HOADONs
                        where hd.MaHoaDon.Equals(Id)
                        select hd).Single();
            if (data != null)
            {
                data.TrangThai = 1;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Phương thức xóa một hóa đơn
        /// </summary>
        /// <param name="Id">Mã hóa đơn cần xóa</param>
        public static void delete(int Id)
        {
            var data = (from hd in db.HOADONs
                        where hd.MaHoaDon.Equals(Id)
                        select hd).Single();
            if (data != null)
            {
                //xóa tất cả các cthd liên quan
                var cthd = (from ct in db.CTHDs
                            where ct.MaHoaDon.Equals(Id)
                            select ct);
                foreach (var ct in cthd)
                {
                    DeleteDetailBill(ct.MaChiTiet);
                }
                // xóa chi tiết hóa đơn
                db.HOADONs.Remove(data);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Phương thức lấy danh sách hóa đơn
        /// </summary>
        /// <param name="min">chỉ số trang trong bảng phân trang</param>
        /// <param name="number">số bảng ghi trong 1 trang</param>
        /// <returns>Danh sách hóa đơn</returns>
        public static IEnumerable<HOADON> LoadBill()
        {
            IEnumerable<HOADON> data;
            data = (from hd in db.HOADONs
                    where hd.TrangThai == 0
                    orderby hd.NgayLap
                    select hd);
            return data;
        }

        public static int LoadIdBill()
        {
            var data = (from hd in db.HOADONs
                    orderby hd.MaHoaDon
                    select hd.MaHoaDon).Single();
            return data;
        }
        #endregion
        #region Các phương thức cho CTHD

        /// <summary>
        /// Phương thức thêm một CHi tiết hóa đơn
        /// </summary>
        /// <param name="ct">chi tiết hóa đơn cần thêm</param>
        public static void AddDetailBill(CTHD ct)
        {
            db.CTHDs.Add(ct);
            db.SaveChanges();
        }

        /// <summary>
        /// Cập nhật một chi tiết hóa đơn
        /// </summary>
        /// <param name="ct">Chi tiết hóa đơn cần cập nhật</param>
        public static void UpdateDetailBill(CTHD ct)
        {
            var data = (from cthd in db.CTHDs
                        where cthd.MaChiTiet == ct.MaChiTiet
                        select cthd).Single();
            if (data != null)
            {
                data.MaHoaDon = ct.MaHoaDon;
                data.MaSanPham = ct.MaSanPham;
                data.SoLuong = ct.SoLuong;
                data.ThanhTien = ct.ThanhTien;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Xóa một chi tiết hóa đơn
        /// </summary>
        /// <param name="id">Mã chi tiết hóa đơn cần xóa</param>
        public static void DeleteDetailBill(int id)
        {
            var data = (from cthd in db.CTHDs
                        where cthd.MaChiTiet == id
                        select cthd).Single();
            if (data != null)
            {
                db.CTHDs.Remove(data);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Phương thức lấy danh sách chi tiết hóa đơn theo mã hóa đơn
        /// </summary>
        /// <param name="Id">Mã hóa đơn</param>
        /// <returns>danh sách chi tiết hóa đơn</returns>
        public static IEnumerable<CTHD> LoadDetailBill(int Id)
        {
            var data = (from cthd in db.CTHDs
                        where cthd.MaHoaDon == Id
                        select cthd);
            return data;
        }
        #endregion
        #region Các phương thức cho PHIEUNHAP

        /// <summary>
        /// Phương thức thêm một phiếu nhập
        /// </summary>
        /// <param name="phieunhap">phiếu nhập cần thêm</param>
        public static void AddImport(PHIEUNHAP phieunhap)
        {
            db.PHIEUNHAPs.Add(phieunhap);
            db.SaveChanges();
        }

        /// <summary>
        /// Phương thức cập nhập phiếu nhập
        /// </summary>
        /// <param name="phieunhap">Phiếu nhập cần cập nhập</param>
        public static void UpdateImport(PHIEUNHAP phieunhap)
        {
            PHIEUNHAP data = (from pn in db.PHIEUNHAPs
                              where pn.MaPhieuNhap == phieunhap.MaPhieuNhap
                              select pn).Single();
            if (data != null)
            {
                data.NgayNhap = phieunhap.NgayNhap;
                data.NhaSanXuat = phieunhap.NhaSanXuat;
                data.TongTien = phieunhap.TongTien;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Phương thức xoa một phiếu nhập
        /// </summary>
        /// <param name="id">id phiếu nhập cần xóa</param>
        public static void DeleteImport(int id)
        {
            PHIEUNHAP data = (from pn in db.PHIEUNHAPs
                              where pn.MaPhieuNhap == id
                              select pn).Single();
            if (data != null)
            {
                db.PHIEUNHAPs.Remove(data);
                db.SaveChanges();
            }
        }

        #endregion
        #region Các phương thức cho CT_PHIEUNHAP

        /// <summary>
        /// Phương thức thêm một chi tiết phiếu nhập
        /// </summary>
        /// <param name="ct_phieunhap">chi tiết phiếu nhập</param>
        public static void AddDetailImport(CT_PHIEUNHAP ct_phieunhap)
        {
            db.CT_PHIEUNHAP.Add(ct_phieunhap);
            db.SaveChanges();
        }

        /// <summary>
        /// Phương thức cập nhật một chi tiết phiếu nhập
        /// </summary>
        /// <param name="ct_phieunhap">ctpn cần cập nhật</param>
        public static void UpdateDetailImport(CT_PHIEUNHAP ct_phieunhap)
        {
            CT_PHIEUNHAP data = (from ct in db.CT_PHIEUNHAP
                                 where ct.MaChiTiet == ct_phieunhap.MaChiTiet
                                 select ct).Single();
            if (data != null)
            {
                data.MaPhieuNhap = ct_phieunhap.MaPhieuNhap;
                data.MaSanPham = ct_phieunhap.MaSanPham;
                data.SoLuong = ct_phieunhap.SoLuong;
                data.ThanhTien = ct_phieunhap.ThanhTien;
                data.DonGia = ct_phieunhap.DonGia;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Phương thức xóa một chi tiết phiếu nhập
        /// </summary>
        /// <param name="Id">Mã chi tiết phiếu nhập</param>
        public static void DeleteDetailImport(int Id)
        {
            CT_PHIEUNHAP data = (from ct in db.CT_PHIEUNHAP
                                 where ct.MaChiTiet == Id
                                 select ct).Single();
            if (data != null)
            {
                db.CT_PHIEUNHAP.Remove(data);
                db.SaveChanges();
            }
        }
        #endregion
        #region Các phuong thuc cho LIENHE
        public static void AddContact(LIENHE lh)
        {
            db.LIENHEs.Add(lh);
            db.SaveChanges();
        }
        public static IEnumerable<LIENHE> LoadContact()
        {
            var data = (from lh in db.LIENHEs
                        orderby lh.Id
                        select lh);
            return data;
        }

        #endregion
    }
}