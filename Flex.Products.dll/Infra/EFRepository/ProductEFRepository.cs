using EFModels.EFModels;
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
					query = query.Where(p => p.Status == false);
				}
				if (criteria.Status == "已下架")
				{
					query = query.Where(p => p.Status == true);
				}
			}
			#endregion

			var products = query.OrderByDescending(p => p.CreateTime).ToList().Select(p => p.ToIndexDto()).ToList();

			//排除LogOut
			products=products.Where(p=>p.LogOut==false).ToList();
			return products;
		}		

		public void EditProductsStatus(List<ProductDto> dto)
		{
			foreach (var item in dto)
			{
				var product=_db.Products.FirstOrDefault(p => p.ProductId == item.ProductId);
				if (product == null) return;

				product.Status = item.Status;
				product.EditTime = DateTime.Now;

				_db.SaveChanges();
			}
		}
	}
}
