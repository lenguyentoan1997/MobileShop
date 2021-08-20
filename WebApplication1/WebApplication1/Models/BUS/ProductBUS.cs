using ShopOnlineConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace WebApplication1.Models.BUS
{
    public class ProductBUS
    {
        private static ShopOnlineConnectionDB DatabaseConnection()
        {
            var database = new ShopOnlineConnectionDB();

            return database;
        }

        //------------------Guest--------------------
        public static IEnumerable<SanPham> DanhSach()
        {
            return DatabaseConnection().Query<SanPham>("SELECT * FROM vw_GetSanPhamByStatus");
        }

        public static IEnumerable<SanPham> AllPhone()
        {
            string phone = "LSP01";
            return DatabaseConnection().Query<SanPham>("SELECT * FROM udf_GetSanPhamByMaLoaiSanPham(@0)", phone);
        }
        public static IEnumerable<SanPham> AllLaptop()
        {
            string laptop = "LSP02";
            return DatabaseConnection().Query<SanPham>("SELECT * FROM udf_GetSanPhamByMaLoaiSanPham(@0)", laptop);
        }

        public static SanPham ChiTiet(string id)
        {
            return DatabaseConnection().SingleOrDefault<SanPham>("SELECT * FROM udf_GetSanPhamByMaSanPham(@0)", id);
        }

        public static IEnumerable<SanPham> Top4New()
        {
            return DatabaseConnection().Query<SanPham>("SELECT * FROM vw_GetTop4ProductByNew");
        }

        public static IEnumerable<SanPham> TopHot()
        {
            return DatabaseConnection().Query<SanPham>("SELECT * FROM vw_GetTop4ProductByLuotView");
        }
        //---------------Admin----------------------
        public static IEnumerable<SanPham> DanhSachSP()
        {
            var db = new ShopOnlineConnectionDB();
            return db.Query<SanPham>("SELECT * FROM vw_GetProduct");
        }

        public static IEnumerable<SanPham> SimilarProducts(string producerCode, int price)
        {
            //create function udf_SimilarProducts(@producer_code nvarchar(10), @price int)
            //returns table
            //as
            //return (
            //select top 5 * from SanPham where MaNhaSanXuat = @producer_code and gia <= (@price + 2000000)
            //)
            //GO

            //select* from udf_SimilarProducts('NSX01', 30000000)

            var db = new ShopOnlineConnectionDB();
            return db.Query<SanPham>("SELECT * FROM udf_SimilarProducts(@0,@1)", producerCode, price);
        }

        public static void InsertSp(SanPham sp)
        {
            var db = new ShopOnlineConnectionDB();
            db.Insert(sp);
        }
        public static void UpdateSp(String id, SanPham sp)
        {
            var db = new ShopOnlineConnectionDB();
            db.Update(sp, id);
        }
        public static void DeleteSp(SanPham sanPham)
        {
            DatabaseConnection().Delete(sanPham);
        }
        //----------------update images
        public static void UpdateImages(string id, string images)
        {
            var db = new ShopOnlineConnectionDB();
            var sp = ProductBUS.ChiTiet(id);
            sp.HinhChinh = images;
            db.Update(sp, id);
        }
    }
}