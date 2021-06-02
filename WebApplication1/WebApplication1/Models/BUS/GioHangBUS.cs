using ShopOnlineConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.BUS
{
    public class GioHangBUS
    {
        public static void Them(string masanpham, string mataikhoan, int soluong, int gia, string tensanpham)
        {
            using (var db = new ShopOnlineConnectionDB())
            {
                var x = db.Query<GioHang>("select * from GioHang Where MaTaiKhoan ='" + mataikhoan + "' and MaSanPham = '" + masanpham + "'").ToList();
                if (x.Count() > 0)
                {
                    //goi ham update soluong
                    int a = (int)x.ElementAt(0).SoLuong + soluong;
                    CapNhat(masanpham, mataikhoan, a, gia, tensanpham);
                }
                else
                {
                    GioHang giohang = new GioHang()
                    {
                        MaSanPham = masanpham,
                        MaTaiKhoan = mataikhoan,
                        SoLuong = soluong,
                        Gia = gia,
                        TenSanPham = tensanpham,
                        TongTien = gia * soluong
                    };
                    db.Insert(giohang);
                }
            }
        }
        public static IEnumerable<GioHang> DanhSach(string mataikhoan)
        {
            using (var db = new ShopOnlineConnectionDB())
            {
                return db.Query<GioHang>("select * from GioHang where MaTaiKhoan = '" + mataikhoan + "'");
            }
        }
        public static void CapNhat(string masanpham, string mataikhoan, int soluong, int gia, string tensanpham)
        {
            using (var db = new ShopOnlineConnectionDB())
            {
                GioHang giohang = new GioHang()
                {
                    MaSanPham = masanpham,
                    MaTaiKhoan = mataikhoan,
                    SoLuong = soluong,
                    Gia = gia,
                    TenSanPham = tensanpham,
                    TongTien = gia * soluong
                };
                var sanPham = db.Query<GioHang>("Select IdGH from GioHang where MaTaiKhoan = '" + mataikhoan + "' and MaSanPham ='" + masanpham + "'").FirstOrDefault();
                db.Update(giohang, sanPham.IdGH);
            }
        }
        public static void Xoa(string masanpham, string mataikhoan)
        {
            using (var db = new ShopOnlineConnectionDB())
            {
                var sanPham = db.Query<GioHang>("SELECT * FROM GioHang WHERE MaSanPham ='" + masanpham + "' AND MaTaiKhoan ='" + mataikhoan + "'").FirstOrDefault();
                db.Delete(sanPham);
            }
        }

        public static int TongTien(string mataikhoan)
        {
            using (var db = new ShopOnlineConnectionDB())
            {
                List<GioHang> gioHang = DanhSach(mataikhoan).ToList();
                if (gioHang.Count() == 0)
                {
                    return 0;
                }
                return db.Query<int>("select sum(TongTIen) from GioHang where MaTaiKhoan = '" + mataikhoan + "'").FirstOrDefault();
            }
        }

    }
}