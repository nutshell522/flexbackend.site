﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Members.dll.Models.ViewsModels.Member
{
	public class MemberDetail
	{
		[Display(Name = "會員編號")]
		public int MemberId { get; set; }

		[Display(Name = "姓名")]
		[StringLength(30)]
		public string Name { get; set; }

		[Display(Name = "年紀")]
		public byte? Age { get; set; }

		[Display(Name = "性別")]
		public bool? Gender { get; set; }

		[Display(Name = "手機")]
		public string Mobile { get; set; }

		[Display(Name = "信箱")]
		public string Email { get; set; }

		[Display(Name = "生日")] 
		public DateTime? Birthday { get; set; }

		[Display(Name = "註冊時間")]
		public DateTime? Registration { get; set; }

		[Display(Name = "會員等級")]
		public int fk_LevelId { get; set; }

		[Display(Name = "是否為黑名單")]
		public int? fk_BlackListId { get; set; }
	}
}
