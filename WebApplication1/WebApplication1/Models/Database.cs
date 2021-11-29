using ShopOnlineConnection;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public partial class Database
    {
        public partial class NhaSanXuatView : ShopOnlineConnectionDB.Record<NhaSanXuat>
        {
            [Column] public string MaNhaSanXuat { get; set; }
            [Column] public string TenNhaSanXuat { get; set; }
            [Column] public string TinhTrang { get; set; }
            [Column] public string TenLoaiSanPham { get; set; }
        }

        public partial class CommentInformation : ShopOnlineConnectionDB.Record<Comment>
        {
            [Column] public int Id { get; set; }
            [Column] public string ProductId { get; set; }
            [Column] public string UserEmail { get; set; }
            [Column] public string CommentContent { get; set; }
            [Column] public int Status { get; set; }
            [Column] public DateTime Date { get; set; }
            [Column] public string FullName { get; set; }
            [Column] public byte? star { get; set; }
        }

        public partial class OrderInformationByDate : ShopOnlineConnectionDB.Record<OrderDetail>
        {
            [Column] public DateTime? CreateDate { get; set; }
            [Column] public string ProductID { get; set; }
            [Column] public int OrderID { get; set; }
            [Column] public int? Quantity { get; set; }
            [Column] public decimal? Price { get; set; }
        }

        public partial class Statistics
        {
            private string _productId;
            private int _quantity;

            public Statistics(string productId, int quantity)
            {
                _productId = productId;
                _quantity = quantity;
            }
            public Statistics() { }

            public string ProductId { get => _productId; set => _productId = value; }
            public int Quantity { get => _quantity; set => _quantity = value; }
        }
    }
}