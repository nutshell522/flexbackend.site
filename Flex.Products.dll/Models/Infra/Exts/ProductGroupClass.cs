using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flex.Products.dll.Models.Infra.Exts
{
	public class ProductGroupClass
	{
		public int ColorId { get; set; }
		public int SizeId { get; set; }
		public int Qty { get; set; }
		public ProductGroupClass(int colorId, int sizeId, int qty)
		{
			this.ColorId = colorId;
			this.SizeId = sizeId;
			this.Qty = qty;
		}
	}
}
