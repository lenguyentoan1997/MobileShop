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
        //------------------Guest--------------------
        public static IEnumerable<SanPham> DanhSach()
        {
            var db = new ShopOnlineConnectionDB();
            return db.Query<SanPham>("select * from SanPham where TinhTrang = 0");

        }

        public static IEnumerable<SanPham> AllPhone()
        {
            var db = new ShopOnlineConnectionDB();
            return db.Query<SanPham>("SELECT * FROM SanPham WHERE TinhTrang = 0 AND MaLoaiSanPham ='LSP01'");

        }
        public static IEnumerable<SanPham> AllLaptop()
        {
            var db = new ShopOnlineConnectionDB();
            return db.Query<SanPham>("SELECT * FROM SanPham WHERE TinhTrang = 0 AND MaLoaiSanPham ='LSP02'");
        }
        public static SanPham ChiTiet(String id)
        {
            var db = new ShopOnlineConnectionDB();
            return db.SingleOrDefault<SanPham>("select * from SanPham where MaSanPham = @0", id);
        }

        public static IEnumerable<SanPham> Top4New()
        {
            var db = new ShopOnlineConnectionDB();
            return db.Query<SanPham>("select Top 4 * from SanPham Where GhiChu = N'New'");
        }

        public static IEnumerable<SanPham> TopHot()
        {
            var db = new ShopOnlineConnectionDB();
            return db.Query<SanPham>("select Top 4 * from SanPham Where LuotView >0");
        }
        //---------------Admin----------------------
        public static IEnumerable<SanPham> DanhSachSP()
        {
            var db = new ShopOnlineConnectionDB();
            return db.Query<SanPham>("SELECT * FROM SanPham");
        }

        public static IEnumerable<SanPham> SimilarProducts(string producer)
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
            return db.Query<SanPham>("SELECT * FROM SanPham");
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
        public static SanPham DeleteSp(String id)
        {
            var db = new ShopOnlineConnectionDB();
            return db.Single<SanPham>("DELETE FROM SanPham WHERE MaSanPham = '" + id + "'");
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