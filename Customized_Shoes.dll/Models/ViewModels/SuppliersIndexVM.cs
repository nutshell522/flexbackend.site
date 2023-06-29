using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Customized_Shoes.dll.Models.ViewModels
{
	public class SuppliersIndexVM
	{
		[Display(Name = "公司加入編號")]
		public int SupplierId { get; set; }

		[Display(Name = "公司名稱")]
		[StringLength(50)]
		public string SupplierCompanyName { get; set; }

		[Display(Name = "是否有合格證明")]
		public bool? HasCertificate { get; set; }

		[Display(Name = "公司提供產品")]
		[StringLength(50)]
		public string Supply_Material { get; set; }

		[Display(Name = "公司電話")]
		public int? SupplierCompanyPhone { get; set; }

		[Display(Name = "電子郵件")]
		[StringLength(250)]
		public string SupplierCompanyEmail { get; set; }

		
		public string SupplierCompanyAddress { get; set; }

		[Display(Name = "公司地址")]
		[StringLength(250)]
		public string SupplierCompanyAddressText
		{
			get
			{
				return this.SupplierCompanyAddress.Length > 10 ? this.SupplierCompanyAddress.Substring(0, 10) + "..." : this.SupplierCompanyAddress;
			}

		}


		[Display(Name = "統一編號")]
		public int? SupplierCompanyNumber { get; set; }

		[Display(Name = "開始合作時間")]
		public DateTime? SupplierStartDate { get; set; }
	}
}
