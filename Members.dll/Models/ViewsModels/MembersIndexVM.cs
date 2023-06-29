using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Members.dll.Models.ViewsModels
{
	public class MembersIndexVM
	{
		public int MemberId { get; set; }

		[Display(Name = "姓名")]
		[Required]
		[StringLength(30)]
		public string Name { get; set; }

		[Display(Name = "性別")]
		public bool? Gender { get; set; }

		[Display(Name = "信箱")]
		[Required]
		[StringLength(300)]
		public string Email { get; set; }

		[Display(Name = "等級")]
		public string LevelName { get; set; }

		[Display(Name = "累計積分")]
		public int PointSubtotal { get; set; }

		[Display(Name = "註冊時間")]
		public DateTime? Registration { get; set; }

		[Display(Name = "是否為黑名單")]
		public int? fk_BlackListId { get; set; }
	}
}
