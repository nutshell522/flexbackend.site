using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.ViewModels
{
	public class OrderItemsVM
	{
		public int Id { get; set; }

		public int order_Id { get; set; }

		[StringLength(50)]
		public string product_name { get; set; }

		public int fk_typeId { get; set; }

		public int? per_price { get; set; }

		public int? quantity { get; set; }

		[StringLength(50)]
		public string discount_name { get; set; }

		public int? subtotal { get; set; }

		public int? discount_subtotal { get; set; }

		[StringLength(50)]
		public string Items_description { get; set; }
	}
}
