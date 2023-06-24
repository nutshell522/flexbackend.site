using EFModels.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flex.Products.dll.Models.Infra.EFRepository
{
	public class ProductSubCategoryRepository
	{
		public List<ProductSubCategory> GetProductSubCategory()
		{
			return new AppDbContext().ProductSubCategories
				.OrderBy(c=>c.ProductSubCategoryId)
				.ToList();
		}
	}
}
