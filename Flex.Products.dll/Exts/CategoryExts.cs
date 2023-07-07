using EFModels.EFModels;
using Flex.Products.dll.Models.Dtos;
using Flex.Products.dll.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flex.Products.dll.Exts
{
	public static class CategoryExts
	{
		public static SalesCategoryIndexVM ToIndexVM(this SalesCategoryDto dto)
		{
			return new SalesCategoryIndexVM
			{
				SalesCategoryId = dto.SalesCategoryId,
				SalesCategoryName = dto.SalesCategoryName,
			};
		}
		
		public static SalesCategoryDto ToCreateDto(this SalesCategoryCreateVM vm)
		{
			return new SalesCategoryDto
			{
				SalesCategoryId = vm.SalesCategoryId,
				SalesCategoryName = vm.SalesCategoryName,
			};
		}

		public static SalesCategoryEditVM ToEditVM(this SalesCategoryDto dto)
		{
			return new SalesCategoryEditVM
			{
				SalesCategoryId = dto.SalesCategoryId,
				SalesCategoryName = dto.SalesCategoryName,
			};
		}

		public static SalesCategoryDto ToEditDto(this SalesCategoryEditVM vm)
		{
			return new SalesCategoryDto
			{
				SalesCategoryId = vm.SalesCategoryId,
				SalesCategoryName = vm.SalesCategoryName,
			};
		}


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

		public static ColorDto ToDto(this ColorCategory entity)
		{
			return new ColorDto
			{
				ColorId = entity.ColorId,
				ColorName = entity.ColorName,
			};
		}

		public static SizeDto ToDto(this SizeCategory entity)
		{
			return new SizeDto
			{
				SizeId = entity.SizeId,
				SizeName = entity.SizeName,
			};
		}
	}
}
