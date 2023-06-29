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
				//StartTime = dto.StartTime,
				//EndTime = dto.EndTime,
				Status = dto.Status,
				Tag = dto.Tag,
				ProductGroupList = dto.ProductGroups
			};
		}

		public static ProductDto ToIndexDto(this Product entity)
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
				//StartTime = entity.StartTime,
				//EndTime = entity.EndTime,
				Status=entity.Status,
				LogOut = entity.LogOut,
				Tag = entity.Tag,
				//fk_ProductSubCategoryId = entity.fk_ProductSubCategoryId,
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


		public static ProductDto ToDto(this ProductIndexVM vm)
		{
			return new ProductDto
			{
				ProductId = vm.ProductId,
				Status = vm.Status,
			};
		}


		public static ProductDto ToCreateDto(this ProductCreateVM vm)
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
				//StartTime = vm.StartTime,
				//EndTime = vm.EndTime,
				Status=vm.Status,
				Tag = vm.Tag,
				fk_ProductSubCategoryId = vm.fk_ProductSubCategoryId,
				ImgPaths = vm.ImgPaths,
				ProductGroups = vm.ProductGroups,
				CreateTime=DateTime.Now,
				EditTime=DateTime.Now,
			};
		}

		public static Product ToCreateEntity(this ProductDto dto)
		{
			return new Product
			{
				ProductId = dto.ProductId,
				ProductName = dto.ProductName,
				ProductDescription = dto.ProductDescription,
				ProductMaterial = dto.ProductMaterial,
				ProductOrigin = dto.ProductOrigin,
				UnitPrice = dto.UnitPrice,
				SalesPrice = dto.SalesPrice,
				//StartTime = dto.StartTime,
				//EndTime = dto.EndTime,
				Status= dto.Status,
				LogOut = dto.LogOut,
				Tag = dto.Tag,
				fk_ProductSubCategoryId = dto.fk_ProductSubCategoryId,
				CreateTime = dto.CreateTime,
				EditTime = dto.EditTime,
				ProductImgs = dto.ImgPaths.Select(p => new ProductImg
				{
					fk_ProductId = dto.ProductId,
					ImgPath = p
				}).ToList(),
				//ProductGroups = dto.ProductGroups.Select(p => new ProductGroup
				//{
				//	fk_ProductId = dto.ProductId,
				//	fk_ColorId = p.ColorId,
				//	fk_SizeID = p.SizeId,
				//	Qty = p.Qty
				//}).ToList()
			};
		}
	}
}
