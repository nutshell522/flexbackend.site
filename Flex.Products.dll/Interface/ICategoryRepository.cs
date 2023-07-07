using Flex.Products.dll.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flex.Products.dll.Interface
{
	public interface ICategoryRepository
	{
		List<SalesCategoryDto> Search();

		void CreateSalesCategory(SalesCategoryDto dto);

		SalesCategoryDto GetSalesCategoryById(int salesCategoryId);

		void EditSalesCategory(SalesCategoryDto dto);

		void DeleteSalesCategory(int salesCategoryId);
	}
}
