using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Members.dll.Models.Dtos.Member
{
	public class LevelsIndexDto
	{
		public int LevelId { get; set; }

		public string LevelName { get; set; }

		public string MinSpending { get; set; }

		public int? Discount { get; set; }

		public string Description { get; set; }

		//public string PrivilegeName { get; set; }
	}
}
