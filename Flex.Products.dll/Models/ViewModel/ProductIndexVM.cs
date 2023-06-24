using EFModels.EFModels;
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
	public class ProductIndexVM
	{
		
		public string ProductId { get; set; }

		[Display(Name = "商品名稱")]
		public string ProductName { get; set; }

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

		[Display(Name = "分類")]
		public string SubCategoryPath { get; set; }

		[Display(Name = "售價")]
		[DisplayFormat(DataFormatString = "{0:#,#}", ApplyFormatInEditMode = false)]
		public int SalesPrice { get; set; }

		public DateTime StartTime { get; set; }

		public DateTime? EndTime { get; set; }

		[Display(Name = "狀態")]
		public string status 
		{
			get
			{
				if(DateTime.Now > StartTime && (EndTime == null || DateTime.Now <= EndTime))
				{
					return "上架中";
				}
				else if(DateTime.Now < StartTime)
				{
					return "待上架";
				}
				else { return "已下架"; }
			}
		}

		[Display(Name = "標籤")]
		public string Tag { get; set; }

	}
}
