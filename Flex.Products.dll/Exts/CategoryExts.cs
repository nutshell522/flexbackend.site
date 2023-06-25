using EFModels.EFModels;
using Flex.Products.dll.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flex.Products.dll.Exts
{
	public static class CategoryExts
	{
		public static ProductSubCategoryDto ToDto(this ProductSubCategory entity)
		{
			return new ProductSubCategoryDto
			{
				ProductSubCategoryId = entity.ProductSubCategoryId,
				ProductSubCategoryName = entity.ProductSubCategoryName,
				fk_ProductCategoryId = entity.fk_ProductCategoryId,
				SubCategoryPath = entity.SubCategoryPath
			};
		}
	}
}
