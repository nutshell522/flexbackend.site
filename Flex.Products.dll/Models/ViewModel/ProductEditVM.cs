using Flex.Products.dll.Models.Infra.Exts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Flex.Products.dll.Models.ViewModel
{
	public class ProductEditVM
	{
		[Display(Name = "商品編碼")]
		public string ProductId { get; set; }

		[Display(Name = "商品名稱")]
		[Required(ErrorMessage = "{0}必填")]
		[StringLength(254)]
		public string ProductName { get; set; }

		[Display(Name = "商品描述")]
		[Required(ErrorMessage = "{0}必填")]
		public string ProductDescription { get; set; }

		[Display(Name = "材質")]
		[StringLength(50)]
		[MaxLength(50, ErrorMessage = "長度限制為{1}個字")]
		public string ProductMaterial { get; set; }

		[Display(Name = "產地")]
		[Required(ErrorMessage = "{0}必填")]
		[StringLength(50)]
		[MaxLength(50, ErrorMessage = "長度限制為{1}個字")]
		public string ProductOrigin { get; set; }

		[Display(Name = "標籤")]
		[StringLength(100)]
		[MaxLength(100, ErrorMessage = "長度限制為{1}個字")]
		public string Tag { get; set; }

		[Display(Name = "定價")]
		[Range(0, 99999, ErrorMessage = "價格須介於{1}至{2}之間")]
		public int? UnitPrice { get; set; }

		[Display(Name = "售價")]
		[Required(ErrorMessage = "{0}必填")]
		[Range(0, 99999, ErrorMessage = "價格須介於{1}至{2}之間")]
		public int SalesPrice { get; set; }

		[Display(Name = "下架")]
		public bool Status { get; set; }

		[Display(Name = "商品分類")]
		[Required(ErrorMessage = "{0}必填")]
		[Range(1, 999, ErrorMessage = "{0}必填")]
		public int fk_ProductSubCategoryId { get; set; }

		public List<ProductGroupClass> ProductGroupList { get; set; } = new List<ProductGroupClass>();

		[Display(Name = "庫存")]
		public string ProductGroup
		{
			get
			{
				var productGroupList = new List<string>();
				foreach (var group in ProductGroupList)
				{
					productGroupList.Add($"{group.ColorName} / {group.SizeName} / {group.Qty}");
				}
				return string.Join(Environment.NewLine, productGroupList);
			}
		}
	}
}
