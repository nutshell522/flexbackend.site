﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Members.dll.Models.ViewsModels.PointHistories
{

	public class IndexVM
	{
		public int PointHistoryId { get; set; }

		[Display(Name="會員名稱")]
		public string Name { get; set; }

		[Display(Name = "訂單編號")]
		public int Id { get; set; }

		[Display(Name = "訂單類型")]
		public string TypeName { get; set; }

		[Display(Name = "異動情形")]
		public bool GetOrDeduct { get; set; }

		[Display(Name = "異動數量")]
		public int UsageAmount { get; set; }

		[Display(Name = "異動時間")]
		public DateTime ordertime { get; set; }

		[Display(Name = "累計積分")]
		public int PointSubtotal { get; set; }
	}
}
