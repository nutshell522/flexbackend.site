using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Orders.ViewModels
{
	public class OrderItemsVM
	{
		[Display(Name = "明細ID")]
		public int Id { get; set; }
		[Display(Name = "訂單ID")]
		public int order_Id { get; set; }
		[Display(Name = "名稱")]
		[StringLength(50)]
		public string product_name { get; set; }
		[Display(Name = "類別")]
		public int fk_typeId { get; set; }
		[Display(Name = "單價")]
		public int? per_price { get; set; }
		[Display(Name = "數量")]
		public int? quantity { get; set; }
		[Display(Name = "折扣名稱")]
		[StringLength(50)]
		public string discount_name { get; set; }
		[Display(Name = "小計")]
		public int? subtotal { get; set; }
		[Display(Name = "折扣後金額")]
		public int? discount_subtotal { get; set; }
		[Display(Name = "描述")]
		[StringLength(50)]
		public string Items_description { get; set; }
	}
}
