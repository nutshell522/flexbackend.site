using EFModels.EFModels;
using Flex.Products.dll.Models.Dtos;
using Flex.Products.dll.Models.Infra.Exts;
using Flex.Products.dll.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flex.Products.dll.Models.Infra.EFRepository
{
	public class ProductEFRepository : IProductRepository
	{
		private AppDbContext _db;
        public ProductEFRepository()
        {
			_db = new AppDbContext();
        }
		public bool ExisProductID(string ProductId)
		{
			return _db.Products.Any(p => p.ProductId == ProductId);
		}
		public void CreateProduct(ProductDto dto)
		{
			//將ProductDto依序轉換成Products,group,img存入database
			var product = new Product
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
			_db.Products.Add(product);
			
			foreach(var img in dto.ImgPaths)
			{
				var productimg = new ProductImg
				{
					fk_ProductId = dto.ProductId,
					ImgPath = img
				};
				_db.ProductImgs.Add(productimg);
			}

			foreach(var group in dto.productGroups)
			{
				var productGroup = new ProductGroup
				{
					fk_ProductId = dto.ProductId,
					fk_ColorId = group.ColorId,
					fk_SizeID = group.SizeId,
					Qty = group.Qty,
				};
				_db.ProductGroups.Add(productGroup);
			}

			_db.SaveChanges();
		}

		public bool ValidationStartAndEndTime(DateTime start, DateTime? end)
		{
			if(end!=null && start > end) { return true; }
			return false;
        }
	}
}
