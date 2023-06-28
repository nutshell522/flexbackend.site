using EFModels.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Members.dll.Models.ViewsModels
{
	public class StaffsIndexVM
	{
		public int StaffId { get; set; }

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

		public DateTime? dueDate { get; set; }

		public string Department { get; set; }

		public string JobTitle { get; set; }

		public string StaffPermission { get; set; }
	}
}
