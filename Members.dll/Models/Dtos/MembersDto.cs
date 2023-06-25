using EFModels.EFModels;
using Members.dll.Models.ViewsModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Members.dll.Models.Dtos
{
	public class MembersDto
	{
		public int MemberId { get; set; }

		public string Name { get; set; }

		public bool? Gender { get; set; }

		public string Email { get; set; }

		public int fk_LevelId { get; set; }

		public int PointSubtotal { get; set; }

		public DateTime? Registration { get; set; }

		public int? fk_BlackListId { get; set; }
	}

	//擴充方法，將MembersDto 轉為 MembersVM，將資料取出
	public static class MembersExts
	{
		public static MembersIndexVM ToIndexVM(this Member emtity)
		{
			return new MembersIndexVM()
			{
				MemberId = emtity.MemberId,
				Name = emtity.Name,
				Gender = emtity.Gender,
				Email = emtity.Email,
				fk_LevelId = emtity.fk_LevelId,
				//PointSubtotal = emtity.PointSubtotal, 需要經過join才有的欄位該如何傳入
				Registration = emtity.Registration,
				fk_BlackListId = emtity.fk_BlackListId
			};
		}

	}
}
