using ShopOnlineConnection;
using System;
using System.Collections.Generic;
using static WebApplication1.Models.Database;

namespace WebApplication1.Models.BUS
{
    public class NhaSanXuatModel
    {
        private static readonly Lazy<NhaSanXuatModel> lazy = new Lazy<NhaSanXuatModel>(() => new NhaSanXuatModel());
        public static NhaSanXuatModel Instance
        {
            get => lazy.Value;
        }
        private NhaSanXuatModel() { }

        private static ShopOnlineConnectionDB DatabaseConnection()
        {
            var database = new ShopOnlineConnectionDB();

            return database;
        }

        //------------Guest---------------------------
        public IEnumerable<NhaSanXuat> DanhSach()
        {
            return DatabaseConnection().Query<NhaSanXuat>("SELECT * FROM vw_GetNhaSanXuatByStatus");
        }
        public IEnumerable<NhaSanXuat> ListPhone()
        {
            string typeProduct = "phone";

            return DatabaseConnection().Query<NhaSanXuat>("SELECT * FROM udf_GetNhaSanXuatByTenLoaiSanPham(@0)", typeProduct);
        }
        public IEnumerable<NhaSanXuat> ListLapTop()
        {
            string typeProduct = "LapTop";

            return DatabaseConnection().Query<NhaSanXuat>("SELECT * FROM udf_GetNhaSanXuatByTenLoaiSanPham(@0)", typeProduct);
        }
        public IEnumerable<NhaSanXuatView> DanhSachAdmin()
        {
            return DatabaseConnection().Query<NhaSanXuatView>("SELECT * FROM vw_NhaSanXuat");
        }
        public IEnumerable<SanPham> ChiTiet(string id)
        {
            return DatabaseConnection().Query<SanPham>("SELECT * FROM udf_GetSanPhamByMaNhaSanXuat(@0)", id);
        }
        //-------------Admin--------------------
        public void ThemNSX(NhaSanXuat nsx)
        {
            DatabaseConnection().Insert(nsx);
        }

        public NhaSanXuatView ChiTietAdmin(string id)
        {
            return DatabaseConnection().SingleOrDefault<NhaSanXuatView>("SELECT * FROM udf_GetNhaSanXuatByMaNhaSanXuat(@0)", id);

        }

        public void UpdateNSX(NhaSanXuat nhaSanXuat, string maNhaSanXuat)
        {
            DatabaseConnection().Update(nhaSanXuat, maNhaSanXuat);
        }

        public void DeleteNSX(NhaSanXuat nhaSanXuat)
        {
            DatabaseConnection().Delete(nhaSanXuat);
        }

    }
}