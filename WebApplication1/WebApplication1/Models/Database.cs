using ShopOnlineConnection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

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
	}
}