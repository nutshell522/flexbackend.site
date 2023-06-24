using EFModels.EFModels;
using Flex.Products.dll.Models.Dtos;
using Flex.Products.dll.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Flex.Products.dll.Models.Infra.Exts
{
	public static class ProductExts
	{

		public static ProductIndexVM ToIndexVM(this ProductDto dto)
		{
			return new ProductIndexVM
			{
				ProductId = dto.ProductId,
				ProductName = dto.ProductName,
				SubCategoryPath = dto.ProductSubCategory.SubCategoryPath,
				SalesPrice = dto.SalesPrice,
				StartTime = dto.StartTime,
				EndTime = dto.EndTime,
				Tag = dto.Tag,
				ProductGroupList = dto.ProductGroups//.Select(x=> new ProductGroupClass { ColorName = x.ColorCategory.ColorName,SizeName=x.SizeCategory.SizeName,Qty=x.Qty }).ToList()
			};
		}

		public static ProductDto ToDto(this Product entity)
		{
			return new ProductDto
			{
				ProductId = entity.ProductId,
				ProductName = entity.ProductName,
				ProductDescription = entity.ProductDescription,
				ProductMaterial = entity.ProductMaterial,
				ProductOrigin = entity.ProductOrigin,
				UnitPrice = entity.UnitPrice,
				SalesPrice = entity.SalesPrice,
				StartTime = entity.StartTime,
				EndTime = entity.EndTime,
				LogOut = entity.LogOut,
				Tag = entity.Tag,
				fk_ProductSubCategoryId = entity.fk_ProductSubCategoryId,
				ProductSubCategory = entity.ProductSubCategory,
				ProductGroups = entity.ProductGroups.Select(x => new ProductGroupClass
				{
					ColorId = x.ColorCategory.ColorId,
					ColorName = x.ColorCategory.ColorName,
					SizeId = x.SizeCategory.SizeId,
					SizeName = x.SizeCategory.SizeName,
					Qty = x.Qty
				}).ToList()
			};
		}

		public static ProductDto ToDto(this ProductCreateVM vm)
		{
			return new ProductDto
			{
				ProductId = vm.ProductId,
				ProductName = vm.ProductName,
				ProductDescription = vm.ProductDescription,
				ProductMaterial = vm.ProductMaterial,
				ProductOrigin = vm.ProductOrigin,
				UnitPrice = vm.UnitPrice,
				SalesPrice = vm.SalesPrice,
				StartTime = vm.StartTime,
				EndTime = vm.EndTime,
				Tag = vm.Tag,
				fk_ProductSubCategoryId = vm.fk_ProductSubCategoryId,
				ImgPaths = vm.ImgPaths,
				//ProductGroups = vm.ProductGroups,
			};
		}

		public static Product DtoToEntity(this ProductDto dto)
		{ 
			return  new Product
			{
				ProductId = dto.ProductId,
				ProductName = dto.ProductName,
				ProductDescription = dto.ProductDescription,
				ProductMaterial = dto.ProductMaterial,
				ProductOrigin = dto.ProductOrigin,
				UnitPrice = dto.UnitPrice,
				SalesPrice = dto.SalesPrice,
				StartTime = dto.StartTime,
				EndTime = dto.EndTime,
				LogOut = dto.LogOut,
				Tag = dto.Tag,
				fk_ProductSubCategoryId = dto.fk_ProductSubCategoryId,
				CreateTime = dto.CreateTime,
				EditTime = dto.EditTime
			};
		}
	}
}
