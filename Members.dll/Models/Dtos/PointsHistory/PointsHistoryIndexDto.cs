using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Members.dll.Models.Dtos
{
	public class PointsHistoryIndexDto
	{
		public int PointHistoryId { get; set; }
		public string Name { get; set; }
		public int Id { get; set; }
		public string TypeName { get; set; }
		public bool GetOrDeduct { get; set; }
		public int UsageAmount { get; set; }
		public DateTime ordertime { get; set; }
		public int PointSubtotal { get; set; }
	}
}
