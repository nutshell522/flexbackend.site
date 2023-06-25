using EFModels.EFModels;
using Flex.Products.dll.Exts;
using Flex.Products.dll.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flex.Products.dll.Models.Infra.EFRepository
{
	public class ProductSubCategoryRepository
	{
		public List<ProductSubCategoryDto> GetProductSubCategory()
		{
			return new AppDbContext().ProductSubCategories
				.OrderBy(c=>c.ProductSubCategoryId)
				.ToList()
				.Select(c=>c.ToDto())
				.ToList();
		}
	}
}
