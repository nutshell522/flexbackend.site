using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flex.Products.dll.Models.Dtos
{
	public class ProductSubCategoryDto
	{
		public int ProductSubCategoryId { get; set; }
		public string ProductSubCategoryName { get; set; }
		public int fk_ProductCategoryId { get; set; }
		public string SubCategoryPath { get; set; }
	}
}
