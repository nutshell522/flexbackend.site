using Dapper;
using Flex.Products.dll.Exts;
using Flex.Products.dll.Interface;
using Flex.Products.dll.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flex.Products.dll.Infra.DapperRepository
{
	public class ProductDPRepository:IProductRepository
	{
		private readonly string _connStr;
		public ProductDPRepository()
		{
			_connStr = System.Configuration.ConfigurationManager.ConnectionStrings["AppDbContext"].ConnectionString;
		}
		public List<ProductExcelReportDto> ReportToExcel()
		{
			string sql = @"select ProductId,ProductName,ProductDescription,ProductMaterial,ProductOrigin,
UnitPrice,SalesPrice,Status,LogOut,Tag,
CreateTime,EditTime,ProductSubCategoryName,ProductCategoryName,SalesCategoryName,
ColorName,SizeName,Qty
from Products
left join ProductGroups on fk_ProductId=ProductId 
join ProductSubCategories on ProductSubCategoryId=fk_ProductSubCategoryId
join ProductCategories on ProductCategoryId=fk_ProductCategoryId
join SalesCategories on SalesCategoryId=fk_SalesCategoryId
join ColorCategories on ColorId=fk_ColorId
join SizeCategories on SizeId=fk_SizeId
where LogOut=0;";

			var result=new SqlConnection(_connStr).Query<ProductExcelReportDto>(sql).ToList();
			return result;
		}

		public void CreateProduct(ProductDto dto)
		{
			throw new NotImplementedException();
		}

		public void DeleteProduct(string productId)
		{
			throw new NotImplementedException();
		}

		public void EditProduct(ProductDto dto)
		{
			throw new NotImplementedException();
		}

		public ProductDto GetById(string productId)
		{
			throw new NotImplementedException();
		}

		public List<ProductImgDto> GetImgById(string productId)
		{
			throw new NotImplementedException();
		}

		public void SaveChangeStatus(List<ProductDto> dto)
		{
			throw new NotImplementedException();
		}

		public void SaveEditImg(List<ProductImgDto> dto)
		{
			throw new NotImplementedException();
		}

		public List<ProductDto> Search(IndexSearchCriteria criteria)
		{
			throw new NotImplementedException();
		}
	}
}
