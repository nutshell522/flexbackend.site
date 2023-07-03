﻿using Customized_Shoes.dll.Models.Dtos;
using Customized_Shoes.dll.Models.ViewModels;
using EFModels.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customized_Shoes.dll.Models.Exts
{
	public static class CustomizedShoesExts
	{
		public static CustomizedShoesIndexVM ToIndexVM(this CustomizedShoesDto dto) 
		{
			return new CustomizedShoesIndexVM
			{
				ShoesProductId = dto.ShoesProductId,
				ShoesName = dto.ShoesName,
				ShoesOrigin = dto.ShoesOrigin,
				ShoesDescription = dto.ShoesDescription,
				ShoesUnitPrice = dto.ShoesUnitPrice,
				Status = dto.Status,
				fk_ShoesCategoryId = dto.fk_ShoesCategoryId,
				fk_ShoesColorId = dto.fk_ShoesColorId,
			};
					
		}

		public static CustomizedShoesDto ToIndexDto(this CustomizedShoesPo entity) 
		{
			return new CustomizedShoesDto
			{
				ShoesProductId = entity.ShoesProductId,
				ShoesName = entity.ShoesName,
				ShoesOrigin = entity.ShoesOrigin,
				ShoesDescription = entity.ShoesDescription,
				ShoesUnitPrice = entity.ShoesUnitPrice,
				Status = entity.Status,
				fk_ShoesCategoryId = entity.fk_ShoesCategoryId,
				fk_ShoesColorId = entity.fk_ShoesColorId,

			};				
		}

		public static CustomizedShoesDto ToDto(this CustomizedShoesIndexVM vm)
		{
			return new CustomizedShoesDto {
				ShoesProductId = vm.ShoesProductId,
				Status = vm.Status,
			};

		}

		public static CustomizedShoesDto ToCreateDto(this CustomizedShoesCreateVM vm) 
		{
			return new CustomizedShoesDto
			{
				ShoesProductId = vm.ShoesProductId,
				ShoesName = vm.ShoesName,
				ShoesOrigin = vm.ShoesOrigin,
				ShoesDescription = vm.ShoesDescription,
				ShoesUnitPrice = vm.ShoesUnitPrice,
				Status = vm.Status,
				fk_ShoesCategoryId = vm.fk_ShoesCategoryId,
				fk_ShoesColorId = vm.fk_ShoesColorId,
				DataCreateTime = DateTime.Now,
				DataEditTime = DateTime.Now,
			};				
		}

		public static CustomizedShoesPo ToCreateEntity(this CustomizedShoesDto dto) 
		{
			return new CustomizedShoesPo
			{
				ShoesProductId = dto.ShoesProductId,
				ShoesName = dto.ShoesName,
				ShoesOrigin = dto.ShoesOrigin,
				ShoesDescription = dto.ShoesDescription,
				ShoesUnitPrice= dto.ShoesUnitPrice,
				Status = dto.Status,
				fk_ShoesCategoryId = dto.fk_ShoesCategoryId,
				fk_ShoesColorId = dto.fk_ShoesColorId,
				ShoesPictures = dto.ImgPaths.Select(p => new ShoesPicture 
				{ 
					fk_ShoesPictureProduct_Id = dto.ShoesProductId,
					ShoesPictureUrl = p				
				}).ToList(),
			};
		}
	}
}