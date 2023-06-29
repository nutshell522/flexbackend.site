using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flex_Activity.dll.Models.ViewModels
{
	public class ActivityIndexVM
	{
		public int ActivityId { get; set; }

		[Required]
		[StringLength(50)]
		public string ActivityName { get; set; }

		public string ActivityCategoryName { get; set; }

		public DateTime ActivityDate { get; set; }

		public string SpeakerName { get; set; }

		[Required]
		[StringLength(100)]
		public string ActivityPlace { get; set; }


		public DateTime ActivityBookStartTime { get; set; }

		public DateTime ActivityBookEndTime { get; set; }

		public string ActivityStatusDescription { get; set; }
	}
}
