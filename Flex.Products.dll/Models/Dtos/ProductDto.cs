using Flex.Products.dll.Models.Infra.Exts;
using Flex.Products.dll.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flex.Products.dll.Models.Dtos
{
	public class ProductDto
	{
		public string ProductId { get; set; }

		public string ProductName { get; set; }

		public string ProductDescription { get; set; }

		public string ProductMaterial { get; set; }

		public string ProductOrigin { get; set; }

		public int? UnitPrice { get; set; }

		public int SalesPrice { get; set; }

		public DateTime StartTime { get; set; }

		public DateTime? EndTime { get; set; }

		public bool LogOut { get; set; }

		public string Tag { get; set; }

		public int fk_ProductSubCategoryId { get; set; }

		public DateTime CreateTime { get; set; }

		public DateTime EditTime { get; set; }

		public List<string> ImgPaths { get; set; }

		public IEnumerable<ProductGroupClass> productGroups { get; set; }
	}
	public static class ProductExts
	{
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
				productGroups = vm.productGroups,
			};
		}
	}
}
