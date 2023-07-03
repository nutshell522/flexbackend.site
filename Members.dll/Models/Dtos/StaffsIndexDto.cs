using EFModels.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Members.dll.Models.Dtos
{
	public class StaffsIndexDto
	{
		public int StaffId { get; set; }

		public string Name { get; set; }

		public byte? Age { get; set; }

		public bool? Gender { get; set; }

		public string Email { get; set; }

		public DateTime? DueDate { get; set; }

		public string Department { get; set; }

		public string TitleName { get; set; }

		public string LevelName { get; set; }
	}
}
