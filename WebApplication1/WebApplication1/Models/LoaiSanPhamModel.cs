using ShopOnlineConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.BUS
{
    public class LoaiSanPhamModel
    {
        private static readonly Lazy<LoaiSanPhamModel> lazy = new Lazy<LoaiSanPhamModel>(() => new LoaiSanPhamModel());
        public static LoaiSanPhamModel Instance
        {
            get => lazy.Value;
        }
        private LoaiSanPhamModel() { }

        //------------GUEST------------------
        public IEnumerable<LoaiSanPham> DanhSach()
        {
            var db = new ShopOnlineConnectionDB();
            string status = "0";

            return db.Query<LoaiSanPham>("SELECT * FROM udf_GetLoaiSanPhamByStatus(@0)", status);

        }

        public IEnumerable<SanPham> ChiTiet(string id)
        {
            var db = new ShopOnlineConnectionDB();
            return db.Query<SanPham>("SELECT * FROM udf_GetSanPhamByMaLoaiSanPham(@0)", id);

        }
        //--------------Admin---------------
        public IEnumerable<LoaiSanPham> DanhSachAdmin()
        {
            var db = new ShopOnlineConnectionDB();
            return db.Query<LoaiSanPham>("SELECT * FROM vw_LoaiSanPham");

        }

        public void AddLoaiSanPham(LoaiSanPham lsp)
        {
            var db = new ShopOnlineConnectionDB();
            db.Insert(lsp);
        }

        public LoaiSanPham ChiTietLSP(String id)
        {
            var db = new ShopOnlineConnectionDB();

            return db.SingleOrDefault<LoaiSanPham>("SELECT * FROM udf_GetLoaiSanPhamByMaLoaiSanPham(@0)", id);
        }

        public void UpdateLSP(LoaiSanPham loaiSanPham, string maLoaiSanPham)
        {
            var db = new ShopOnlineConnectionDB();

            db.Update(loaiSanPham, maLoaiSanPham);
        }
        public void DeleteLSP(LoaiSanPham loaiSanPham)
        {
            var db = new ShopOnlineConnectionDB();

            db.Delete(loaiSanPham);
        }
    }
}


