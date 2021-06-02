using ShopOnlineConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.BUS
{
    public class LoaiSanPhamBUS
    {
        //------------GUEST------------------
        public static IEnumerable<LoaiSanPham> DanhSach()
        {
            var db = new ShopOnlineConnectionDB();
            return db.Query<LoaiSanPham>("select * from LoaiSanPham where TinhTrang = 0");

        }

        public static IEnumerable<SanPham> ChiTiet(String id)
        {
            var db = new ShopOnlineConnectionDB();
            return db.Query<SanPham>("select * from SanPham where MaLoaiSanPham = '" + id + "'");

        }
        //--------------Admin---------------
        public static IEnumerable<LoaiSanPham> DanhSachAdmin()
        {
            var db = new ShopOnlineConnectionDB();
            return db.Query<LoaiSanPham>("select * from LoaiSanPham");

        }
        public static void AddLoaiSanPham(LoaiSanPham lsp)
        {
            var db = new ShopOnlineConnectionDB();
            db.Insert(lsp);
        }
        public static LoaiSanPham ChiTietLSP(String id)
        {
            var db = new ShopOnlineConnectionDB();
            return db.SingleOrDefault<LoaiSanPham>("SELECT * FROM LoaiSanPham WHERE MaLoaiSanPham = '" + id + "'");
        }
        public static LoaiSanPham UpdateLSP(String maLoaiSanPham, String tenLoaiSanPham, String tinhTrang)
        {
            var db = new ShopOnlineConnectionDB();
            return db.Single<LoaiSanPham>("UPDATE LoaiSanPham SET TenLoaiSanPham = '" + tenLoaiSanPham + "',TinhTrang = '" + tinhTrang + "' WHERE MaLoaiSanPham = '" + maLoaiSanPham + "'");

        }
        public static LoaiSanPham DeleteLSP(String maLoaiSanPham)
        {
            var db = new ShopOnlineConnectionDB();
            return db.Single<LoaiSanPham>("DELETE FROM LoaiSanPham WHERE MaLoaiSanPham = '" + maLoaiSanPham + "'");
        }
    }
}


