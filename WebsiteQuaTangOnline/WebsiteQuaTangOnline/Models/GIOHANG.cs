using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteQuaTangOnline.Models;

namespace WebsiteQuaTangOnline.Models
{
    public class GIOHANG
    {
        public List<SANPHAM> ListProduct {get; set;}
        
        public GIOHANG()
        {
            ListProduct = new List<SANPHAM>();
        }
        /// <summary>
        /// Phương thức thêm một sản phẩm mới vào giỏ
        /// </summary>
        /// <param name="sanpham"></param>
        public void Add(SANPHAM sanpham)
        {
            if (sanpham.SoLuong <= 0)
                return;
            bool check=true;
            foreach(SANPHAM item in ListProduct)
            {
                if(item.MaSanPham==sanpham.MaSanPham)
                {
                    check = false;
                    item.SoLuong += sanpham.SoLuong;
                    break;
                }
            }
            if(check)
            {
                ListProduct.Add(sanpham);
            }
        }

        /// <summary>
        /// Phương thức xóa 1 sản phẩm ra khỏi danh sách
        /// </summary>
        /// <param name="masp">Mã sản phẩm cần xóa</param>
        public void Delete(string masp)
        {
            for(int i=0;i<ListProduct.Count;i++)
            {
                if(ListProduct[i].MaSanPham==masp)
                {
                    ListProduct.RemoveAt(i);
                    break;
                }
            }
        }

        /// <summary>
        /// Phương thức cập nhật số lượng sản phẩm
        /// </summary>
        /// <param name="sanpham">sản phẩm cần cập nhật</param>
        public void Update(string id,int sl)
        {
            if (sl <= 0)
                return;
            foreach (SANPHAM item in ListProduct)
            {
                if (item.MaSanPham == id)
                {
                    item.SoLuong = sl;
                    break;
                }
            }
        }

        public int Money(int index)
        {
            return ListProduct[index].SoLuong * ListProduct[index].DonGia;
        }

        public int TotalMoney()
        {
            int tongtien = 0;
            foreach(SANPHAM item in ListProduct)
            {
                tongtien += (item.SoLuong * item.DonGia);
            }
            return tongtien;
        }

    }
}