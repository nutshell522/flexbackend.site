﻿using EFModels.EFModels;
using Flex.Products.dll.Models.Dtos;
using Flex.Products.dll.Models.Infra.Exts;
using Flex.Products.dll.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Flex.Products.dll.Exts;
using Flex.Products.dll.Service;
using static Flex.Products.dll.Service.ProductService;
using Flex.Products.dll.Models.ViewModel;

namespace Flex.Products.dll.Models.Infra.EFRepository
{
	public class ProductEFRepository : IProductRepository
	{
		private AppDbContext _db;
        public ProductEFRepository()
        {
			_db = new AppDbContext();
        }

		public void CreateProduct(ProductDto dto)
		{
			//將ProductDto依序轉換成Products,group,img存入database
			var product = dto.ToCreateEntity();
			 _db.Products.Add(product);

			//foreach (var img in product.ProductImgs)
			//{
			//	var productimg = new ProductImg
			//	{
			//		fk_ProductId = img.fk_ProductId,
			//		ImgPath = img.ImgPath
			//	};
			//	_db.ProductImgs.Add(productimg);
			//}

			//foreach(var group in product.ProductGroups)
			//{
			//	var productGroup = new ProductGroup
			//	{
			//		fk_ProductId = group.fk_ProductId,
			//		fk_ColorId = group.fk_ColorId,
			//		fk_SizeID = group.fk_SizeID,
			//		Qty = group.Qty,
			//	};
			//	_db.ProductGroups.Add(productGroup);
			//}

			_db.SaveChanges();
		}
		public List<ProductDto> Search(IndexSearchCriteria criteria)
		{
			criteria=criteria ?? new IndexSearchCriteria();

			var query = _db.Products.Include(p => p.ProductSubCategory).Include(p => p.ProductGroups);

			#region 搜尋條件
			if (criteria.Name != null)
			{
				query = query.Where(p => p.ProductId.Contains(criteria.Name) ||
										p.ProductName.Contains(criteria.Name) ||
										p.Tag.Contains(criteria.Name) ||
										p.ProductSubCategory.SubCategoryPath.Contains(criteria.Name));
			}
			if (criteria.ProductSubCategoryId != null && criteria.ProductSubCategoryId.Value > 0)
			{
				query = query.Where(p => p.fk_ProductSubCategoryId == criteria.ProductSubCategoryId);
			}
			if (criteria.Status != null && criteria.Status != "請選擇狀態")
			{
				if (criteria.Status == "上架中")
				{
					query = query.Where(p => DateTime.Now >= p.StartTime && (p.EndTime == null || DateTime.Now <= p.EndTime));
				}
				else if (criteria.Status == "待上架")
				{
					query = query.Where(p => DateTime.Now < p.StartTime && (p.EndTime == null || DateTime.Now <= p.EndTime));
				}
				else if (criteria.Status == "已下架")
				{
					query = query.Where(p => p.EndTime != null && DateTime.Now > p.EndTime && DateTime.Now > p.StartTime);
				}

			}
			#endregion

			var products = query.OrderBy(p => p.CreateTime).ToList().Select(p => p.ToIndexDto()).ToList();

			//排除LogOut
			products=products.Where(p=>p.LogOut==false).ToList();
			return products;
		}		
	}
}
