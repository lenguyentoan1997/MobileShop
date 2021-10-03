using ShopOnlineConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace WebApplication1.Models.BUS
{
    public class ProductModel
    {
        private static readonly Lazy<ProductModel> lazy = new Lazy<ProductModel>(() => new ProductModel());
        public static ProductModel Instance
        {
            get => lazy.Value;
        }
        private ProductModel() { }

        private static ShopOnlineConnectionDB DatabaseConnection()
        {
            var database = new ShopOnlineConnectionDB();

            return database;
        }

        //------------------Guest--------------------
        public IEnumerable<SanPham> DanhSach() => DatabaseConnection().Query<SanPham>("SELECT * FROM vw_GetSanPhamByStatus");

        public IEnumerable<SanPham> AllPhone()
        {
            string phone = "LSP01";
            return DatabaseConnection().Query<SanPham>("SELECT * FROM udf_GetSanPhamByMaLoaiSanPham(@0)", phone);
        }
        public IEnumerable<SanPham> AllLaptop()
        {
            string laptop = "LSP02";
            return DatabaseConnection().Query<SanPham>("SELECT * FROM udf_GetSanPhamByMaLoaiSanPham(@0)", laptop);
        }

        public SanPham ChiTiet(string id) => DatabaseConnection().SingleOrDefault<SanPham>("SELECT * FROM udf_GetSanPhamByMaSanPham(@0)", id);

        public void UpdateProductViews(string id)
        {
            DatabaseConnection().Execute(PetaPoco.Sql.Builder.Append("EXEC sp_UpdateProductViews @@MaSanPham = @0", id));
        }
        public void UpdateProductLike(string id)
        {
            DatabaseConnection().Execute(PetaPoco.Sql.Builder.Append("EXEC sp_UpdateProductLike @@MaSanPham = @0", id));
        }
        public IEnumerable<SanPham> Top4New()
        {
            return DatabaseConnection().Query<SanPham>("SELECT * FROM vw_GetTop4ProductByNew");
        }

        public IEnumerable<SanPham> TopHot()
        {
            return DatabaseConnection().Query<SanPham>("SELECT * FROM vw_GetTop4ProductByLuotView");
        }
        //---------------Admin----------------------
        public IEnumerable<SanPham> DanhSachSP()
        {
            var db = new ShopOnlineConnectionDB();
            return db.Query<SanPham>("SELECT * FROM vw_GetProduct");
        }

        public IEnumerable<SanPham> SimilarProducts(string producerCode, int price)
        {
            var db = new ShopOnlineConnectionDB();
            return db.Query<SanPham>("SELECT * FROM udf_SimilarProducts(@0,@1)", producerCode, price);
        }

        public void InsertSp(SanPham sp)
        {
            var db = new ShopOnlineConnectionDB();
            db.Insert(sp);
        }
        public void UpdateSp(SanPham product)
        {
            var db = new ShopOnlineConnectionDB();
            db.Update(product, product.MaSanPham);
        }
        public void DeleteSp(SanPham sanPham)
        {
            DatabaseConnection().Delete(sanPham);
        }

        public void UpdateProductAveragePoint(int averagePoint, string productId)
        {
            DatabaseConnection().Execute(PetaPoco.Sql.Builder.Append("Exec sp_UpdateProductAveragePoint @@AveragePoint = @0, @@MaSanPham = @1", averagePoint, productId));
        }

        //----------------update images
        public void UpdateImages(string id, string images)
        {
            var db = new ShopOnlineConnectionDB();
            var sp = ProductModel.Instance.ChiTiet(id);
            sp.HinhChinh = images;
            db.Update(sp, id);
        }
    }
}