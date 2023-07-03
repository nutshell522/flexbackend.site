using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Members.dll.Models.ViewsModels.Staff
{
	public class ForgetPasswordVM
	{
		[Display(Name = "帳號")]
		[Required]
		public string Account { get; set; }

		[Display(Name = "新設密碼")]
		[Required]
		[DataType(DataType.Password)]
		[StringLength(10)]
		public string NewPassword { get; set; }

		[Display(Name = "確認密碼")]
		[Required]
		[DataType(DataType.Password)]
		[StringLength(10)]
		public string ConfirmPassword { get; set; }
	}
}
