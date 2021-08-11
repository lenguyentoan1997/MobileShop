using ShopOnlineConnection;
using System;
using System.Collections.Generic;

namespace WebApplication1.Models.BUS
{
    public class NhaSanXuatBUS
    {

        //------------Guest---------------------------
        public static IEnumerable<NhaSanXuat> DanhSach()
        {
            var db = new ShopOnlineConnectionDB();
            return db.Query<NhaSanXuat>("SELECT * FROM NhaSanXuat WHERE TinhTrang = 0");

        }
        public static IEnumerable<NhaSanXuat> ListPhone()
        {
            var db = new ShopOnlineConnectionDB();
            return db.Query<NhaSanXuat>("SELECT * FROM NhaSanXuat WHERE TinhTrang = 0 AND LoaiSanXuat ='Phone'");

        }
        public static IEnumerable<NhaSanXuat> ListLapTop()
        {
            var db = new ShopOnlineConnectionDB();
            return db.Query<NhaSanXuat>("SELECT * FROM NhaSanXuat WHERE TinhTrang = 0 AND LoaiSanXuat ='LapTop'");

        }
        public static IEnumerable<NhaSanXuat> DanhSachAdmin()
        {
            var db = new ShopOnlineConnectionDB();
            return db.Query<NhaSanXuat>("select * from NhaSanXuat");

        }
        public static IEnumerable<SanPham> ChiTiet(String id)
        {
            var db = new ShopOnlineConnectionDB();
            return db.Query<SanPham>("select * from SanPham where MaNhaSanXuat = '" + id + "'");

        }
        //-------------Admin--------------------
        public static void ThemNSX(NhaSanXuat nsx)
        {
            var db = new ShopOnlineConnectionDB();
            db.Insert(nsx);
        }

        public static NhaSanXuat ChiTietAdmin(String id)
        {
            var db = new ShopOnlineConnectionDB();
            return db.SingleOrDefault<NhaSanXuat>("select * from NhaSanXuat where MaNhaSanXuat = '" + id + "'");

        }

        public static NhaSanXuat UpdateNSX(String maNhaSanXuat, String tenNhaSanXuat, String tinhTrang)
        {
            var db = new ShopOnlineConnectionDB();
            return db.Single<NhaSanXuat>("UPDATE NhaSanXuat set TenNhaSanXuat = '" + tenNhaSanXuat + "',TinhTrang = '" + tinhTrang + "' WHERE MaNhaSanXuat ='" + maNhaSanXuat + "'");
        }

        public static NhaSanXuat DeleteNSX(String maNhaSanXuat)
        {
            var db = new ShopOnlineConnectionDB();
            return db.Single<NhaSanXuat>("DELETE FROM NhaSanXuat WHERE MaNhaSanXuat = '" + maNhaSanXuat + "'");
        }

    }
}