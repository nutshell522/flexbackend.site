using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Members.dll.Models.ViewsModels
{
	public class StaffsCreateVM
	{
		[Display(Name = "編號")]
		public int StaffId { get; set; }

		[Display(Name = "部門")]
		[Required]
		public string Department { get; set; }

		[Display(Name = "職稱")]
		[Required]
		public string JobTitle { get; set; }

		[Display(Name = "姓名")]
		[Required]
		[StringLength(30)]
		public string Name { get; set; }

		[Display(Name = "年齡")]
		public byte? Age { get; set; }

		[Display(Name = "生日")]
		[Required]
		public DateTime Birthday { get; set; }

		[Display(Name = "性別")]
		[Required]
		public bool? Gender { get; set; }

		[Display(Name = "信箱")]
		[Required]
		[StringLength(300)]
		[EmailAddress]
		public string Email { get; set; }

		[Display(Name = "權限")]
		[Required]
		public string StaffPermission { get; set; }

	}
}
