using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customized_Shoes.dll.Models.ViewModels
{
	public class SupplierEditVM
	{
		public int SupplierId { get; set; }

	
		[StringLength(50)]
		public string SupplierCompanyName { get; set; }

	
		public bool? HasCertificate { get; set; }

	
		[StringLength(50)]
		public string Supply_Material { get; set; }

	
		public int? SupplierCompanyPhone { get; set; }

		
		[StringLength(250)]
		public string SupplierCompanyEmail { get; set; }

		
		[StringLength(250)]
		public string SupplierCompanyAddress { get; set; }


		
		public int? SupplierCompanyNumber { get; set; }

	
		public DateTime? SupplierStartDate { get; set; }
	}
}
