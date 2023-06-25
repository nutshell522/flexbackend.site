using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Members.dll.Models.ViewsModels
{
	public class MembersEditVM
	{
		public int MemberId { get; set; }

		[Required]
		[StringLength(30)]
		public string Name { get; set; }

		public byte? Age { get; set; }

		public bool? Gender { get; set; }

		[Required]
		[StringLength(10)]
		public string Mobile { get; set; }

		[Required]
		[StringLength(300)]
		public string Email { get; set; }

		[Column(TypeName = "date")]
		public DateTime? Birthday { get; set; }


		public DateTime? Registration { get; set; }


		public int fk_LevelId { get; set; }

		public int? fk_BlackListId { get; set; }
	}
}
