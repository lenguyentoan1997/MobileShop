using ShopOnlineConnection;
using System;
using System.Collections.Generic;

namespace WebApplication1.Models.BUS
{
    public class NhaSanXuatBUS
    {
        private static ShopOnlineConnectionDB DatabaseConnection()
        {
            var database = new ShopOnlineConnectionDB();

            return database;
        }

        //------------Guest---------------------------
        public static IEnumerable<NhaSanXuat> DanhSach()
        {
            return DatabaseConnection().Query<NhaSanXuat>("SELECT * FROM vw_GetNhaSanXuatByStatus");
        }
        public static IEnumerable<NhaSanXuat> ListPhone()
        {
            string typeProduct = "phone";

            return DatabaseConnection().Query<NhaSanXuat>("SELECT * FROM udf_GetNhaSanXuatByTenLoaiSanPham(@0)", typeProduct);
        }
        public static IEnumerable<NhaSanXuat> ListLapTop()
        {
            string typeProduct = "LapTop";

            return DatabaseConnection().Query<NhaSanXuat>("SELECT * FROM udf_GetNhaSanXuatByTenLoaiSanPham(@0)", typeProduct);
        }
        public static IEnumerable<NhaSanXuat> DanhSachAdmin()
        {
            return DatabaseConnection().Query<NhaSanXuat>("SELECT * FROM vw_NhaSanXuat");
        }
        public static IEnumerable<SanPham> ChiTiet(string id)
        {
            return DatabaseConnection().Query<SanPham>("SELECT * FROM udf_GetSanPhamByMaNhaSanXuat(@0)", id);
        }
        //-------------Admin--------------------
        public static void ThemNSX(NhaSanXuat nsx)
        {
            DatabaseConnection().Insert(nsx);
        }

        public static NhaSanXuat ChiTietAdmin(string id)
        {
            return DatabaseConnection().SingleOrDefault<NhaSanXuat>("SELECT * FROM udf_GetNhaSanXuatByMaNhaSanXuat(@0)", id);

        }

        public static void UpdateNSX(NhaSanXuat nhaSanXuat, string maNhaSanXuat)
        {
            DatabaseConnection().Update(nhaSanXuat, maNhaSanXuat);
        }

        public static void DeleteNSX(NhaSanXuat nhaSanXuat)
        {
            DatabaseConnection().Delete(nhaSanXuat);
        }

    }
}