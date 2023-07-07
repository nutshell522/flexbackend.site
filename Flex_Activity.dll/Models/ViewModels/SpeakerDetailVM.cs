﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flex_Activity.dll.Models.ViewModels
{
	public class SpeakerDetailVM
	{
		[Display(Name ="講師編號")]
		public int SpeakerId { get; set; }

		[Display(Name ="講師姓名")]
		public string SpeakerName { get; set; }

		[Display(Name ="連絡電話")]
		public string SpeakerPhone { get; set; }

		[Display(Name ="專攻領域")]
		public string FieldName { get; set; }

		[Display(Name ="講師照片")]
		public string SpeakerImg { get; set; }

		[Display(Name ="駐點分店")]
		public string BranchName { get; set; }

		[Display(Name ="講師敘述")]
		public string SpeakerDescription { get; set; }
	}
}