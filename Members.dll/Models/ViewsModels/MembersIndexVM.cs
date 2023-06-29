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

		[Required]
		[StringLength(30)]
		public string Name { get; set; }

		public bool? Gender { get; set; }

		[Required]
		[StringLength(300)]
		public string Email { get; set; }

		//public int fk_LevelId { get; set; }

		public string LevelName { get; set; }

		public int PointSubtotal { get; set; } //這個拿掉畫面上就沒有了

		public DateTime? Registration { get; set; }

		public int? fk_BlackListId { get; set; }
	}
}
