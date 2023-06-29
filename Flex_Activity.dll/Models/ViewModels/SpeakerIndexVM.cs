﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flex_Activity.dll.Models.ViewModels
{
	public class SpeakerIndexVM
	{
		public int SpeakerId { get; set; }

		[Required]
		[StringLength(50)]
		public string SpeakerName { get; set; }

		public string FieldName { get; set; }
	}
}
