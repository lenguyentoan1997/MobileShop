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
            string status = "0";

            return db.Query<LoaiSanPham>("SELECT * FROM udf_GetLoaiSanPhamByStatus(@0)", status);

        }

        public static IEnumerable<SanPham> ChiTiet(string id)
        {
            var db = new ShopOnlineConnectionDB();
            return db.Query<SanPham>("SELECT * FROM udf_GetSanPhamByMaLoaiSanPham(@0)", id);

        }
        //--------------Admin---------------
        public static IEnumerable<LoaiSanPham> DanhSachAdmin()
        {
            var db = new ShopOnlineConnectionDB();
            return db.Query<LoaiSanPham>("SELECT * FROM vw_LoaiSanPham");

        }

        public static void AddLoaiSanPham(LoaiSanPham lsp)
        {
            var db = new ShopOnlineConnectionDB();
            db.Insert(lsp);
        }

        public static LoaiSanPham ChiTietLSP(String id)
        {
            var db = new ShopOnlineConnectionDB();

            return db.SingleOrDefault<LoaiSanPham>("SELECT * FROM udf_GetLoaiSanPhamByMaLoaiSanPham(@0)", id);
        }

        public static void UpdateLSP(LoaiSanPham loaiSanPham, string maLoaiSanPham)
        {
            var db = new ShopOnlineConnectionDB();

            db.Update(loaiSanPham, maLoaiSanPham);
        }
        public static void DeleteLSP(LoaiSanPham loaiSanPham)
        {
            var db = new ShopOnlineConnectionDB();

            db.Delete(loaiSanPham);
        }
    }
}


