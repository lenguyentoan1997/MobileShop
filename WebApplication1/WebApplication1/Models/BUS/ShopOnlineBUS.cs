using ShopOnlineConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace WebApplication1.Models.BUS
{
    public class ShopOnlineBUS
    {
        //------------------Guest--------------------
        public static IEnumerable<SanPham> DanhSach()
        {
            var db = new ShopOnlineConnectionDB();
            return db.Query<SanPham>("select * from SanPham where TinhTrang = 0");

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
        public static void InsertSp(SanPham sp)
        {
            var db = new ShopOnlineConnectionDB();
            db.Insert(sp);
        }
        public static void UpdateSp(String id,SanPham sp)
        {
            var db = new ShopOnlineConnectionDB();
            db.Update(sp,id);
        }
        public static SanPham DeleteSp(String id)
        {
            var db = new ShopOnlineConnectionDB();
            return db.Single<SanPham>("DELETE FROM SanPham WHERE MaSanPham = '" + id + "'");
        }
        //----------------update images
        public static void UpdateImages(string id,string images)
        {
            var db = new ShopOnlineConnectionDB();
            var sp = ShopOnlineBUS.ChiTiet(id);
            sp.HinhChinh = images;
            db.Update(sp, id);
        }
        //------Loai anh dai dien cho hinh anh------------
        //public static string LoadAvartaImg(string id)
        //{
        //    var sp = ChiTiet(id);
        //    var product = ShopOnlineBUS.ChiTiet(id);
        //    var images = product.HinhChinh;
        //    XElement xImages = XElement.Parse(images);
        //    List<string> listImageReturn = new List<string>();
        //    foreach(XElement element in xImages.Elements())
        //    {
        //        listImageReturn.Add(element.Value);
        //    }
        //    if (listImageReturn.Count() == 0)
        //    {
        //        return "~/Asset/images/default.png";
        //    }
        //    return listImageReturn.ElementAt(0).ToString();
        //}
    }
}