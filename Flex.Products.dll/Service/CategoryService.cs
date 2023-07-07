using EFModels.EFModels;
using Flex.Products.dll.Exts;
using Flex.Products.dll.Interface;
using Flex.Products.dll.Models.Dtos;
using Flex.Products.dll.Models.Infra.Exts;
using Flex.Products.dll.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flex.Products.dll.Service
{
	public class CategoryService
	{
		private ICategoryRepository _repo;
		private AppDbContext _db;
		public CategoryService(ICategoryRepository repo)
		{
			_repo = repo;
			_db = new AppDbContext();
		}

		public IEnumerable<SalesCategoryDto> Search()
		{
			var salseCategory = _repo.Search();
			return salseCategory;
		}

		public Result CreateSalesCategory(SalesCategoryDto dto)
		{
			var result = ExisSalesCategoryName(dto.SalesCategoryName);
			if (result)
			{
				return Result.Fail("分類名稱已存在，請確認");
			}
			_repo.CreateSalesCategory(dto);
			return Result.Success();
		}

		public Result EditSalesCategory(SalesCategoryDto dto)
		{

			var result = ExisSalesCategoryName(dto.SalesCategoryName);

			if (result)
			{
				return Result.Fail("分類名稱已存在，請確認");
			}

			_repo.EditSalesCategory(dto);
			return Result.Success();


		}

		public SalesCategoryDto GetSalesCategoryById(int salesCategoryId)
		{
			var result = _repo.GetSalesCategoryById(salesCategoryId);

			return result;
		}

		public Result DeleteSalesCategory(int salesCategoryId)
		{
			 _repo.DeleteSalesCategory(salesCategoryId);
			return Result.Success();
		}

		public bool ExisSalesCategoryName(string salesCatogoryName)
		{
			return _db.SalesCategories.Any(s => s.SalesCategoryName == salesCatogoryName);
		}
	}

}
